using CsvHelper;
using CsvHelper.Configuration;
using GoldenRaspberryAwards.Core.Models;
using GoldenRaspberryAwards.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace GoldenRaspberryAwards.Data.Tests.Services
{
    public class DataLoaderServiceTests
    {
        private AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "GoldenRaspberryAwardsDb")
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task LoadDataAsync_ShouldLoadMoviesFromCsvFile()
        {
            // Arrange
            var csvContent = "Year;Title;Studios;Producers;Winner\n" +
                             "1980;Can't Stop the Music;Associated Film Distribution;Allan Carr;yes\n" +
                             "1981;Mommie Dearest;Paramount Pictures;Frank Yablans;yes";

            var filePath = Path.Combine(AppContext.BaseDirectory, "Files", "movielist.csv");

            // Cria a pasta e o arquivo CSV fictício
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
            await File.WriteAllTextAsync(filePath, csvContent);

            var context = CreateInMemoryContext();
            var service = new DataLoaderService(context);

            // Configuração do CsvHelper para ignorar a validação de cabeçalho
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HeaderValidated = null, // Ignora a validação de cabeçalhos
                MissingFieldFound = null // Ignora campos ausentes
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                // Agora, vamos usar diretamente a leitura sem o ClassMap
                var records = csv.GetRecords<dynamic>().ToList();

                // Convertendo para a lista de filmes
                var movies = records.Select(record => new Movie
                {
                    Year = int.Parse(record.Year),
                    Title = record.Title,
                    Studios = record.Studios,
                    Producers = record.Producers,
                    IsWinner = record.Winner.ToLower() == "yes"
                }).ToList();

                // Salva os filmes no banco de dados
                await context.AddRangeAsync(movies);
                await context.SaveChangesAsync();
            }

            // Act
            var loadedMovies = await context.Movies.ToListAsync();

            // Assert
            Assert.Equal(7, loadedMovies.Count);

            var movie1 = loadedMovies[0];
            Assert.Equal(1980, movie1.Year);
            Assert.Equal("Can't Stop the Music", movie1.Title);
            Assert.Equal("Associated Film Distribution", movie1.Studios);
            Assert.Equal("Allan Carr", movie1.Producers);
            Assert.True(movie1.IsWinner);

            var movie2 = loadedMovies[1];
            Assert.Equal(1981, movie2.Year);
            Assert.Equal("Mommie Dearest", movie2.Title);
            Assert.Equal("Paramount Pictures", movie2.Studios);
            Assert.Equal("Frank Yablans", movie2.Producers);
            Assert.True(movie2.IsWinner);

            // Cleanup
            File.Delete(filePath);
        }
    }
}

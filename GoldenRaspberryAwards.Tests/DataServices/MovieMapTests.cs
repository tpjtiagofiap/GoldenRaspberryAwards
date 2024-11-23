using CsvHelper;
using CsvHelper.Configuration;
using GoldenRaspberryAwards.Core.Mappings;
using GoldenRaspberryAwards.Core.Models;
using System.Globalization;

namespace GoldenRaspberryAwards.Domain.Tests.Mappings
{
    public class MovieMapTests
    {
        [Fact]
        public void MovieMap_ShouldMapPropertiesCorrectly()
        {
            // Arrange
            var csvData = "1994,The Shawshank Redemption,Castle Rock Entertainment,Frank Darabont,yes";
            var movieMap = new MovieMap();
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = false
            };
            var csvReader = new CsvReader(new StringReader(csvData), csvConfig);
            csvReader.Context.RegisterClassMap(movieMap);

            // Act
            csvReader.Read(); 
            var movie = csvReader.GetRecord<Movie>();

            // Assert
            Assert.Equal("The Shawshank Redemption", movie.Title);
            Assert.Equal("Frank Darabont", movie.Producers);
            Assert.Equal(1994, movie.Year);
            Assert.Equal("Castle Rock Entertainment", movie.Studios);
            Assert.True(movie.IsWinner);
        }



    }
}

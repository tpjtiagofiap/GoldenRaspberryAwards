using CsvHelper;
using CsvHelper.Configuration;
using GoldenRaspberryAwards.Core.Mappings;
using GoldenRaspberryAwards.Core.Contracts;
using GoldenRaspberryAwards.Core.Models;
using System.Globalization;

namespace GoldenRaspberryAwards.Data.Services
{
    public class DataLoaderService : IDataLoaderService
    {
        private readonly AppDbContext _context;

        public DataLoaderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task LoadDataAsync()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "Files", "movielist.csv");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            csv.Context.RegisterClassMap<MovieMap>();

            var movies = csv.GetRecords<Movie>().ToList();
            await _context.AddRangeAsync(movies);
            await _context.SaveChangesAsync();
        }

    }
}

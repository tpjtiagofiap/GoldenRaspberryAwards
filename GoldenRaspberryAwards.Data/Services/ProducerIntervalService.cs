using GoldenRaspberryAwards.Applicaction.Contracts;
using GoldenRaspberryAwards.Core.Models;

namespace GoldenRaspberryAwards.Data.Services
{
    public class ProducerIntervalService : IProducerIntervalService
    {
        private readonly AppDbContext _context;

        public ProducerIntervalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProducerInterval>> GetProducerAwardIntervalsAsync()
        {
            var movies = _context.Movies
                .Where(m => m.IsWinner && !string.IsNullOrEmpty(m.Producers))
                .AsEnumerable()
                .SelectMany(m => m.Producers.Split(new[] { ", " }, StringSplitOptions.None)
                    .Select(p => new { m.Year, Producer = p }))
                .OrderBy(m => m.Producer)
                .ThenBy(m => m.Year)
                .ToList();

            var intervals = new List<ProducerInterval>();
            var groupedByProducer = movies.GroupBy(m => m.Producer);

            foreach (var group in groupedByProducer)
            {
                var wins = group.Select(g => g.Year).ToList();
                for (int i = 1; i < wins.Count; i++)
                {
                    intervals.Add(new ProducerInterval
                    {
                        Producer = group.Key,
                        Interval = wins[i] - wins[i - 1],
                        PreviousWin = wins[i - 1],
                        FollowingWin = wins[i]
                    });
                }
            }

            return intervals;
        }
    }
}

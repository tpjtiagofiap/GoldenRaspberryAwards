using GoldenRaspberryAwards.Api.Contracts;
using GoldenRaspberryAwards.Api.Data;
using GoldenRaspberryAwards.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberryAwards.Api.Services
{
    public class ProducerIntervalService : IProducerIntervalService
    {
        private readonly AppDbContext _context;

        public ProducerIntervalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetProducerAwardIntervalsAsync()
        {
            // Obtenha a lista de filmes vencedores e seus produtores
            var winners = await _context.Movies
                .Where(m => m.IsWinner)
                .OrderBy(m => m.Year)  // Ordena por ano para garantir que os prêmios estão na ordem correta
                .ToListAsync();

            // Inicializa a lista de intervalos de produtores
            var producerIntervals = new List<ProducerInterval>();

            // Preenche a lista de intervalos de produtores
            foreach (var movie in winners)
            {
                var producers = movie.Producers.Split(';');  // Supondo que os produtores estão separados por ponto e vírgula.

                foreach (var producer in producers)
                {
                    // Encontra o prêmio anterior do produtor
                    var previousWins = winners
                        .Where(w => w.Producers.Contains(producer) && w.Year < movie.Year)
                        .OrderByDescending(w => w.Year)
                        .FirstOrDefault();

                    if (previousWins != null)
                    {
                        // Calcula o intervalo entre os prêmios
                        var interval = movie.Year - previousWins.Year;
                        producerIntervals.Add(new ProducerInterval
                        {
                            Producer = producer,
                            Interval = interval,
                            PreviousWin = previousWins.Year,
                            FollowingWin = movie.Year
                        });
                    }
                }
            }

            // Verifique se há intervalos
            if (!producerIntervals.Any())
            {
                return new { min = new List<object>(), max = new List<object>() };  // Se não houver intervalos, retorne vazio
            }

            // Encontrar o intervalo mínimo por produtor (menor intervalo)
            var minInterval = producerIntervals
                .GroupBy(p => p.Producer)
                .Select(group => new
                {
                    Producer = group.Key,
                    MinInterval = group.OrderBy(p => p.Interval).FirstOrDefault()  // O menor intervalo
                })
                .Where(x => x.MinInterval != null)  // Filtra intervalos nulos
                .Select(x => new
                {
                    producer = x.Producer,
                    interval = x.MinInterval.Interval,
                    previousWin = x.MinInterval.PreviousWin,
                    followingWin = x.MinInterval.FollowingWin
                })
                .ToList();

            // Encontrar o intervalo máximo por produtor (maior intervalo)
            var maxInterval = producerIntervals
                .GroupBy(p => p.Producer)
                .Select(group => new
                {
                    Producer = group.Key,
                    MaxInterval = group.OrderByDescending(p => p.Interval).FirstOrDefault()  // O maior intervalo
                })
                .Where(x => x.MaxInterval != null)  // Filtra intervalos nulos
                .Select(x => new
                {
                    producer = x.Producer,
                    interval = x.MaxInterval.Interval,
                    previousWin = x.MaxInterval.PreviousWin,
                    followingWin = x.MaxInterval.FollowingWin
                })
                .ToList();

            // Retornar os intervalos no formato desejado
            return new
            {
                min = minInterval,
                max = maxInterval
            };
        }



    }
}

using GoldenRaspberryAwards.Core.Models;

namespace GoldenRaspberryAwards.Applicaction.Contracts
{
    public interface IProducerIntervalService
    {
        Task<List<ProducerInterval>> GetProducerAwardIntervalsAsync();
    }
}

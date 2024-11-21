namespace GoldenRaspberryAwards.Api.Contracts
{
    public interface IProducerIntervalService
    {
        Task<object> GetProducerAwardIntervalsAsync();
    }
}

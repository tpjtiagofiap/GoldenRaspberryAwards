using GoldenRaspberryAwards.Core.Models;
using Xunit;

namespace GoldenRaspberryAwards.Domain.Tests.Models
{
    public class ProducerIntervalTests
    {
        [Fact]
        public void CanCreateProducerInterval()
        {
            // Arrange
            string producer = "John Doe";
            int interval = 5;
            int previousWin = 2000;
            int followingWin = 2005;

            // Act
            var producerInterval = new ProducerInterval
            {
                Producer = producer,
                Interval = interval,
                PreviousWin = previousWin,
                FollowingWin = followingWin
            };

            // Assert
            Assert.Equal(producer, producerInterval.Producer);
            Assert.Equal(interval, producerInterval.Interval);
            Assert.Equal(previousWin, producerInterval.PreviousWin);
            Assert.Equal(followingWin, producerInterval.FollowingWin);
        }
    }
}

using GoldenRaspberryAwards.Core.Models;
using Xunit;

namespace GoldenRaspberryAwards.Domain.Tests.Models
{
    public class ProducerTests
    {
        [Fact]
        public void Producer_Id_Should_SetCorrectly()
        {
            // Arrange
            int expectedId = 1;

            // Act
            Producer producer = new Producer();
            producer.Id = expectedId;

            // Assert
            Assert.Equal(expectedId, producer.Id);
        }

        [Fact]
        public void Producer_Name_Should_SetCorrectly()
        {
            // Arrange
            string expectedName = "John Doe";

            // Act
            Producer producer = new Producer();
            producer.Name = expectedName;

            // Assert
            Assert.Equal(expectedName, producer.Name);
        }
    }
}

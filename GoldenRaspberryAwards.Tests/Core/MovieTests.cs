using GoldenRaspberryAwards.Core.Models;
using Xunit;

namespace GoldenRaspberryAwards.Domain.Tests
{
    public class MovieTests
    {
        [Fact]
        public void MovieProperties_SetAndGetCorrectly()
        {
            // Arrange
            int id = 1;
            string title = "The Room";
            string studios = "Wiseau Films";
            int year = 2003;
            bool isWinner = true;
            string producers = "Tommy Wiseau";

            // Act
            Movie movie = new Movie
            {
                Id = id,
                Title = title,
                Studios = studios,
                Year = year,
                IsWinner = isWinner,
                Producers = producers
            };

            // Assert
            Assert.Equal(id, movie.Id);
            Assert.Equal(title, movie.Title);
            Assert.Equal(studios, movie.Studios);
            Assert.Equal(year, movie.Year);
            Assert.Equal(isWinner, movie.IsWinner);
            Assert.Equal(producers, movie.Producers);
        }
    }
}

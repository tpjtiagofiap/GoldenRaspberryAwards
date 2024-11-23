using GoldenRaspberryAwards.Core.Models;
using GoldenRaspberryAwards.Data;
using GoldenRaspberryAwards.Data.Services;
using Microsoft.EntityFrameworkCore;

public class ProducerIntervalServiceTests
{
    [Fact]
    public async Task GetProducerAwardIntervalsAsync_ShouldReturnCorrectIntervals_WhenDataIsValid()
    {
        // Arrange
        var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("GoldenRaspberryAwardsDb")
            .Options);

        context.Movies.AddRange(new List<Movie>
        {
            new Movie { Year = 1980, Title = "Movie 1", Producers = "Producer A", Studios = "Studio A", IsWinner = true },
            new Movie { Year = 1985, Title = "Movie 2", Producers = "Producer A", Studios = "Studio B", IsWinner = true },
            new Movie { Year = 1990, Title = "Movie 3", Producers = "Producer A", Studios = "Studio C", IsWinner = true },
            new Movie { Year = 1982, Title = "Movie 4", Producers = "Producer B", Studios = "Studio D", IsWinner = true },
            new Movie { Year = 1986, Title = "Movie 5", Producers = "Producer B", Studios = "Studio E", IsWinner = true }
        });

        await context.SaveChangesAsync();

        var service = new ProducerIntervalService(context);

        // Act
        var result = await service.GetProducerAwardIntervalsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(9, result.Count);
    }

    [Fact]
    public async Task GetProducerAwardIntervalsAsync_ShouldReturnEmptyList_WhenNoDataExists()
    {
        // Arrange
        var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("GoldenRaspberryAwardsDb")
            .Options);

        var service = new ProducerIntervalService(context);

        // Act
        var result = await service.GetProducerAwardIntervalsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetProducerAwardIntervalsAsync_ShouldReturnEmptyList_WhenNoWinnerExists()
    {
        // Arrange
        var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("GoldenRaspberryAwardsDb")
            .Options);

        context.Movies.AddRange(new List<Movie>
        {
            new Movie { Year = 1980, Title = "Movie 1", Producers = "Producer A", Studios = "Studio A", IsWinner = false },
            new Movie { Year = 1985, Title = "Movie 2", Producers = "Producer A", Studios = "Studio B", IsWinner = false },
            new Movie { Year = 1990, Title = "Movie 3", Producers = "Producer A", Studios = "Studio C", IsWinner = false },
            new Movie { Year = 1982, Title = "Movie 4", Producers = "Producer B", Studios = "Studio D", IsWinner = false },
            new Movie { Year = 1986, Title = "Movie 5", Producers = "Producer B", Studios = "Studio E", IsWinner = false }
        });

        await context.SaveChangesAsync();

        var service = new ProducerIntervalService(context);

        // Act
        var result = await service.GetProducerAwardIntervalsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetProducerAwardIntervalsAsync_ShouldReturnCorrectIntervals_WhenMultipleWinnersExist()
    {
        // Arrange
        var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("GoldenRaspberryAwardsDb")
            .Options);

        context.Movies.AddRange(new List<Movie>
        {
            new Movie { Year = 1980, Title = "Movie 1", Producers = "Producer A", Studios = "Studio A", IsWinner = true },
            new Movie { Year = 1985, Title = "Movie 2", Producers = "Producer A", Studios = "Studio B", IsWinner = true },
            new Movie { Year = 1990, Title = "Movie 3", Producers = "Producer A", Studios = "Studio C", IsWinner = true },
            new Movie { Year = 1982, Title = "Movie 4", Producers = "Producer B", Studios = "Studio D", IsWinner = true },
            new Movie { Year = 1986, Title = "Movie 5", Producers = "Producer B", Studios = "Studio E", IsWinner = true },
            new Movie { Year = 1987, Title = "Movie 6", Producers = "Producer C", Studios = "Studio F", IsWinner = true },
            new Movie { Year = 1988, Title = "Movie 7", Producers = "Producer C", Studios = "Studio G", IsWinner = true }
        });

        await context.SaveChangesAsync();

        var service = new ProducerIntervalService(context);

        // Act
        var result = await service.GetProducerAwardIntervalsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4, result.Count);
    }
}

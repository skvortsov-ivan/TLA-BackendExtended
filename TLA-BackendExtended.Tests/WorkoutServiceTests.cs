using Moq;
using System.Threading.Tasks;
using TLA_BackendExtended.Clients;
using TLA_BackendExtended.DTOs;
using TLA_BackendExtended.Models;
using TLA_BackendExtended.Services;
using Xunit;

namespace TLA_BackendExtended.Tests
{
    public class WorkoutServiceTests
    {
        [Fact]
        public async Task GetCaloriesAsync_MapsDtoToModelCorrectly()
        {
            // ARRANGE
            var mockClient = new Mock<IWorkoutClient>();

            mockClient
                .Setup(c => c.FetchCaloriesAsync("running", 80, 30))
                .ReturnsAsync(new CaloriesResponse
                {
                    WorkoutCategory = "running",
                    CaloriesPerHour = 30000,
                    DurationMinutes = 30,
                    TotalCaloriesBurned = 150
                });

            var service = new WorkoutService(mockClient.Object);

            // ACT
            var result = await service.GetCaloriesAsync("running", 80, 30);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("running", result.WorkoutCategory);
            Assert.Equal(30000, result.CaloriesPerHour);
            Assert.Equal(30, result.DurationMinutes);
            Assert.Equal(150, result.TotalCalories);
        }
    }
}


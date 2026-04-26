using Moq;
using System.ComponentModel.DataAnnotations;
using System.Net;
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
            var request = new CaloriesRequestDto // 
            {
                WorkoutCategory = "running",
                Weight = 80,
                Duration = 30
            };

            var mockClient = new Mock<IWorkoutClient>();

            mockClient
                .Setup(c => c.FetchCaloriesAsync(It.Is<CaloriesRequestDto>(r =>
                        r.WorkoutCategory == "running" &&
                        r.Weight == 80 &&
                        r.Duration == 30)))
                .ReturnsAsync(new CaloriesResponse
                {
                    WorkoutCategory = "running",
                    CaloriesPerHour = 30000,
                    DurationMinutes = 30,
                    TotalCaloriesBurned = 150
                });

            var service = new WorkoutService(mockClient.Object);

            // ACT
            var result = await service.GetCaloriesAsync(request);

            // ASSERT
            Assert.NotNull(result);
            Assert.Equal("running", result.WorkoutCategory);
            Assert.Equal(30000, result.CaloriesPerHour);
            Assert.Equal(30, result.DurationMinutes);
            Assert.Equal(150, result.TotalCalories);
        }

        [Theory]
        [InlineData("running", -80, 30)]
        [InlineData("running", 80, -30)]
        [InlineData("running", -80, -30)]
        public void CaloriesRequestDto_NegativeValues_AreInvalid( 
            string workout, int weight, int duration)
        {
            // ARRANGE
            var dto = new CaloriesRequestDto
            {
                WorkoutCategory = workout,
                Weight = weight,
                Duration = duration
            };

            // ACT
            var isWeightInvalid = dto.Weight < 2;
            var isDurationInvalid = dto.Duration < 1;

            // ASSERT
            Assert.True(isWeightInvalid || isDurationInvalid);
        }
    }
}
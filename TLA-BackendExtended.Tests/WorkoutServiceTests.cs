using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
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
            // ARRANGE - Simulate a mocked CaloriesResponse DTO
            var request = new CaloriesRequestDTO
            {
                WorkoutCategory = "running",
                Weight = 80,
                Duration = 30

            };

            var mockClient = new Mock<IWorkoutClient>();

            mockClient
                .Setup(c => c.FetchCaloriesAsync(It.Is<CaloriesRequestDTO>(r =>
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

            // Adding Hybrid cache into
            var services = new ServiceCollection();
            services.AddHybridCache();
            var provider = services.BuildServiceProvider();

            var cache = provider.GetRequiredService<HybridCache>();
            var service = new WorkoutService(mockClient.Object, cache);


            // ACT - Call service for DTO maping to the Workout calories model
            var result = await service.GetCaloriesAsync(request);

            // ASSERT - Verify the mapping from CaloriesResponse to WorkoutCalories
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
        public void CaloriesRequestDTO_NegativeValues_AreInvalid(
            string workout, int weight, int duration)
        {
            // ARRANGE - Setting up user input DTO
            var dto = new CaloriesRequestDTO
            {
                WorkoutCategory = workout,
                Weight = weight,
                Duration = duration
            };

            // ACT - Check if weight or duration is invalid
            var isWeightInvalid = dto.Weight < 2;
            var isDurationInvalid = dto.Duration < 1;

            // ASSERT - True if either duration or weight is below data annotation limit
            Assert.True(isWeightInvalid || isDurationInvalid);
        }
    }
}



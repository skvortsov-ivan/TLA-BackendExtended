using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TLA_BackendExtended.Models;
using Xunit;

namespace TLA_BackendExtended.Tests
{
    public class UserIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public UserIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task DeleteUser_WithAdminAuth_Returns_Ok()
        {
            // ARRANGE - Client with fake Admin authentication
            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "TestScheme";
                        options.DefaultChallengeScheme = "TestScheme";
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", _ => { });
                });
            }).CreateClient();

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("TestScheme");

            // Create user to be deleted
            await client.PostAsJsonAsync("/api/users", new
            {
                Username = "ivan123",
                Password = "ivan123",
                Age = 31,
                Weight = 75,
                Location = "Stockholm",
                DarkMode = true
            });

            // ACT - Delete user
            var response = await client.DeleteAsync("/api/users/ivan123");

            // ASSERT - Ok
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task DeleteUser_WithoutAuth_Returns_Unauthorized()
        {
            // ARRANGE - Endpoint
            var url = "/api/users/ivan123";

            // ACT - Delete attempt
            var response = await _client.DeleteAsync(url);

            // ASSERT - Unauthorized
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

    }
}

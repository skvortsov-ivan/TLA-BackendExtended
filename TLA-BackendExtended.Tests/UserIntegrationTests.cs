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
        public async Task CreateUser_WithMockedAuth_Returns_Ok()
        {

            // ARRANGE - Client with fake authentication
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


            // ACT — Create user
            var createResponse = await client.PostAsJsonAsync("/api/users", new
            {
                username = "ivan123",
                password = "ivan123",
                age = 31,
                weight = 75,
                location = "Stockholm",
                darkMode = true
            });

            // ASSERT — Ok Http Status
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
        }

        [Fact]
        public async Task CreateUser_WithoutAuth_Returns_Unauthorized()
        {
            // ARRANGE - Endpoint
            var url = "/api/users";

            // ACT - Create user
            var response = await _client.PostAsJsonAsync(url, new
            {
                Username = "melvin123",
                Password = "melvin123",
                Age = 24,
                Weight = 75,
                Location = "Stockholm",
                DarkMode = true
            });

            // ASSERT - Unauthorized
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}

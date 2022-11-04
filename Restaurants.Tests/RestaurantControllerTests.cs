using API_Restaurants.Entities;
using API_Restaurants.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Restaurants.Tests
{
    public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _httpClient;

        public RestaurantControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));
                        services.Remove(dbContextOptions);

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));
                    });
                })
                .CreateClient();
        }

        [Theory]
        [InlineData("PageSize=5&PageNumber=4")]
        [InlineData("PageSize=10&PageNumber=3")]
        [InlineData("PageSize=15&PageNumber=2")]
        public async Task GetResponseFromQuery_WithQueryParameters_ReturnsOkResult(string queryParams)
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/api/restaurant/query?" + queryParams);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("PageSize=4&PageNumber=4")]
        [InlineData("PageSize=9&PageNumber=3")]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetResponseFromQuery_WithQueryParameters_ReturnsBadRequest(string queryParams)
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/api/restaurant/query?" + queryParams);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateRestaurant_WithValidModel_ReturnsCreated()
        {
            //Arrange
            var restaurantDto = new CreateRestaurantDto()
            {
                Name = "Test restaurant name",
                City = "Test city",
                Street = "Test street"
            };
            var jsonModel = JsonConvert.SerializeObject(restaurantDto);
            var httpContent = new StringContent(jsonModel, UnicodeEncoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync("/api/restaurant", httpContent);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }
    }
}
using API_Restaurants.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

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
    }
}
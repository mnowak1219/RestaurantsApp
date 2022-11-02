using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Restaurants.Tests
{
    public class RestaurantControllerTests
    {
        private HttpClient _httpClient;

        public RestaurantControllerTests()
        {
            var factory = new WebApplicationFactory<Program>();
            _httpClient = factory.CreateClient();
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

            //httpClient.BaseAddress = new Uri("https://localhost:5001");

            //Act
            var response = await _httpClient.GetAsync("/api/restaurant/query?" + queryParams);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}
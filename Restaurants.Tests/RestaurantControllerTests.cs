using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Restaurants.Tests
{
    public class RestaurantControllerTests
    {
        [Fact]
        public async Task GetResponseFromQuery_WithQueryParameters_ReturnsOkResult()
        {
            //Arrange
            var factory = new WebApplicationFactory<Program>();
            var httpClient = factory.CreateClient();
            //httpClient.BaseAddress = new Uri("https://localhost:5001/");

            //Act
            var response = await httpClient.GetAsync("/api/restaurant/query?PageSize=5&PageNumber=1");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
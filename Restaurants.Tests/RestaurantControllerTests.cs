using API_Restaurants.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Net.Http;

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

            //Act
            var response = await httpClient.GetAsync("/api/restaurant/query?PageSize=5&PageNumber=1");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
        //[Fact]
        //public async Task HelloWorld()
        //{
        //    //Arrange
        //    var application = new WebApplicationFactory<Program>()
        //        .WithWebHostBuilder(builder =>
        //        {
        //            builder.ConfigureServices(services =>
        //            {
        //                services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer("Server=PDCWIN11;Database=RestaurantDb;Trusted_Connection=True;"));
        //            });
        //        });
        //    var httpClient = application.CreateClient();

        //    //Act
        //    var response = await httpClient.GetAsync("/api/restaurant/query?PageSize=5&PageNumber=1");

        //    //Assert
        //    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        //}
    }
}
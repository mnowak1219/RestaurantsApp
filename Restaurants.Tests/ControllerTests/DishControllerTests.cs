using API_Restaurants.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Restaurants.Tests.Fakes;
using Restaurants.Tests.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Tests.ControllerTests
{
    public class DishControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;

        public DishControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));
                        services.Remove(dbContextOptions);

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb_ForDishController_1"));
                    });
                });
            _httpClient = _factory.CreateClient();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(9)]
        [InlineData(12)]
        [InlineData(27)]
        [InlineData(44)]
        public async Task GetAllDishes_ForValidData_ReturnsOK(int restaurantId)
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/api/restaurant/" + restaurantId + "/dish");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-9)]
        [InlineData(512)]
        [InlineData(-827)]
        [InlineData(944)]
        public async Task GetAllDishes_ForInValidData_ReturnsNotFound(int restaurantId)
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/api/restaurant/" + restaurantId + "/dish");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Theory]
        [InlineData(1, 182)]
        [InlineData(2, 1578)]
        [InlineData(9, 1149)]
        [InlineData(33, 459)]
        [InlineData(44, 2456)]
        public async Task GetOneRestaurantDish_ForValidData_ReturnsOK(int restaurantId, int dishId)
        {
            //Arrange
            var factory = new WebApplicationFactory<Program>();
            factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));
                        services.Remove(dbContextOptions);

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb_ForDishController_2"));
                    });
                });
            var httpClient = factory.CreateClient();
            //Act
            var response = await httpClient.GetAsync($"/api/restaurant/{restaurantId}/dish/{dishId}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(2, 0)]
        [InlineData(9, 55)]
        [InlineData(33, 99)]
        [InlineData(44, -33)]
        public async Task GetOneRestaurantDish_ForInValidData_ReturnsNotFound(int restaurantId, int dishId)
        {
            //Arrange
            var factory = new WebApplicationFactory<Program>();
            factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));
                        services.Remove(dbContextOptions);

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb_ForDishController_3"));
                    });
                });
            var httpClient = factory.CreateClient();
            //Act
            var response = await httpClient.GetAsync($"/api/restaurant/{restaurantId}/dish/{dishId}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}

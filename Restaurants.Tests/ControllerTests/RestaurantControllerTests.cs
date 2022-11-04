using API_Restaurants.Entities;
using API_Restaurants.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Restaurants.Tests.Fakes;
using Restaurants.Tests.Filters;
using Restaurants.Tests.Helpers;

namespace Restaurants.Tests.ControllerTests
{
    public class RestaurantControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;
        
        private void SeedRestaurant(Restaurant restaurant)
        {
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<RestaurantDbContext>();
            _dbContext.Add(restaurant);
            _dbContext.SaveChanges();
        }

        public RestaurantControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));
                        services.Remove(dbContextOptions);

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb_ForRestaurantController"));
                        services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                        services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));
                    });
                });
            _httpClient = _factory.CreateClient();
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

        public static IEnumerable<object[]> GetRestaurantValidModels()
        {
            yield return new object[]
            {
                new CreateRestaurantDto()
                {
                    Name = "Test restaurant name",
                    City = "Test city",
                    Street = "Test street"
                },
            };
            yield return new object[]
            {
                new CreateRestaurantDto()
                {
                    Name = "Test restaurant name",
                    City = "Test city name",
                    Street = "Test street",
                    ContactEmail = "testc@mail.pl",
                },
            };

        }
        [Theory]
        [MemberData(nameof(GetRestaurantValidModels))]
        public async Task CreateRestaurant_WithValidModel_ReturnsCreated(CreateRestaurantDto restaurantDto)
        {
            //Arrange
            var httpContent = restaurantDto.ToJsonHttpContent();

            //Act
            var response = await _httpClient.PostAsync("/api/restaurant", httpContent);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        public static IEnumerable<object[]> GetRestaurantInvalidModels()
        {
            yield return new object[]
            {
                new CreateRestaurantDto()
                {
                    Name = "Test very long restaurant name",
                    City = "Test city",
                    Street = "Test street"
                },
            };
            yield return new object[]
            {
                new CreateRestaurantDto()
                {
                    Name = "Test restaurant name",
                    City = "Test city",
                    Street = "Test the longest street name in the Solar System and the Galaxy"
                },
            };
            yield return new object[]
            {
                new CreateRestaurantDto(),
            };
            yield return new object[]
            {
                new CreateRestaurantDto()
                {
                    ContactEmail = "Test@mail.com",
                    City = "Test city",
                    Street = "Test street name"
                },
            };
        }
        [Theory]
        [MemberData(nameof(GetRestaurantInvalidModels))]
        public async Task CreateRestaurant_WithInvalidModel_ReturnsBadRequest(CreateRestaurantDto restaurantDto)
        {
            //Arrange
            var httpContent = restaurantDto.ToJsonHttpContent();

            //Act
            var response = await _httpClient.PostAsync("/api/restaurant", httpContent);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteRestaurant_ForNonExistingRestaurant_ReturnsNotFound()
        {
            //Arrange
            var id = 665;

            //Act
            var response = await _httpClient.DeleteAsync($"api/restaurant/{id}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
            
        [Fact]
        public async Task DeleteRestaurant_ForRestaurantOwner_ReturnsNoContent()
        {
            //Arrange
            var restaurant = new Restaurant()
            {
                CreatedById = 1,
                Name = "Best Onion Rings",
            };
            SeedRestaurant(restaurant);

            //Act
            var response = await _httpClient.DeleteAsync($"api/restaurant/{restaurant.Id}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteRestaurant_ForNotRestaurantOwner_ReturnsForbidden()
        {
            //Arrange
            var restaurant = new Restaurant()
            {
                CreatedById = 3,
                Name = "Good Food",
            };
            SeedRestaurant(restaurant);

            //Act
            var response = await _httpClient.DeleteAsync($"api/restaurant/{restaurant.Id}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }
    }
}
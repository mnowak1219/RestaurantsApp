using API_Restaurants.Entities;
using API_Restaurants.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurants.Tests.Fakes;
using Restaurants.Tests.Filters;
using Restaurants.Tests.Helpers;
using System.Text;

namespace Restaurants.Tests
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

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb"));
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
            yield return new object[]
            {
                new CreateRestaurantDto()
                {
                    Name = "Test restaurant name",
                    ContactNumber = "123123123",
                    City = "Test city",
                    Street = "Test street name",
                    PostalCode = "12-123",
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
                    City = "Test the longest city name in the Solar System and the Galaxy",
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
            yield return new object[]
            {
                new CreateRestaurantDto()
                {
                    Name = "Test restaurant name",
                    Street = "Test street"
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

        [Theory]
        [InlineData(766)]
        [InlineData(994)]
        [InlineData(655)]
        public async Task DeleteRestaurant_ForNonExistingRestaurant_ReturnsNotFound(int id)
        {
            //Arrange

            //Act
            var response = await _httpClient.DeleteAsync($"api/restaurant/{id}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        public static IEnumerable<object[]> GetRestaurantsWithCorrectOwner()
        {
            yield return new object[]
            {
                new Restaurant()
                {
                    CreatedById = 1,
                    Name = "Test",
                },
            };
            yield return new object[]
            {
                new Restaurant()
                {
                    CreatedById = 1,
                    Name = "Other test",
                },
            };
        }
        [Theory]
        [MemberData(nameof(GetRestaurantsWithCorrectOwner))]
        public async Task DeleteRestaurant_ForRestaurantOwner_ReturnsNoContent(Restaurant restaurant)
        {
            //Arrange
            SeedRestaurant(restaurant);

            //Act
            var response = await _httpClient.DeleteAsync($"api/restaurant/{restaurant.Id}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        public static IEnumerable<object[]> GetRestaurantsWithWrongOwner()
        {
            yield return new object[]
            {
                new Restaurant()
                {
                    CreatedById = 3,
                    Name = "Nice chips",
                },
            };
            yield return new object[]
            {
                new Restaurant()
                {
                    CreatedById = 8,
                    Name = "Good Food",
                },
            };
        }
        [Theory]
        [MemberData(nameof(GetRestaurantsWithWrongOwner))]
        public async Task DeleteRestaurant_ForNotRestaurantOwner_ReturnsForbidden(Restaurant restaurant)
        {
            //Arrange
            SeedRestaurant(restaurant);

            //Act
            var response = await _httpClient.DeleteAsync($"api/restaurant/{restaurant.Id}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Forbidden);
        }
    }
}
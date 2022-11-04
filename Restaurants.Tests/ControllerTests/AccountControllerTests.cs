using API_Restaurants.Entities;
using API_Restaurants.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Restaurants.Tests.Helpers;

namespace Restaurants.Tests.ControllerTests
{
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _httpClient;

        public AccountControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));
                        services.Remove(dbContextOptions);

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase("RestaurantDb_ForAccountController"));
                    });
                }).CreateClient();
        }

        [Fact]
        public async Task RegisterUser_ForValidModel_ReturnsOk()
        {
            //Arrange
            var registerUserDto = new RegisterUserDto()
            {
                Email = "test@mail.pl",
                Password = "Password123",
                ConfirmPassword = "Password123",
            };
            var httpContent = registerUserDto.ToJsonHttpContent();

            //Act
            var response = await _httpClient.PostAsync("api/account/register", httpContent);

            //Arrange
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task RegisterUser_ForInvalidModel_BadRequest()
        {
            //Arrange
            var registerUserDto = new RegisterUserDto()
            {
                Password = "Password1234",
                ConfirmPassword = "Password123",
            };
            var httpContent = registerUserDto.ToJsonHttpContent();

            //Act
            var response = await _httpClient.PostAsync("api/account/register", httpContent);

            //Arrange
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }
    }
}

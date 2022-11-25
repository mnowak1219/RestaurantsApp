namespace Restaurants.Tests.ControllerTests
{
    public class AccountControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private HttpClient _httpClient;
        private Mock<IAccountService> _accountServiceMock = new Mock<IAccountService>();

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
                        services.AddSingleton(_accountServiceMock.Object);
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

            //Assert
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

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_ForRegisteredUser_ReturnsOk()
        {
            //Arrange
            _accountServiceMock.Setup(m => m.SGenerateJwtToken(It.IsAny<LoginUserDto>()))
            .Returns("token jwt");
            var loginDto = new LoginUserDto()
            {
                Email = "test@mail.pl",
                Password = "Password123",
            };
            var httpContent = loginDto.ToJsonHttpContent();

            //Act
            var response = await _httpClient.PostAsync("api/account/login", httpContent);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Content.ReadAsStringAsync().Result.Should().NotBeNullOrEmpty();
        }
    }
}
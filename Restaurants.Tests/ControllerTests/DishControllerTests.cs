namespace Restaurants.Tests.ControllerTests
{
    public class DishControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _httpClient;
        private RestaurantDbContext _dbContext;

        internal void SeedDishes()
        {
            var testDishes = new List<Dish>()
            {
                new Dish()
                {
                    Name = "test dish 1",
                    RestaurantId = 1,
                },
                new Dish()
                {
                    Name = "test dish 2",
                    RestaurantId = 1,
                },
                new Dish()
                {
                    Name = "test dish 3",
                    RestaurantId = 1,
                },
                new Dish()
                {
                    Name = "test dish 4",
                    RestaurantId = 1,
                },
                new Dish()
                {
                    Name = "test dish 1",
                    RestaurantId = 2,
                },
                new Dish()
                {
                    Name = "test dish 2",
                    RestaurantId = 2,
                },
                new Dish()
                {
                    Name = "test dish 3",
                    RestaurantId = 2,
                },
                new Dish()
                {
                    Name = "test dish 4",
                    RestaurantId = 2,
                },
            };
            _dbContext.AddRange(testDishes);
            _dbContext.SaveChanges();
        }

        internal HttpClient CreateHttpClient(string useInMemoryDatabaseName)
        {
            var factory = new WebApplicationFactory<Program>();
            _factory = factory
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<RestaurantDbContext>));
                        services.Remove(dbContextOptions);

                        services.AddDbContext<RestaurantDbContext>(options => options.UseInMemoryDatabase(useInMemoryDatabaseName));
                    });
                });
            return _factory.CreateClient();
        }

        public DishControllerTests(WebApplicationFactory<Program> factory)
        {
            _httpClient = CreateHttpClient("RestaurantDb_ForDishController_1");
            _dbContext = _factory.Services.GetService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetService<RestaurantDbContext>();
            SeedDishes();
        }

        [Theory]
        [ClassData(typeof(GetAllDishes_WithValidModel200))]
        public async Task GetAllDishes_WithValidModel_ReturnsOK(int restaurantId)
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/api/restaurant/" + restaurantId + "/dish");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [ClassData(typeof(GetAllDishes_WithInvalidModel404))]
        public async Task GetAllDishes_WithInvalidModel_ReturnsNotFound(int restaurantId)
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync("/api/restaurant/" + restaurantId + "/dish");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(GetOneRestaurantDish_WithValidModel200))]
        public async Task GetOneRestaurantDish_WithValidModel_ReturnsOK(int restaurantId, int dishId)
        {
            //Arrange
            _httpClient = CreateHttpClient("RestaurantDb_ForDishController_2");

            //Act
            var response = await _httpClient.GetAsync($"/api/restaurant/{restaurantId}/dish/{dishId}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [ClassData(typeof(GetOneRestaurantDish_WithInvalidModel404))]
        public async Task GetOneRestaurantDish_WithInvalidModel_ReturnsNotFound(int restaurantId, int dishId)
        {
            //Arrange

            //Act
            var response = await _httpClient.GetAsync($"/api/restaurant/{restaurantId}/dish/{dishId}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(CreateRestaurantDish_WithValidModel201))]
        public async Task CreateRestaurantDish_WithValidModel_ReturnsCreated(int restaurantId, CreateDishDto dto)
        {
            //Arrange
            var model = dto.ToJsonHttpContent();

            //Act
            var response = await _httpClient.PostAsync($"/api/restaurant/{restaurantId}/dish", model);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Theory]
        [ClassData(typeof(CreateRestaurantDish_WithInvalidModel401))]
        public async Task CreateRestaurantDish_WithInvalidModel_ReturnsBadRequest(int restaurantId, CreateDishDto dto)
        {
            //Arrange
            var model = dto.ToJsonHttpContent();

            //Act
            var response = await _httpClient.PostAsync($"/api/restaurant/{restaurantId}/dish", model);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Theory]
        [ClassData(typeof(CreateRestaurantDish_WithInvalidModel404))]
        public async Task CreateRestaurantDish_WithInvalidModel_ReturnsNotFound(int restaurantId, CreateDishDto dto)
        {
            //Arrange
            var model = dto.ToJsonHttpContent();

            //Act
            var response = await _httpClient.PostAsync($"/api/restaurant/{restaurantId}/dish", model);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(DeleteAllRestaurantDishes_WithValidModel204))]
        public async Task DeleteAllRestaurantDishes_WithValidModel_ReturnsNoContent(int restaurantId)
        {
            //Arrange

            //Act
            var response = await _httpClient.DeleteAsync($"/api/restaurant/{restaurantId}/dish");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(DeleteAllRestaurantDishes_WithInvalidModel404))]
        public async Task DeleteAllRestaurantDishes_WithInvalidModel_ReturnsNoContent(int restaurantId)
        {
            //Arrange

            //Act
            var response = await _httpClient.DeleteAsync($"/api/restaurant/{restaurantId}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(DeleteOneRestaurantDish_WithValidModel204))]
        public async Task DeleteOneRestaurantDish_WithValidModel_ReturnsNoContent(int restaurantId, int dishId)
        {
            //Arrange

            //Act
            var response = await _httpClient.DeleteAsync($"/api/restaurant/{restaurantId}/dish/{dishId}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        }

        [Theory]
        [ClassData(typeof(DeleteOneRestaurantDish_WithInvalidModel404))]
        public async Task DeleteOneRestaurantDish_WithInvalidModel_ReturnsNotFound(int restaurantId, int dishId)
        {
            //Arrange

            //Act
            var response = await _httpClient.DeleteAsync($"/api/restaurant/{restaurantId}/dish/{dishId}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
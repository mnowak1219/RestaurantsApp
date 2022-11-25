namespace Restaurants.Tests.CommonTests
{
    public class DependencyInjectionTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private List<Type> _controllerTypes;
        private WebApplicationFactory<Program> _factory;

        public DependencyInjectionTests(WebApplicationFactory<Program> factory)
        {
            _controllerTypes = typeof(Program)
                .Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ControllerBase)))
                .ToList();

            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    _controllerTypes.ForEach(c => services.AddScoped(c));
                });
            });
        }

        [Fact]
        public void ConfigureServices_ForControllers_RegisterAllDependencies()
        {
            //Arrange
            var scopeFactory = _factory.Services.GetService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();

            //Act
            _controllerTypes.ForEach(controllerType =>
            {
                var controller = scope.ServiceProvider.GetService(controllerType);

                //Assert
                controller.Should().NotBeNull();
            });
        }
    }
}
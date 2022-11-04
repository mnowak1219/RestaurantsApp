using API_Restaurants.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Tests.ControllerTests
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
            var controller = scope.ServiceProvider.GetService<AccountController>();

            //Assert
            _controllerTypes.ForEach(t =>
            {
                var controller = scope.ServiceProvider.GetService(t);
                controller.Should().NotBeNull();
            });
    }
}
}

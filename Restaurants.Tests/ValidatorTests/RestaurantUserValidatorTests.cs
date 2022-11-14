using API_Restaurants.Entities;
using API_Restaurants.Models;
using API_Restaurants.Models.Validators;
using FluentValidation.TestHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurants.Tests.ValidatorTests.Data;

namespace Restaurants.Tests.ValidatorTests
{
    public class RestaurantUserValidatorTests
    {
        private RestaurantDbContext _dbContext;

        internal void Seed()
        {
            var testUsers = new List<User>()
            {
                new User()
                {
                    Email = "test2@test.com"
                },
                new User()
                {
                    Email = "test3@test.com"
                }
            };
            _dbContext.AddRange(testUsers);
            _dbContext.SaveChanges();
        }

        public RestaurantUserValidatorTests()
        {
            var builder = new DbContextOptionsBuilder<RestaurantDbContext>();
            builder.UseInMemoryDatabase("RestaurantDB_ForValidators");
            _dbContext = new RestaurantDbContext(builder.Options);
            Seed();
        }

        [Theory]
        [ClassData(typeof(RestaurantUserValidatorTestsValidData))]
        public void Validate_ForValidModel_ReturnsSuccess(RegisterUserDto model)
        {
            //Arrange
            var validator = new RegisterUserDtoValidator(_dbContext);

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [ClassData(typeof(RestaurantUserValidatorTestsInvalidData))]
        public void Validate_ForInValidModel_ReturnsFailure(RegisterUserDto model)
        {
            //Arrange
            var validator = new RegisterUserDtoValidator(_dbContext);

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}

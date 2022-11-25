namespace Restaurants.Tests.ValidatorTests
{
    public class RestaurantQueryValidatorTests
    {
        public static IEnumerable<object[]> GetSampleValidData()
        {
            var list = new List<RestaurantQuery>()
            {
                new RestaurantQuery()
                {
                    PageNumber = 2,
                    PageSize = 10,
                },
                new RestaurantQuery()
                {
                    PageNumber = 3,
                    PageSize = 5,
                },
                new RestaurantQuery()
                {
                    PageNumber = 1,
                    PageSize = 15,
                },
                new RestaurantQuery()
                {
                    PageNumber = 11,
                    PageSize = 5,
                    SortBy = nameof(Restaurant.Name),
                },
                new RestaurantQuery()
                {
                    PageNumber = 13,
                    PageSize = 10,
                    SortBy = nameof(Restaurant.Category),
                }
            };
            return list.Select(r => new object[] { r });
        }

        [Theory]
        [MemberData(nameof(GetSampleValidData))]
        public void Validate_ForCorrectModel_ReturnsSuccess(RestaurantQuery model)
        {
            //Arrange
            var validator = new RestaurantQueryValidator();

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        public static IEnumerable<object[]> GetSampleInvalidData()
        {
            var list = new List<RestaurantQuery>()
            {
                new RestaurantQuery()
                {
                    PageNumber = 2,
                    PageSize = 11,
                },
                new RestaurantQuery()
                {
                    PageNumber = 0,
                    PageSize = 15,
                },
                new RestaurantQuery()
                {
                    PageNumber = 3,
                    PageSize = 0,
                },
                new RestaurantQuery()
                {
                    PageNumber = 11,
                    PageSize = 5,
                    SortBy = nameof(Restaurant.Address),
                },
                new RestaurantQuery()
                {
                    PageNumber = 13,
                    PageSize = 10,
                    SortBy = nameof(Restaurant.ContactEmail),
                }
            };
            return list.Select(r => new object[] { r });
        }

        [Theory]
        [MemberData(nameof(GetSampleInvalidData))]
        public void Validate_ForIncorrectModel_ReturnsFailure(RestaurantQuery model)
        {
            //Arrange
            var validator = new RestaurantQueryValidator();

            //Act
            var result = validator.TestValidate(model);

            //Assert
            result.ShouldHaveAnyValidationError();
        }
    }
}
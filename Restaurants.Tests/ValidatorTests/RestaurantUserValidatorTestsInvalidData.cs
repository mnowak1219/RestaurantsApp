using API_Restaurants.Models;
using System.Collections;

namespace Restaurants.Tests.ValidatorTests
{
    public class RestaurantUserValidatorTestsInvalidData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<RegisterUserDto>()
            {
                new RegisterUserDto()
                {
                    Email = "test2@test.com",
                    Password = "Password123",
                    ConfirmPassword = "Password123",
                },
                new RegisterUserDto()
                {
                    Email = "test5@test.com",
                    Password = "Password123a",
                    ConfirmPassword = "Password123",
                },
                new RegisterUserDto()
                {
                    Email = "test5@test.com",
                    Password = "Password123",
                    ConfirmPassword = "Password123n",
                },
                new RegisterUserDto()
                {
                    Email = "test5@test.com",
                    Password = "Password123n",
                },
                new RegisterUserDto()
                {
                    Password = "test5@test.com",
                    ConfirmPassword = "Password123n",
                },
                new RegisterUserDto()
                {
                    Email = "test5@test.com",
                },
            };
            return objectList.Select(o => new object[] { o }).GetEnumerator();

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

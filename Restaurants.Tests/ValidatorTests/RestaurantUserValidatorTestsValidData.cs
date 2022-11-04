using API_Restaurants.Models;
using System.Collections;

namespace Restaurants.Tests.ValidatorTests
{
    public class RestaurantUserValidatorTestsValidData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<RegisterUserDto>()
            {
                new RegisterUserDto()
                {
                    Email = "test@test.com",
                    Password = "Password123",
                    ConfirmPassword = "Password123",
                },
                new RegisterUserDto()
                {
                    Email = "tes!t@test.com",
                    Password = "Password123",
                    ConfirmPassword = "Password123",
                },
                new RegisterUserDto()
                {
                    Email = "test@test.com",
                    Password = "password123",
                    ConfirmPassword = "password123",
                },
                new RegisterUserDto()
                {
                    Email = "test@tes12t.com",
                    Password = "Password123",
                    ConfirmPassword = "Password123",
                },
                new RegisterUserDto()
                {
                    Email = "test@test.com",
                    Password = "Password123",
                    ConfirmPassword = "Password123",
                    Nationality = "Poland",
                },
            };
            return objectList.Select(o => new object[] { o }).GetEnumerator();

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

using API_Restaurants.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Tests.ControllerTests.Data
{
    public class CreateRestaurantDish_WithValidModel201 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<object[]>()
            {
                new object[]
                {
                    4,
                    new CreateDishDto()
                    {
                        Name = "Fried pork",
                    },
                },
                new object[]
                {
                    14,
                    new CreateDishDto()
                    {
                        Name = "Wine",
                        RestaurantId = 77,
                    },
                },
                new object[]
                {
                    49,
                    new CreateDishDto()
                    {
                        Name = "Chick-Chicken",
                        Description = "Meal for ladies.",
                    },
                },
                new object[]
                {
                    149,
                    new CreateDishDto()
                    {
                        Name = "Loly-Chicken",
                        Price = 14.22M,
                    },
                },
                new object[]
                {
                    89,
                    new CreateDishDto()
                    {
                        Name = "Candy",
                        Description = "Sweet meal",
                        RestaurantId = 21,
                    },
                },
                new object[]
                {
                    189,
                    new CreateDishDto()
                    {
                        Name = "Dog slice",
                        Description = "Some good things.",
                        Price = 21,
                    },
                },
                new object[]
                {
                    56,
                    new CreateDishDto()
                    {
                        Name = "Some of my chicken",
                        RestaurantId = 22,
                        Price = 21,
                    },
                },
                new object[]
                {
                    56,
                    new CreateDishDto()
                    {
                        Name = "Muuuuuu",
                        Description = "beef is so tasty",
                        RestaurantId = 22,
                        Price = 21,
                    },
                },
            };
            return objectList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

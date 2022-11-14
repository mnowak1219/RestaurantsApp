using API_Restaurants.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Tests.ControllerTests.Data
{
    public class CreateRestaurantDish_WithInvalidModel401 : IEnumerable<object[]>
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
                    },
                },
                new object[]
                {
                    14,
                    new CreateDishDto()
                    {
                        RestaurantId = 77,
                    },
                },
                new object[]
                {
                    49,
                    new CreateDishDto()
                    {
                        Description = "Meal for ladies.",
                    },
                },
                new object[]
                {
                    149,
                    new CreateDishDto()
                    {
                        Price = 14.22M,
                    },
                },
                new object[]
                {
                    89,
                    new CreateDishDto()
                    {
                        Description = "Sweet meal",
                        RestaurantId = 21,
                    },
                },
                new object[]
                {
                    189,
                    new CreateDishDto()
                    {
                        Description = "Some good things.",
                        Price = 21,
                    },
                },
                new object[]
                {
                    56,
                    new CreateDishDto()
                    {
                        RestaurantId = 22,
                        Price = 21,
                    },
                },
                new object[]
                {
                    56,
                    new CreateDishDto()
                    {
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Tests.ControllerTests.Data
{
    public class GetOneRestaurantDish_WithInvalidModel404 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<object[]>()
            {
                new object[] {1, -1 },
                new object[] {2, 0 },
                new object[] {9, 55 },
                new object[] {33, 99 },
                new object[] {44, -33 },
            };
            return objectList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Tests.ControllerTests.Data
{
    public class DeleteOneRestaurantDish_WithValidModel204 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<object[]>()
            {
                new object[] {1, 2503 },
                new object[] {1, 2501 },
                new object[] {1, 2502 },
                new object[] {2, 2506 },
                new object[] {2, 2507 },
            };
            return objectList.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

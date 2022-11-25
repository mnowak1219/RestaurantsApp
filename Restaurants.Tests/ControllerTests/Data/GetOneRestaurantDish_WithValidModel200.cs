namespace Restaurants.Tests.ControllerTests.Data
{
    public class GetOneRestaurantDish_WithValidModel200 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<object[]>()
            {
                new object[] {1, 182 },
                new object[] {2, 1578 },
                new object[] {9, 1149 },
                new object[] {33, 459 },
                new object[] {44, 2456 },
            };
            return objectList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
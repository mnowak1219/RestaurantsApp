namespace Restaurants.Tests.ControllerTests.Data
{
    public class DeleteAllRestaurantDishes_WithInvalidModel404 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<int>()
            {
                0,
                -9,
                1512,
                -827,
                2944,
            };
            return objectList.Select(o => new object[] { o }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
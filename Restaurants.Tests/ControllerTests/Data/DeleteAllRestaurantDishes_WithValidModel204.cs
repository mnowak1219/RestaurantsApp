namespace Restaurants.Tests.ControllerTests.Data
{
    public class DeleteAllRestaurantDishes_WithValidModel204 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<int>()
            {
                2,
                5,
                17,
                22,
                88,
            };
            return objectList.Select(o => new object[] { o }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
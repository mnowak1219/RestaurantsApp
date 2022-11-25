namespace Restaurants.Tests.ControllerTests.Data
{
    public class GetAllDishes_WithInvalidModel404 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<int>()
            {
                0,
                -9,
                512,
                -827,
                944,
            };
            return objectList.Select(o => new object[] { o }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
﻿namespace Restaurants.Tests.ControllerTests.Data
{
    public class GetAllDishes_WithValidModel200 : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var objectList = new List<int>()
            {
                2,
                9,
                12,
                27,
                44,
            };
            return objectList.Select(o => new object[] { o }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
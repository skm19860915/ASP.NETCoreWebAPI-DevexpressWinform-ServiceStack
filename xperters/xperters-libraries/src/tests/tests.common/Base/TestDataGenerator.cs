using System.Collections;
using System.Collections.Generic;

namespace xperters.tests.common.Base
{
    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data;

        public TestDataGenerator(List<object[]> items)
        {
            _data = items;
        }


        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
using System;
using VirtualScrollView.data;

namespace VirtualScrollView.test.data
{
    public class TestDataProvider : IDataProvider<TestData>
    {
        public int totalItems { get; private set; }

        private TestData[] _items;

        public void SetItems(TestData[] items)
        {
            if (items == null || items.Length == 0) throw new Exception("Invalid data or empty data set.");

            _items = items;
            totalItems = _items.Length;
        }

        public TestData GetItem(int index)
        {
            if (index < 0 || index >= totalItems) throw new IndexOutOfRangeException("Index out of items range.");

            return _items[index];
        }
    }
}
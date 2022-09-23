using UnityEngine;
using VirtualScrollView.data;
using VirtualScrollView.test.data;

namespace VirtualScrollView.test
{
    public class VirtualListTest : MonoBehaviour
    {

        public VirtualList listVertical;
        public VirtualList listHorizontal;

        private IDataProvider<TestData> _dataProvider;

        private void Awake()
        {
            // Create test data
            TestData[] testData = new TestData[100];
            for (int i = 0; i < 100; i++)
                testData[i] = new TestData { text = i.ToString() };

            // Create data provider
            _dataProvider = new TestDataProvider();
            _dataProvider.SetItems(testData);

            // Add cell data listener
            listVertical.onSetCellData += OnSetCellData;
            listHorizontal.onSetCellData += OnSetCellData;
            // Init list
            listVertical.InitList(_dataProvider.totalItems);
            listHorizontal.InitList(_dataProvider.totalItems);
        }

        private void OnSetCellData(ICell cell, int index)
        {
            IData data = _dataProvider.GetItem(index);
            cell.SetData(data);
        }
    }
}
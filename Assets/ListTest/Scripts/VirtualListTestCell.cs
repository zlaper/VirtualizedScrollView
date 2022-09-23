using TMPro;
using UnityEngine;
using VirtualScrollView.data;
using VirtualScrollView.test.data;

namespace VirtualScrollView.test
{
    public class VirtualListTestCell : MonoBehaviour, ICell
    {
        [SerializeField] private TextMeshProUGUI label;

        public void SetData(IData data)
        {
            TestData test = data as TestData;
            label.text = test.text;
        }
    }
}
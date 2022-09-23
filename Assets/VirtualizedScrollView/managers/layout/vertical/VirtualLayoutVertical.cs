using UnityEngine;
using VirtualScrollView.data;

namespace VirtualScrollView.managers.layout.vertical
{
    public class VirtualLayoutVertical : VirtualLayoutManager
    {
        public VirtualLayoutVertical(VirtualList list, VirtualCellManager cellManager) : base(list, cellManager)
        {
        }

        protected override void SetCellPosition(ICell cell, int index)
        {
            float yPosition = index * list.cellSize.y;

            RectTransform rTrans = cell.transform as RectTransform;
            // Set anchor
            rTrans.anchorMin = new Vector2(0, 1);
            rTrans.anchorMax = new Vector2(1, 1);
            // Set pivot
            rTrans.pivot = new Vector2(0, 1);
            // Set offset
            rTrans.offsetMin = new Vector2(0, rTrans.offsetMin.y);
            rTrans.offsetMax = new Vector2(0, rTrans.offsetMax.y);
            // Set position
            rTrans.anchoredPosition = new Vector2(0, -yPosition);
        }
    }
}
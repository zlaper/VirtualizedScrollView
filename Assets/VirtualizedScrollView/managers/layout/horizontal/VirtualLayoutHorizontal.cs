using UnityEngine;
using VirtualScrollView.data;

namespace VirtualScrollView.managers.layout.horizontal
{
    public class VirtualLayoutHorizontal : VirtualLayoutManager
    {
        public VirtualLayoutHorizontal(VirtualList list, VirtualCellManager cellManager) : base(list, cellManager)
        {
        }

        protected override void SetCellPosition(ICell cell, int index)
        {
            float xPosition = index * list.cellSize.x;

            RectTransform rTrans = cell.transform as RectTransform;
            // Set anchor
            rTrans.anchorMin = Vector2.zero; 
            rTrans.anchorMax = new Vector2(0,1);
            // Set pivot
            rTrans.pivot = Vector2.zero; 
            // Set offset
            rTrans.offsetMin = new Vector2(rTrans.offsetMin.x, 0);
            rTrans.offsetMax = new Vector2(rTrans.offsetMax.x, 0);
            // Set position
            rTrans.anchoredPosition = new Vector2(xPosition, 0);
        }
    }
}
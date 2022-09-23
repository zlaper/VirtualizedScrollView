using UnityEngine;

namespace VirtualScrollView.managers.viewport.horizontal
{
    public class VirtualViewportHorizontal : VirtualViewport
    {
        public VirtualViewportHorizontal(VirtualList list) : base(list)
        {
            // Set list to horizontal
            list.horizontal = true;
            list.vertical = false;

            // Set anchors
            list.content.anchorMin = Vector2.zero;
            list.content.anchorMax = new Vector2(0, 1);

            // Cache viewport width
            viewportSize = list.viewport.rect.width;
        }

        public override void InitContent(int totalItems)
        {
            currentTotalItems = totalItems;

            // Calculate size
            cellSize = list.cellSize.x; // Cache cell width
            contentSize = cellSize * currentTotalItems; // Set content width to total items width

            // Set size - We subtract the padding so that the last item snaps to the right of the list
            list.content.sizeDelta = new Vector2(contentSize - list.settings.padding, list.viewport.sizeDelta.y);

            // Force update cells
            OnScrollContent();
        }

        protected override void OnScrollContent()
        {
            // Get current values
            float xMin = -list.content.anchoredPosition.x; // Get current scroll position
            float xMax = xMin + viewportSize; // Find max x

            // Limits
            if (xMin < 0) xMin = 0; // Cap x position to 0
            if (xMax > contentSize) xMax = contentSize; // Cap max x to content width

            // Find indexes
            int firstIndex = Mathf.FloorToInt(xMin / cellSize);
            int lastIndex = Mathf.CeilToInt(xMax / cellSize);

            // Limits
            firstIndex = Mathf.Clamp(firstIndex, 0, currentTotalItems);
            lastIndex = Mathf.Clamp(lastIndex, 0, currentTotalItems);

            // Inform listeners
            onChangeIndex?.Invoke(firstIndex, lastIndex);
        }
    }
}
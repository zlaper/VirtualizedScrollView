using UnityEngine;

namespace VirtualScrollView.managers.viewport.vertical
{
    public class VirtualViewportVertical : VirtualViewport
    {
        public VirtualViewportVertical(VirtualList list) : base(list)
        {
            // Set list to vertical
            list.horizontal = false;
            list.vertical = true;

            // Set anchors
            list.content.anchorMin = new Vector2(0, 1);
            list.content.anchorMax = Vector2.one;

            // Cache viewport height
            viewportSize = list.viewport.rect.height;
        }

        public override void InitContent(int totalItems)
        {
            currentTotalItems = totalItems;

            // Calculate size
            cellSize = list.cellSize.y; // Cache cell height
            contentSize = cellSize * currentTotalItems; // Set content height to total items height

            // Set size - We subtract the padding so that the last item snaps to the bottom of the list
            list.content.sizeDelta = new Vector2(list.viewport.sizeDelta.x, contentSize - list.settings.padding);

            // Force update cells
            OnScrollContent();
        }

        protected override void OnScrollContent()
        {
            // Get current values
            float yMin = list.content.anchoredPosition.y; // Get current scroll position
            float yMax = yMin + viewportSize; // Find max y

            // Limits
            if (yMin < 0) yMin = 0; // Cap y position to 0
            if (yMax > contentSize) yMax = contentSize; // Cap max y to content height

            // Find indexes
            int firstIndex = Mathf.FloorToInt(yMin / cellSize);
            int lastIndex = Mathf.CeilToInt(yMax / cellSize);

            // Limits
            firstIndex = Mathf.Clamp(firstIndex, 0, currentTotalItems);
            lastIndex = Mathf.Clamp(lastIndex, 0, currentTotalItems);

            // Inform listeners
            onChangeIndex?.Invoke(firstIndex, lastIndex);
        }
    }
}
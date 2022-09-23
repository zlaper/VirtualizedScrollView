using UnityEngine;
using UnityEngine.UI;
using VirtualScrollView.data;
using VirtualScrollView.managers;
using VirtualScrollView.managers.layout;

namespace VirtualScrollView
{
    /// <summary>
    /// Virtual List main class - Only spawns as many items as can fit in the scroll view.
    /// </summary>
    [DefaultExecutionOrder(-1)] // Init list before main code
    public class VirtualList : ScrollRect
    {
        // Event for setting cell data
        public SetCellData onSetCellData { get; set; }

        [Header("List Settings")]
        public VirtualListSettings settings;

        // Cell size cache
        public Vector2 cellSize { get; private set; }

        // List manager
        private VirtualListManager _manager;

        protected override void Awake()
        {
            base.Awake();

            // ScrollRect has [ExecuteInEditMode], so we prevent from initializing the list if we are not in play mode
            if (!Application.isPlaying) return; 

            // Get cell size
            RectTransform rect = settings.cellPrefab.transform as RectTransform;
            cellSize = rect.sizeDelta + Vector2.one * settings.padding; // Set size to cell size + padding

            // Create list manager
            _manager = new VirtualListManager(this);
        }

        /// <summary>
        /// Initializes the virtual list
        /// </summary>
        /// <param name="totalItems">Total number of cells to display</param>
        public void InitList(int totalItems)
        {
            _manager.InitList(totalItems);
        }
    }
}
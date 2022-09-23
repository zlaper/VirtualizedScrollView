using VirtualScrollView.data;
using VirtualScrollView.managers.layout;
using VirtualScrollView.managers.layout.horizontal;
using VirtualScrollView.managers.layout.vertical;
using VirtualScrollView.managers.viewport;
using VirtualScrollView.managers.viewport.horizontal;
using VirtualScrollView.managers.viewport.vertical;

namespace VirtualScrollView.managers
{
    public class VirtualListManager
    {

        private VirtualList list;
        private VirtualCellManager _cellManager;
        private IVirtualLayoutManager _layoutManager;
        private IVirtualViewport _virtualViewport;

        /// <summary>
        /// A manager for all the subsystems - Takes care of layout and creation of cells.
        /// </summary>
        /// <param name="list">The list to manage</param>
        public VirtualListManager(VirtualList list)
        {
            this.list = list;

            // Create cell manager
            _cellManager = new VirtualCellManager(list);

            // Create layout and viewport
            if (list.settings.direction == ListDirection.Vertical)
            {
                _layoutManager = new VirtualLayoutVertical(list, _cellManager);
                _virtualViewport = new VirtualViewportVertical(list);
            }
            else // Horizontal
            {
                _layoutManager = new VirtualLayoutHorizontal(list, _cellManager);
                _virtualViewport = new VirtualViewportHorizontal(list);
            }

            // Listen for set data
            _layoutManager.onSetCellData += OnSetCellData;
            // Listen for new indexes
            _virtualViewport.onChangeIndex += OnUpdateIndexes;
        }

        /// <summary>
        /// Initialize layout and viewport
        /// </summary>
        /// <param name="totalItems">Total amount of cells that will be displayed.</param>
        public void InitList(int totalItems)
        {
            // Clear previous cells
            _cellManager.Clear();
            // Initialize layout
            _layoutManager.InitCells(totalItems);
            // Initialize viewport
            _virtualViewport.InitContent(totalItems);
        }

        #region Event handlers

        private void OnSetCellData(ICell cell, int index)
        {
            list.onSetCellData?.Invoke(cell, index);
        }

        private void OnUpdateIndexes(int firstIndex, int lastIndex)
        {
            _layoutManager.ShowRange(firstIndex, lastIndex);
        }

        #endregion
    }
}
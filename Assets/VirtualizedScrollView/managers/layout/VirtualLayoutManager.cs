using System.Collections.Generic;
using VirtualScrollView.data;

namespace VirtualScrollView.managers.layout
{
    ///<inheritdoc cref="IVirtualLayoutManager"/>
    public abstract class VirtualLayoutManager : IVirtualLayoutManager
    {
        public SetCellData onSetCellData { get; set; }

        protected VirtualList list;
        private int _totalCells;
        private ICell[] _cellSlots;
        private Stack<ICell> _outOfViewportCells;
        private VirtualCellManager _cellManager;

        /// <summary>
        /// Manager for laying out the cells in the list content.
        /// </summary>
        /// <param name="list">The list to manage.</param>
        /// <param name="cellManager">The cell manage.r</param>
        public VirtualLayoutManager(VirtualList list, VirtualCellManager cellManager)
        {
            this.list = list;

            _cellManager = cellManager;
            _outOfViewportCells = new Stack<ICell>();
        }

        #region Abstract functions

        /// <summary>
        /// Set the cells position in the content.
        /// </summary>
        /// <param name="cell">The target cell.</param>
        /// <param name="index">The index of the cell.</param>
        protected abstract void SetCellPosition(ICell cell, int index);

        #endregion

        public void InitCells(int totalCells)
        {
            _totalCells = totalCells;
            // Create slots
            _cellSlots = new ICell[totalCells];
        }

        public void ShowRange(int firstIndex, int lastIndex)
        {
            // Gather out of range cells for immediate recycling
            for (int index = 0; index < firstIndex; index++) // Start to first index
                CheckOutOfViewport(index);
            for (int index = lastIndex; index < _totalCells; index++) // Last index to end
                CheckOutOfViewport(index);

            // Assign cells to slots and set data
            for (int index = firstIndex; index < lastIndex; index++)
            {
                if (_cellSlots[index] == null) // If we have an empty slots
                {
                    // Get free cell
                    ICell newCell = GetCell();
                    // Assign position
                    SetCellPosition(newCell, index);
                    // Assign to slot
                    _cellSlots[index] = newCell;
                    // Request data
                    onSetCellData(newCell, index);
                }
            }

            // Store unused out of view cells
            while (_outOfViewportCells.Count > 0)
                _cellManager.StoreCell(_outOfViewportCells.Pop());
        }

        private void CheckOutOfViewport(int index)
        {
            if (_cellSlots[index] != null) // If there is a slot in the index
            {
                _outOfViewportCells.Push(_cellSlots[index]); // Add it to the out of viewport cells stack
                _cellSlots[index] = null; // Clear the slot
            }
        }

        private ICell GetCell()
        {
            ICell cell;

            // Check if we have an out of viewport cell
            if (_outOfViewportCells.Count > 0)
            {
                // Get an out of viewport cell
                cell = _outOfViewportCells.Pop();
            }
            else
            {
                // Create or get from pool
                cell = _cellManager.GetCell();
            }

            return cell;
        }
    }
}
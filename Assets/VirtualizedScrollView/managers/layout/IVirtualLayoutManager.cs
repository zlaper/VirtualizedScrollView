using VirtualScrollView.data;

namespace VirtualScrollView.managers.layout
{
    /// <summary>
    /// Initiate an update of a cells displayed data.
    /// </summary>
    /// <param name="cell">The cell to update.</param>
    /// <param name="index">The index of the data to display.</param>
    public delegate void SetCellData(ICell cell, int index);

    public interface IVirtualLayoutManager
    {
        /// <summary>
        /// Event for setting cell data.
        /// </summary>
        SetCellData onSetCellData { get; set; }

        /// <summary>
        /// Initialize the layout manager.
        /// </summary>
        /// <param name="totalCells">Total number of cells that will be displayed.</param>
        void InitCells(int totalCells);
        /// <summary>
        /// Show a range of data in the viewport.
        /// </summary>
        /// <param name="firstIndex">The first index of the data.</param>
        /// <param name="lastIndex">The last index of the data.</param>
        void ShowRange(int firstIndex, int lastIndex);
    }
}
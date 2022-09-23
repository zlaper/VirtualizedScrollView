using UnityEngine;

namespace VirtualScrollView.data
{
    /// <summary>
    /// Cells interface.
    /// </summary>
    public interface ICell
    {
        Transform transform { get; }

        /// <summary>
        /// Provides the cell with the appropriate data, depending on its current index.
        /// </summary>
        /// <param name="data">The data to display.</param>
        void SetData(IData data);
    }
}
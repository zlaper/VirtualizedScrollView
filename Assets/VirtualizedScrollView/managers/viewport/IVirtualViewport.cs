namespace VirtualScrollView.managers.viewport
{
    /// <summary>
    /// Change the displayed data to a new range of indices.
    /// </summary>
    /// <param name="firstIndex">The first index of the data.</param>
    /// <param name="lastIndex">The last index of the data.</param>
    public delegate void ChangeIndex(int firstIndex, int lastIndex);

    public interface IVirtualViewport
    {
        /// <summary>
        /// Event to change indices.
        /// </summary>
        ChangeIndex onChangeIndex { get; set; }

        /// <summary>
        /// Initialize the viewport - Sets the size of the content container.
        /// </summary>
        /// <param name="totalItems">Total number of items that will be displayed.</param>
        void InitContent(int totalItems);
    }
}
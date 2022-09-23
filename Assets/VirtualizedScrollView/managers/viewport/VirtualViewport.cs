namespace VirtualScrollView.managers.viewport
{
    ///<inheritdoc cref="IVirtualViewport"/>
    public abstract class VirtualViewport : IVirtualViewport
    {
        public ChangeIndex onChangeIndex { get; set; }

        protected readonly VirtualList list;

        protected float cellSize;
        protected float viewportSize;
        protected float contentSize;

        protected int currentTotalItems;

        /// <summary>
        /// A virtual viewport manager - Translates the viewport into visible indices.
        /// </summary>
        /// <param name="list">The list to manage</param>
        protected VirtualViewport(VirtualList list)
        {
            this.list = list;

            // Listen for scroll event - we discard the position, since we only need to know when the list is scrolled
            this.list.onValueChanged.AddListener(_ => OnScrollContent());
        }

        #region Abstract functions

        public abstract void InitContent(int totalItems);

        /// <summary>
        /// Update the indices based on scroll position.
        /// </summary>
        protected abstract void OnScrollContent();

        #endregion
    }
}
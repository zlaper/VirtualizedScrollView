namespace VirtualScrollView.data
{
    /// <summary>
    /// Data wrapper for controlled access to data.
    /// </summary>
    /// <typeparam name="T">Accepts IData types for data.</typeparam>
    public interface IDataProvider<T> where T : IData
    {
        int totalItems { get; }

        void SetItems(T[] items);
        T GetItem(int index);
    }
}
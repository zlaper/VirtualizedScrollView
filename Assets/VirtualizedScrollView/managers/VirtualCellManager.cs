using System.Collections.Generic;
using UnityEngine;
using VirtualScrollView.data;
using VirtualScrollView.utils;

namespace VirtualScrollView.managers
{
    public class VirtualCellManager
    {
        private readonly List<ICell> _usedCells;
        private readonly Transform _contentTransform;

        private SimpleCellPool _cellPool;

        /// <summary>
        /// Manages the creation and storage of cells.
        /// </summary>
        /// <param name="list">The list to manage.</param>
        public VirtualCellManager(VirtualList list)
        {
            _usedCells = new List<ICell>();
            // Content transform cache
            _contentTransform = list.content.transform;
            // Create cell pool
            CreatePool(list);
        }

        /// <summary>
        /// Get a new or pooled cell.
        /// </summary>
        /// <returns>A cell to use for display.</returns>
        public ICell GetCell()
        {
            ICell cell = _cellPool.Create();

            cell.transform.SetParent(_contentTransform, false);

            // Keep a reference for clearing
            if (!_usedCells.Contains(cell))
                _usedCells.Add(cell);

            return cell;
        }

        /// <summary>
        /// Store a cell for later usage.
        /// </summary>
        /// <param name="cell">The cell to store.</param>
        public void StoreCell(ICell cell)
        {
            if (_usedCells.Contains(cell))
                _usedCells.Remove(cell);

            _cellPool.Store(cell);
        }
        
        /// <summary>
        /// Store all created cells to the pool.
        /// </summary>
        public void Clear()
        {
            int len = _usedCells.Count;
            for (int i = 0; i < len; i++)
                StoreCell(_usedCells[i]);

            _usedCells.Clear();
        }

        private void CreatePool(VirtualList list)
        {
            GameObject pool = new GameObject("CellPool");
            pool.transform.SetParent(list.transform, false);

            _cellPool = pool.AddComponent<SimpleCellPool>();
            _cellPool.Init(list.settings.cellPrefab);
        }
    }
}
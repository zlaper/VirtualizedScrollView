using System;
using System.Collections.Generic;
using UnityEngine;
using VirtualScrollView.data;

namespace VirtualScrollView.utils
{
    /// <summary>
    /// A simple cell pool.
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public class SimpleCellPool : MonoBehaviour
    {
        private GameObject _original;
        private Stack<ICell> _pool;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Initialize the pool
        /// </summary>
        /// <param name="itemTemplate">The cell template to create</param>
        public void Init(GameObject itemTemplate)
        {
            _original = itemTemplate;
            _pool = new Stack<ICell>();
        }

        /// <summary>
        /// Get a new or pooled cell.
        /// </summary>
        /// <returns>A cell to use for display.</returns>
        public ICell Create()
        {
            ICell item;
            if (_pool.Count > 0)
            {
                item = _pool.Pop();
            }
            else
            {
                GameObject original = Instantiate(_original);
                item = original.GetComponent<ICell>();

                if (item == null) throw new Exception("VirtualList: Invalid cell template.");
            }

            return item;
        }

        /// <summary>
        /// Store a cell for later usage.
        /// </summary>
        /// <param name="cell">The cell to store.</param>
        public void Store(ICell cell)
        {
            if (!_pool.Contains(cell))
            {
                cell.transform.SetParent(transform, false);
                _pool.Push(cell);
            }
        }
    }
}
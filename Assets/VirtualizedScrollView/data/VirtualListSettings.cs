using System;
using UnityEngine;

namespace VirtualScrollView.data
{
    public enum ListDirection
    {
        Vertical, 
        Horizontal
    }

    /// <summary>
    /// Settings for virtual list
    /// </summary>
    [Serializable]
    public struct VirtualListSettings
    {
        public ListDirection direction;
        public GameObject cellPrefab;
        public float padding;
    }
}
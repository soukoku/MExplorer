using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MExplorer
{
    /// <summary>
    /// An observable collection that will dispose items when they are removed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AutoDisposeObservableCollection<T> : ObservableCollection<T> where T : IDisposable
    {
        /// <summary>
        /// Clears the items.
        /// </summary>
        protected override void ClearItems()
        {
            // use a handle since the items may still be in display and disposing them will be bad.
            var handle = new List<T>(this);
            base.ClearItems();
            foreach (var it in handle) { it.Dispose(); }
        }

        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="index">The index.</param>
        protected override void RemoveItem(int index)
        {
            T it = this[index];
            base.RemoveItem(index);
            if (it != null) { it.Dispose(); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MExplorer
{
    public class AutoDisposeObservableCollection<T> : ObservableCollection<T> where T : IDisposable
    {
        protected override void ClearItems()
        {
            var list = new List<T>(this);
            base.ClearItems();
            foreach (var it in list) { it.Dispose(); }
        }

        protected override void RemoveItem(int index)
        {
            T it = this[index];
            base.RemoveItem(index);
            if (it != null) { it.Dispose(); }
        }
    }
}

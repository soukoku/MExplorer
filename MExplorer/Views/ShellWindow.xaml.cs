using MExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MExplorer.Views
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        ShellVM _shell;

        public ShellWindow()
        {
            InitializeComponent();

            _shell = this.DataContext as ShellVM;
            if (_shell != null)
            {
                _shell.Providers.CollectionChanged += Providers_CollectionChanged;
                foreach (var p in _shell.Providers)
                {
                    AddTabItem(p);
                }
            }
        }

        #region tab fixes

        // do our own tab creation instead of databinding to persist tab state on switch
        // http://www.codeproject.com/Articles/212233/Persist-the-Visual-Tree-when-switching-tabs-in-the

        void Providers_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var it in e.NewItems) { AddTabItem(it); }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (var it in e.OldItems) { RemoveTabItem(it); }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
            }
        }

        private void AddTabItem(object vm)
        {
            var view = new ProviderView();
            var tab = new TabItem
            {
                DataContext = vm,
                Content = new ModernWPF.Controls.ModernContentControl { Content = view }
            };
            tab.SetBinding(TabItem.HeaderProperty, new Binding("Name"));

            theTabs.Items.Add(tab);

            // When there is only 1 Item, the tab can't be rendered without have it selected
            // Don't do Refresh(). This may clear
            // the Selected item, causing issue in the ViewModel
            if (theTabs.SelectedItem == null)
                tab.IsSelected = true;
        }

        private void RemoveTabItem(object view)
        {
            var foundItem = theTabs.Items.Cast<TabItem>().FirstOrDefault(t => t.DataContext == view);

            if (foundItem != null)
                theTabs.Items.Remove(foundItem);
        }

        private void theTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var ti = e.AddedItems[0] as TabItem;
                if (ti != null)
                {
                    var prov = ti.DataContext as ProviderVM;
                    _shell.SelectedProvider = prov;
                }
            }
        }

        #endregion
    }
}

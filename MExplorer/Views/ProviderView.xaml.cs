using MExplorer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ProviderView.xaml
    /// </summary>
    public partial class ProviderView : UserControl
    {
        GridView _gridView;
        public ProviderView()
        {
            InitializeComponent();
            _gridView = this.Resources["GridView"] as GridView;
            this.DataContextChanged += ProviderView_DataContextChanged;
        }

        void ProviderView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var d = this.DataContext;
        }

        private void theTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var vm = this.DataContext as ProviderVM;
            if (vm != null)
            {
                vm.SelectedContainer = e.NewValue as ContainerVM;
            }
        }


        #region list view

        private void theView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_gridView != null)
            {
                GenerateMetaColumns();
            }
        }

        private void GenerateMetaColumns()
        {
            while (_gridView.Columns.Count > 1)
            {
                _gridView.Columns.RemoveAt(1);
            }

            // dynamically generate columns for meta data 
            var container = theView.DataContext as ContainerVM;
            if (container != null)
            {

                foreach (var info in container.KnownMetaData)
                {
                    GridViewColumn col = new GridViewColumn
                    {
                        Header = info.Name,
                        HeaderContainerStyle = TryFindResource(string.Format("{0}AlignColHeader", info.HeaderAlignment)) as Style,
                        Width = info.Width,
                    };
                    var txt = new FrameworkElementFactory(typeof(TextBlock));
                    txt.SetBinding(TextBlock.TextProperty, new Binding(string.Format("MetaData[{0}].Value", info.Name)) { Converter = info.Formatter, ConverterParameter = info.FormatParameter });
                    txt.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis);
                    txt.SetValue(TextBlock.TextAlignmentProperty, info.ContentAlignment);
                    col.CellTemplate = new DataTemplate() { VisualTree = txt };

                    _gridView.Columns.Add(col);
                }
            }
        }

        protected void ListItem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    HandleListItemAction(sender);
                    break;
            }
        }
        protected void ListItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            HandleListItemAction(sender);
        }

        private void HandleListItemAction(object sender)
        {
            var lvi = sender as ListViewItem;
            if (lvi != null)
            {
                var container = lvi.DataContext as ContainerVM;
                if (container != null)
                {
                    container.Parent.IsExpanded = true;
                    container.IsTreeSelected = true;
                }
                else
                {
                    var item = lvi.DataContext as ItemVM;
                    if (item != null)
                    {
                        item.DoDefaultAction();
                    }
                }
            }
        }

        #endregion
    }
}

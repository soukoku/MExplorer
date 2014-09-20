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

namespace MExplorer
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        GridView _gridView;
        public ShellWindow()
        {
            InitializeComponent();
            _gridView = this.Resources["GridView"] as GridView;
        }

        private void theTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var shell = this.DataContext as ShellVM;
            if (shell != null)
            {
                shell.SelectedContainer = e.NewValue as ContainerVM;
            }
        }

        string _columnXaml =
@"<GridViewColumn Header=""{Binding MetaData[{0}].Name}""  Width=""100""
DisplayMemberBinding=""{Binding MetaData[{0}].FormattedValue}""/>";

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

    }
}

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
        public ShellWindow()
        {
            InitializeComponent();
        }

        private void theTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var shell = this.DataContext as ShellVM;
            if (shell != null)
            {
                shell.SelectedContainer = e.NewValue as ContainerVM;
            }
        }

    }
}

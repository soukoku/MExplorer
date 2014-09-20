using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MExplorer.ViewModels
{
    class ShellVM : ViewModelBase
    {
        public ShellVM()
        {
            RootContainers = new ObservableCollection<ContainerVM>();
            if (IsInDesignMode)
            {
                for (int i = 0; i < 10; i++)
                {
                    RootContainers.Add(new ContainerVM(null, null, "Sample " + i, false));
                }
            }
            else
            {
                foreach (var p in ProviderLoader.FindProviders())
                {
                    RootContainers.Add(p.Root);
                }
            }
        }

        public ObservableCollection<ContainerVM> RootContainers { get; private set; }

        private ContainerVM _selectedContainer;

        public ContainerVM SelectedContainer
        {
            get { return _selectedContainer; }
            set
            {
                _selectedContainer = value;
                RaisePropertyChanged(() => SelectedContainer);

                if (value != null)
                {
                    value.LoadContainers(false);
                    value.LoadSingles(false);
                }
            }
        }

    }
}

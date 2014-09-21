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
            Providers = new ObservableCollection<ProviderVM>();
            if (IsInDesignMode)
            {
                //for (int i = 0; i < 10; i++)
                //{
                //    Providers.Add(new ContainerVM(null, null, "Sample " + i, false));
                //}
            }
            else
            {
                foreach (var p in ProviderLoader.FindProviders())
                {
                    Providers.Add(new ProviderVM(p));
                }
            }
        }

        public ObservableCollection<ProviderVM> Providers { get; private set; }

        private ProviderVM _selectedProv;

        public ProviderVM SelectedProvider
        {
            get { return _selectedProv; }
            set
            {
                _selectedProv = value;
                RaisePropertyChanged(() => SelectedProvider);
            }
        }


    }
}

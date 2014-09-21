using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer.ViewModels
{
    class ProviderVM : ViewModelBase
    {
        private IItemProvider _provider;

        public ProviderVM(IItemProvider provider)
        {
            _provider = provider;
        }

        public string Name { get { return _provider.Name; } }

        public IEnumerable<ContainerVM> Roots { get { yield return _provider.Root; } }

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

        public override string ToString()
        {
            return Name;
        }
    }
}

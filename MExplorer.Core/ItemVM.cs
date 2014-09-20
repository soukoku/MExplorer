using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace MExplorer
{
    public class ItemVM : ViewModelBase
    {
        public ItemVM(IItemProvider provider, string name, bool canRename)
        {
            Provider = provider;
            _name = name;
            CanRename = canRename;
        }

        public IItemProvider Provider { get; private set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (OnNameChanging(value)) { _name = value; }
                RaisePropertyChanged(() => Name);
            }
        }

        protected virtual bool OnNameChanging(string value)
        {
            return true;
        }

        private ICommand _renameCommand;

        public ICommand RenameCommand
        {
            get
            {
                return _renameCommand ?? (
                    _renameCommand = new RelayCommand(() => { IsRenaming = true; }, () => { return CanRename && !IsRenaming; })
                    );
            }
        }

        public bool CanRename { get; private set; }

        private bool _isRenaming;

        public bool IsRenaming
        {
            get { return _isRenaming; }
            private set
            {
                _isRenaming = value;
                RaisePropertyChanged(() => IsRenaming);
            }
        }

        protected virtual ImageSource GetIcon(IconSize size)
        {
            switch (size)
            {
                case IconSize.ExtraLarge:
                    return DefaultIcons.ItemExtraLarge;
                case IconSize.Large:
                    return DefaultIcons.ItemLarge;
                case IconSize.Medium:
                    return DefaultIcons.ItemMedium;
                case IconSize.Small:
                    return DefaultIcons.ItemSmall;
            }
            return null;
        }

        public ImageSource SmallIcon { get { return GetIcon(IconSize.Small); } }
        public ImageSource MediumIcon { get { return GetIcon(IconSize.Medium); } }
        public ImageSource LargeIcon { get { return GetIcon(IconSize.Large); } }
        public ImageSource ExtraLargeIcon { get { return GetIcon(IconSize.ExtraLarge); } }

        public override string ToString()
        {
            return Name;
        }
    }
}

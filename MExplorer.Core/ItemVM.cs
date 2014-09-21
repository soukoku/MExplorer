using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MExplorer.Assets;
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
    /// <summary>
    /// Represents a basic hierarchy item.
    /// </summary>
    public class ItemVM : ViewModelBase, IDisposable
    {
        public ItemVM(IItemProvider provider, ContainerVM parent, string name, bool canRename)
        {
            Provider = provider;
            Parent = parent;
            _name = name;
            CanRename = canRename;
            MetaData = new MetaData();
        }

        public IItemProvider Provider { get; private set; }

        public ContainerVM Parent { get; private set; }

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

        public MetaData MetaData { get; private set; }

        #region icon

        /// <summary>
        /// Called when icon is requested.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        protected virtual ImageSource OnGetIcon(IconSize size)
        {
            switch (size)
            {
                case IconSize.ExtraLarge:
                    return DefaultIcons.ExtraLargeItem;
                case IconSize.Large:
                    return DefaultIcons.LargeItem;
                case IconSize.Medium:
                    return DefaultIcons.MediumItem;
                case IconSize.Small:
                    return DefaultIcons.SmallItem;
            }
            return null;
        }

        /// <summary>
        /// Gets the small icon.
        /// </summary>
        /// <value>
        /// The small icon.
        /// </value>
        public ImageSource SmallIcon { get { return OnGetIcon(IconSize.Small); } }
        /// <summary>
        /// Gets the medium icon.
        /// </summary>
        /// <value>
        /// The medium icon.
        /// </value>
        public ImageSource MediumIcon { get { return OnGetIcon(IconSize.Medium); } }
        /// <summary>
        /// Gets the large icon.
        /// </summary>
        /// <value>
        /// The large icon.
        /// </value>
        public ImageSource LargeIcon { get { return OnGetIcon(IconSize.Large); } }
        /// <summary>
        /// Gets the extra large icon.
        /// </summary>
        /// <value>
        /// The extra large icon.
        /// </value>
        public ImageSource ExtraLargeIcon { get { return OnGetIcon(IconSize.ExtraLarge); } }

        #endregion

        /// <summary>
        /// Performs the default action on this item.
        /// </summary>
        public virtual void DoDefaultAction() { }

        public override string ToString()
        {
            return Name;
        }

        #region IDisposable Members

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ItemVM()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            IsDisposed = true;
            if (disposing)
            {
                OnDisposeManaged();
            }
            OnDisposeNative();
        }

        protected virtual void OnDisposeManaged()
        {
            Cleanup();
        }

        protected virtual void OnDisposeNative()
        {

        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace MExplorer
{
    public class ContainerVM : ItemVM
    {
        public ContainerVM(IItemProvider provider, string name, bool canRename)
            : base(provider, name, canRename)
        {
            ContainerChildren = new ObservableCollection<ContainerVM>();
            ContainerChildren.Add(this); // dummy self for expander

            SingleChildren = new ObservableCollection<ItemVM>();
            AllChildren = new CompositeCollection();
            AllChildren.Add(new CollectionContainer { Collection = ContainerChildren });
            AllChildren.Add(new CollectionContainer { Collection = SingleChildren });

        }

        private bool _isExpanded;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                _isExpanded = value;
                if (value)
                {
                    LoadContainers(true);
                }
                RaisePropertyChanged(() => IsExpanded);
            }
        }

        public virtual void LoadContainers(bool refresh)
        {
            if (refresh || ContainerChildren.Count == 0 || ContainerChildren.FirstOrDefault() == this)
            {
                ContainerChildren.Clear();
                foreach (var c in Provider.GetContainerChildren(this))
                {
                    ContainerChildren.Add(c);
                }
            }
        }

        public virtual void LoadSingles(bool refresh)
        {
            if (refresh || SingleChildren.Count == 0)
            {
                SingleChildren.Clear();
                foreach (var c in Provider.GetSingleChildren(this))
                {
                    SingleChildren.Add(c);
                }
            }
        }

        protected override ImageSource GetIcon(IconSize size)
        {
            switch (size)
            {
                case IconSize.ExtraLarge:
                    return DefaultIcons.ContainerExtraLarge;
                case IconSize.Large:
                    return DefaultIcons.ContainerLarge;
                case IconSize.Medium:
                    return DefaultIcons.ContainerMedium;
                case IconSize.Small:
                    return DefaultIcons.ContainerSmall;
            }
            return null;
        }


        public virtual ViewTypes AllowedViews { get { return ViewTypes.Details; } }

        public CompositeCollection AllChildren { get; private set; }

        public ObservableCollection<ContainerVM> ContainerChildren { get; private set; }

        public ObservableCollection<ItemVM> SingleChildren { get; private set; }
    }
}

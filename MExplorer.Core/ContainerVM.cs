using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace MExplorer
{
    public class ContainerVM : ItemVM
    {
        public static readonly ContainerVM PLACE_HOLDER = new ContainerVM(null, null, null, false);


        public ContainerVM(IItemProvider provider, ContainerVM parent, string name, bool canRename)
            : base(provider, parent, name, canRename)
        {
            ContainerChildren = new AutoDisposeObservableCollection<ContainerVM>();
            ContainerChildren.Add(PLACE_HOLDER); // dummy self for expander

            SingleChildren = new AutoDisposeObservableCollection<ItemVM>();
            
            AllChildren = new CompositeCollection();
            if (IncludeContainersInAll)
            {
                AllChildren.Add(new CollectionContainer { Collection = ContainerChildren });
            }
            AllChildren.Add(new CollectionContainer { Collection = SingleChildren });

        }

        public virtual bool IncludeContainersInAll { get { return true; } }

        public virtual string AppendText { get { return null; } }

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
            if (refresh || ContainerChildren.Count == 0 || ContainerChildren.FirstOrDefault() == PLACE_HOLDER)
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

        public AutoDisposeObservableCollection<ContainerVM> ContainerChildren { get; private set; }

        public AutoDisposeObservableCollection<ItemVM> SingleChildren { get; private set; }

        public virtual IEnumerable<MetaColumnInfo> KnownMetaData { get { return Enumerable.Empty<MetaColumnInfo>(); } }
    }
}

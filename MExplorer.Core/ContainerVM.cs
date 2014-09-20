using MExplorer.Assets;
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
    /// <summary>
    /// Represents a container type item.
    /// </summary>
    public class ContainerVM : ItemVM
    {
        /// <summary>
        /// The placeholder container for tricking the UI that it has children containers.
        /// </summary>
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

        public virtual string BadgeText { get { return null; } }

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

        /// <summary>
        /// Loads the <see cref="ContainerChildren"/> collection.
        /// </summary>
        /// <param name="refresh">if set to <c>true</c> then always load, otherwise it'll only load when necessary.</param>
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

        /// <summary>
        /// Loads the <see cref="SingleChildren"/> collection.
        /// </summary>
        /// <param name="refresh">if set to <c>true</c> then always load, otherwise it'll only load when necessary.</param>
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

        /// <summary>
        /// Called when icon is requested.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        protected override ImageSource OnGetIcon(IconSize size)
        {
            switch (size)
            {
                case IconSize.ExtraLarge:
                    return DefaultIcons.ExtraLargeContainer;
                case IconSize.Large:
                    return DefaultIcons.LargeContainer;
                case IconSize.Medium:
                    return DefaultIcons.MediumContainer;
                case IconSize.Small:
                    return DefaultIcons.SmallContainer;
            }
            return null;
        }


        //public virtual ViewTypes AllowedViews { get { return ViewTypes.Details; } }

        /// <summary>
        /// Gets all children.
        /// </summary>
        /// <value>
        /// All children.
        /// </value>
        public CompositeCollection AllChildren { get; private set; }

        /// <summary>
        /// Gets the children that are <see cref="ContainerVM"/>.
        /// </summary>
        /// <value>
        /// The container children.
        /// </value>
        public AutoDisposeObservableCollection<ContainerVM> ContainerChildren { get; private set; }

        /// <summary>
        /// Gets the children that are <see cref="ItemVM"/>.
        /// </summary>
        /// <value>
        /// The single children.
        /// </value>
        public AutoDisposeObservableCollection<ItemVM> SingleChildren { get; private set; }

        /// <summary>
        /// Gets the known meta data columns for UI to display.
        /// </summary>
        /// <value>
        /// The known meta data.
        /// </value>
        public virtual IEnumerable<MetaColumnInfo> KnownMetaData { get { return Enumerable.Empty<MetaColumnInfo>(); } }
    }
}

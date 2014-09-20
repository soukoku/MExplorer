using MExplorer.Registry.Assets;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer.Registry
{
    class ValueVM : ItemVM
    {
        public ValueVM(IItemProvider provider, ContainerVM parent, string name, RegistryValueKind kind) :
            base(provider, parent, name, false)
        {
            Kind = kind;
        }

        public RegistryValueKind Kind { get; private set; }

        protected override System.Windows.Media.ImageSource GetIcon(IconSize size)
        {
            if (size == IconSize.Small)
                switch (Kind)
                {
                    case RegistryValueKind.Binary:
                    case RegistryValueKind.DWord:
                    case RegistryValueKind.QWord:
                    case RegistryValueKind.Unknown:
                        return ImageAssets.BinarySmall;
                    default:
                        return ImageAssets.StringSmall;
                }
            return base.GetIcon(size);
        }
    }
}
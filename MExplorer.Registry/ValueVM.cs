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
        public ValueVM(IItemProvider provider, ContainerVM parent, string name, RegistryValueKind kind, object value) :
            base(provider, parent, string.IsNullOrEmpty(name) ? "(Default)" : name, false)
        {
            MetaData.Add(new MetaEntry { Name = RegistryMetaData.Type.Name, Value = kind });
            MetaData.Add(new MetaEntry { Name = RegistryMetaData.Data.Name, Value = value });
        }

        public RegistryValueKind Kind { get { return (RegistryValueKind)MetaData[0].Value; } }

        protected override System.Windows.Media.ImageSource OnGetIcon(IconSize size)
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
            return base.OnGetIcon(size);
        }
    }
}
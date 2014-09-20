using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MExplorer.Files
{
    class ShellFolderVM : ContainerVM
    {

        public ShellFolderVM(IItemProvider provider, ContainerVM parent, ShellFolder folder)
            : base(provider, parent, folder.GetDisplayName(DisplayNameType.Default), false)
        {
            Folder = folder;
            Util.FillCommonData(folder, MetaData);
        }

        public ShellFolder Folder { get; private set; }

        public override void LoadContainers(bool refresh)
        {
            if (refresh || ContainerChildren.Count == 0 || ContainerChildren.FirstOrDefault() == ContainerVM.PLACE_HOLDER)
            {
                // due to nature of shell enumeration 
                // we can load both file and folders together.

                ContainerChildren.Clear();
                SingleChildren.Clear();

                foreach (ShellObject sub in Folder)
                {
                    var folder = sub as ShellFolder;
                    if (folder != null)
                    {
                        ContainerChildren.Add(new ShellFolderVM(Provider, this, folder));
                    }
                    else
                    {
                        var file = sub as ShellFile;
                        if (file != null)
                        {
                            SingleChildren.Add(new ShellFileVM(Provider, this, file));
                        }
                        else
                        {
                            sub.Dispose();
                        }
                    }
                }
            }
        }


        protected override ImageSource GetIcon(IconSize size)
        {
            return Util.GetIcon(Folder, size);
        }

        static MetaColumnInfo[] _metas = new MetaColumnInfo[] { 
            FileMetaData.ModifiedDate,
            FileMetaData.Type,
            FileMetaData.Size,
        };


        public override IEnumerable<MetaColumnInfo> KnownMetaData
        {
            get
            {
                return _metas;
            }
        }

        protected override void OnDisposeManaged()
        {
            if (Folder != null)
            {
                Folder.Dispose();
                Folder = null;
            }
            base.OnDisposeManaged();
        }
    }
}

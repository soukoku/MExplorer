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
            Folder.Thumbnail.CurrentSize = new System.Windows.Size(16, 16);
            switch (size)
            {
                case IconSize.ExtraLarge:
                    return Folder.Thumbnail.LargeBitmapSource;
                case IconSize.Large:
                    return Folder.Thumbnail.MediumBitmapSource;
                case IconSize.Medium:
                    return Folder.Thumbnail.SmallBitmapSource;
                case IconSize.Small:
                    return Folder.Thumbnail.BitmapSource;
            }
            return null;
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

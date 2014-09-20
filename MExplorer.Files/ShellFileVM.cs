using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MExplorer.Files
{
    class ShellFileVM : ItemVM
    {
        public ShellFileVM(IItemProvider provider, ContainerVM parent, ShellFile file)
            : base(provider, parent, file.GetDisplayName(DisplayNameType.Default), false)
        {
            File = file;
        }

        public ShellFile File { get; private set; }

        protected override ImageSource GetIcon(IconSize size)
        {
            File.Thumbnail.CurrentSize = new System.Windows.Size(16, 16);
            switch (size)
            {
                case IconSize.ExtraLarge:
                    return File.Thumbnail.LargeBitmapSource;
                case IconSize.Large:
                    return File.Thumbnail.MediumBitmapSource;
                case IconSize.Medium:
                    return File.Thumbnail.SmallBitmapSource;
                case IconSize.Small:
                    return File.Thumbnail.BitmapSource;
            }
            return null;
        }

        protected override void OnDisposeManaged()
        {
            if (File != null)
            {
                File.Dispose();
                File = null;
            }
            base.OnDisposeManaged();
        }
    }
}

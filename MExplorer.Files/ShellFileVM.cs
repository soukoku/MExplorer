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
            Util.FillCommonData(file, MetaData);
        }

        public ShellFile File { get; private set; }

        protected override ImageSource GetIcon(IconSize size)
        {
            return Util.GetIcon(File, size);
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

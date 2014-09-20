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

        public ShellFolderVM(IItemProvider provider, ContainerVM parent, string parsingName)
            : base(provider, parent, GetName(parsingName), false)
        {
            ParsingName = parsingName;
        }

        public string ParsingName { get; private set; }

        public override void LoadContainers(bool refresh)
        {
            if (refresh || ContainerChildren.Count == 0 || ContainerChildren.FirstOrDefault() == this)
            {
                // due to nature of shell enumeration 
                // we can load both file and folders together.

                ContainerChildren.Clear();
                SingleChildren.Clear();
                using (ShellFolder sh = ShellObject.FromParsingName(ParsingName) as ShellFolder)
                {
                    foreach (ShellObject sub in sh)
                    {
                        var folder = sub as ShellFolder;
                        if (folder != null)
                        {
                            ContainerChildren.Add(new ShellFolderVM(Provider, this, sub.ParsingName));
                        }
                        else
                        {
                            var file = sub as ShellFile;
                            if (file != null)
                            {
                                SingleChildren.Add(new ShellFileVM(Provider, this, sub.ParsingName));
                            }
                        }
                        sub.Dispose();
                    }
                }
            }
        }

        private static string GetName(string parsingName)
        {
            using (var sh = ShellObject.FromParsingName(parsingName))
            {
                return sh.GetDisplayName(DisplayNameType.Default);
            }
        }

        protected override ImageSource GetIcon(IconSize size)
        {
            using (var shell = ShellObject.FromParsingName(ParsingName))
            {
                shell.Thumbnail.CurrentSize = new System.Windows.Size(16, 16);
                switch (size)
                {
                    case IconSize.ExtraLarge:
                        return shell.Thumbnail.LargeBitmapSource;
                    case IconSize.Large:
                        return shell.Thumbnail.MediumBitmapSource;
                    case IconSize.Medium:
                        return shell.Thumbnail.SmallBitmapSource;
                    case IconSize.Small:
                        return shell.Thumbnail.BitmapSource;
                }
            }
            return null;
        }
    }
}

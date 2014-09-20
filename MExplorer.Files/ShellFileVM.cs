using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MExplorer.Files.SystemItems
{
    class ShellFileVM : ItemVM
    {
        public ShellFileVM(IItemProvider provider, string parsingName)
            : base(provider, GetName(parsingName), false)
        {
            ParsingName = parsingName;
        }

        public string ParsingName { get; private set; }

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

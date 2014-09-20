using MExplorer.Files.SystemItems;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer.Files
{
    class FileSystemItemProvider : IItemProvider
    {
        #region IItemProvider Members

        public ContainerVM Root
        {
            get
            {
                using (var sh = KnownFolders.Desktop)
                {
                    return new ShellFolderVM(this, sh.ParsingName) { IsExpanded = true };
                }
            }
        }

        public IEnumerable<ItemVM> GetSingleChildren(ContainerVM container)
        {
            var list = new List<ItemVM>();

            var mine = container as ShellFolderVM;
            if (mine != null)
            {
                using (ShellFolder sh = ShellObject.FromParsingName(mine.ParsingName) as ShellFolder)
                {
                    foreach (ShellObject sub in sh)
                    {
                        var file = sub as ShellFile;
                        if (file != null)
                        {
                            list.Add(new ShellFileVM(mine.Provider, sub.ParsingName));
                        }
                        sub.Dispose();
                    }
                }
            }

            return list;
        }

        public IEnumerable<ContainerVM> GetContainerChildren(ContainerVM container)
        {
            var list = new List<ContainerVM>();

            var mine = container as ShellFolderVM;
            if (mine != null)
            {
                using (ShellFolder sh = ShellObject.FromParsingName(mine.ParsingName) as ShellFolder)
                {
                    foreach (ShellObject sub in sh)
                    {
                        var folder = sub as ShellFolder;
                        if (folder != null)
                        {
                            list.Add(new ShellFolderVM(mine.Provider, sub.ParsingName));
                        }
                        sub.Dispose();
                    }
                }
            }

            return list;
        }

        #endregion
    }
}

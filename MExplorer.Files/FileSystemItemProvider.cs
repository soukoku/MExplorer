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
                    return new ShellFolderVM(this, null, (ShellFolder)ShellObject.FromParsingName(sh.ParsingName)) { IsExpanded = true };
                }
            }
        }

        public IEnumerable<ItemVM> GetSingleChildren(ContainerVM container)
        {
            var list = new List<ItemVM>();

            var mine = container as ShellFolderVM;
            if (mine != null)
            {
                foreach (ShellObject sub in mine.Folder)
                {
                    var file = sub as ShellFile;
                    if (file != null)
                    {
                        list.Add(new ShellFileVM(mine.Provider, mine, file));
                    }
                    else
                    {
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
                foreach (ShellObject sub in mine.Folder)
                {
                    var folder = sub as ShellFolder;
                    if (folder != null)
                    {
                        list.Add(new ShellFolderVM(mine.Provider, mine, folder));
                    }
                    else
                    {
                        sub.Dispose();
                    }
                }
            }

            return list;
        }

        #endregion
    }
}

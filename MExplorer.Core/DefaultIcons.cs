using CommonWin32.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Media;

namespace MExplorer
{
    public static class DefaultIcons
    {
        static DefaultIcons()
        {
            //var filePath = Assembly.GetExecutingAssembly().Location;
            //var folderPath = Path.GetDirectoryName(filePath);

            //var folder = ShellFileSystemFolder.FromFolderPath(folderPath);

            ContainerSmall = IconReader.GetFolderIconWpf(IconReader.IconSize.Small, IconReader.FolderType.Closed);
            ContainerMedium = IconReader.GetFolderIconWpf(IconReader.IconSize.Large, IconReader.FolderType.Closed);
            //ContainerLarge = folder.Thumbnail.MediumBitmapSource;
            //ContainerExtraLarge = folder.Thumbnail.LargeBitmapSource;

            //var file = ShellFile.FromFilePath(filePath);

            ItemSmall = IconReader.GetFileIconWpf("dummy", IconReader.IconSize.Small, false);
            ItemMedium = IconReader.GetFileIconWpf("dummy", IconReader.IconSize.Large, false);
            //ItemLarge = file.Thumbnail.MediumBitmapSource;
            //ItemExtraLarge = file.Thumbnail.LargeBitmapSource;

        }

        public static ImageSource ContainerSmall { get; private set; }
        public static ImageSource ContainerMedium { get; private set; }
        public static ImageSource ContainerLarge { get; private set; }
        public static ImageSource ContainerExtraLarge { get; private set; }


        public static ImageSource ItemSmall { get; private set; }
        public static ImageSource ItemMedium { get; private set; }
        public static ImageSource ItemLarge { get; private set; }
        public static ImageSource ItemExtraLarge { get; private set; }
    }
}

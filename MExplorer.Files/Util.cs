using MExplorer.Converters;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace MExplorer.Files
{
    class Util
    {
        internal static void FillCommonData(ShellFolder shell, MetaData data)
        {
            if (shell.IsFileSystemObject)
            {
                var di = new DirectoryInfo(shell.GetDisplayName(DisplayNameType.FileSystemPath));
                data.Add(new MetaEntry { Name = FileMetaData.ModifiedDate.Name, Value = di.LastWriteTimeUtc });
                data.Add(new MetaEntry { Name = FileMetaData.Type.Name, Value = "File Folder" });
            }
        }
        internal static void FillCommonData(ShellFile shell, MetaData data)
        {
            if (shell.IsFileSystemObject)
            {
                var fi = new FileInfo(shell.GetDisplayName(DisplayNameType.FileSystemPath));
                data.Add(new MetaEntry { Name = FileMetaData.ModifiedDate.Name, Value = fi.LastWriteTimeUtc });
                data.Add(new MetaEntry { Name = FileMetaData.Type.Name, Value = "File" });
                data.Add(new MetaEntry { Name = FileMetaData.Size.Name, Value = fi.Length });
            }
        }

        internal static ImageSource GetIcon(ShellObject shell, IconSize size)
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
            return null;
        }
    }
}

using MExplorer.Converters;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
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
                data.Add(new MetaEntry { Name = FileMetaData.ModifiedDate.Name, Value = shell.Properties.GetProperty(SystemProperties.System.DateModified).ValueAsObject });
                data.Add(new MetaEntry { Name = FileMetaData.Type.Name, Value = shell.Properties.GetProperty(SystemProperties.System.ItemTypeText).ValueAsObject });
            }
        }
        internal static void FillCommonData(ShellFile shell, MetaData data)
        {
            if (shell.IsFileSystemObject)
            {
                data.Add(new MetaEntry { Name = FileMetaData.ModifiedDate.Name, Value = shell.Properties.GetProperty(SystemProperties.System.DateModified).ValueAsObject });
                data.Add(new MetaEntry { Name = FileMetaData.Type.Name, Value = shell.Properties.GetProperty(SystemProperties.System.ItemTypeText).ValueAsObject });
                data.Add(new MetaEntry { Name = FileMetaData.Size.Name, Value = shell.Properties.GetProperty(SystemProperties.System.Size).ValueAsObject });
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

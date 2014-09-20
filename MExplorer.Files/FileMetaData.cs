using MExplorer.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer.Files
{
    static class FileMetaData
    {

        public static readonly MetaColumnInfo ModifiedDate = new MetaColumnInfo { Name = "Date modified", Width = 150, Formatter = new ToLocalDateConverter() };
        public static readonly MetaColumnInfo Type = new MetaColumnInfo { Name = "Type" };
        public static readonly MetaColumnInfo Size = new MetaColumnInfo { Name = "Size", ContentAlignment = System.Windows.TextAlignment.Right, Formatter = new StorageSizeConverter(), FormatParameter = "#" };
    }
}

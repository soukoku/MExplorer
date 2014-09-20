using MExplorer.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MExplorer.Registry
{
    static class RegistryMetaData
    {
        public static readonly MetaColumnInfo Type = new MetaColumnInfo { Name = "Type" };
        public static readonly MetaColumnInfo Data = new MetaColumnInfo { Name = "Data", Width = 200 };
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace MExplorer
{
    public class MetaColumnInfo
    {
        public MetaColumnInfo()
        {
            Width = 100;
        }

        public string Name { get; set; }
        public int Width { get; set; }
        public HorizontalAlignment HeaderAlignment { get; set; }
        public TextAlignment ContentAlignment { get; set; }


        public object FormatParameter { get; set; }

        public IValueConverter Formatter { get; set; }

        //public object FormattedValue
        //{
        //    get
        //    {
        //        if (Formatter != null)
        //        {
        //            return Formatter.Convert(Value, typeof(object), FormatParameter, CultureInfo.CurrentCulture);
        //        }
        //        return Value;
        //    }
        //}
    }
}

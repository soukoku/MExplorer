using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Humanizer;
using Humanizer.Bytes;

namespace MExplorer.Converters
{
    public class StorageSizeConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                ByteSize sz;
                if (ByteSize.TryParse(value.ToString() + "B", out sz))
                {
                    if (parameter != null)
                    {
                        return sz.Humanize(parameter.ToString());
                    }
                    return sz.Humanize();
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

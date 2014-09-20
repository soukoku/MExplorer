using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MExplorer.Converters
{
    /// <summary>
    /// Converts selected container to app window title text.
    /// </summary>
    class AppTitleConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var container = value as ContainerVM;
            if (container != null)
            {
                return string.Format("{0} - Multi Explorer", container.Name);
            }
            return "Multi Explorer";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}

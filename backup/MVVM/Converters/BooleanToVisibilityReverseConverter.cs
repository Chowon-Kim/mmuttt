using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Samsung.AppCommon.MVVM.Converters
{
    public class BooleanToVisibilityReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility trueVisibility = Visibility.Collapsed;

            if (parameter != null && Enum.TryParse(parameter.ToString(), out Visibility parameterVisibility))
            {
                trueVisibility = parameterVisibility;
            }

            return (value is bool && (bool)value) ? trueVisibility : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is Visibility && (Visibility)value == Visibility.Collapsed);
        }
    }
}

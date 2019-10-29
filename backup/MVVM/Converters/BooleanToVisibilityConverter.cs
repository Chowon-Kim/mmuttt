using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Samsung.AppCommon.MVVM.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility falseVisibility = Visibility.Collapsed;

            if (parameter != null && Enum.TryParse(parameter.ToString(), out Visibility parameterVisibility))
            {
                falseVisibility = parameterVisibility;
            }

            return (value is bool && (bool)value) ? Visibility.Visible : falseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is Visibility && (Visibility)value == Visibility.Visible);
        }
    }
}

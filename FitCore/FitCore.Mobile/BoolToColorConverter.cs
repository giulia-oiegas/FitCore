using System.Globalization;
using Microsoft.Maui.Graphics;

namespace FitCore.Mobile.Converters;

public class BoolToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => (bool)value ? Colors.Green : Colors.Gray;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

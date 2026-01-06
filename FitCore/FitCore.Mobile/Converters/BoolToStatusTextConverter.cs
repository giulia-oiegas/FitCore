using System.Globalization;

namespace FitCore.Mobile.Converters;

public class BoolToStatusTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => (bool)value ? "ACTIV" : "EXPIRAT";

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}

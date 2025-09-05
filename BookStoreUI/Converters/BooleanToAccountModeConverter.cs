using System.Globalization;
using System.Windows.Data;

namespace BookStoreUI.Converters
{
    public class BooleanToAccountModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolean && boolean ? "Admin" : "Seller";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
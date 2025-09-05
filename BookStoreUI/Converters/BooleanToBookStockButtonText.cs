using System.Globalization;
using System.Windows.Data;

namespace BookStoreUI.Converters
{
    public class BooleanToBookStockButtonText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolean && boolean ? "Write off book" : "Add books";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
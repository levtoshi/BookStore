using System.Globalization;
using System.Windows.Data;

namespace BookStoreUI.Converters
{
    public class BooleanToBookModelButtonText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool boolean && boolean ? "Update book" : "Add new book";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
using System.Globalization;

namespace RestaurantPos.Converters
{
    public class DecimalStringConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return ((decimal)value).ToString();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)value!))
                return 0;

            try
            {
                return decimal.Parse((string)value);
            }
            catch (FormatException)
            {
                // Handle the format exception, possibly logging it or providing a default value
                return 0;
            }
        }
    }
}

using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace WeatherIconsAvaloniaSample.Converters
{
    public class NumericConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return $"{value}";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (parameter != null && parameter is string str && string.Equals(str, "int"))
            {
                if (int.TryParse((string?)value, out var intRes) == true)
                {
                    return intRes;
                }

                if (double.TryParse((string?)value, out var doubleRes) == true)
                {
                    return (int)doubleRes;
                }
            }

            if (double.TryParse((string?)value, out var res) == true)
            {
                return res;
            }

            return default;
        }
    }
}

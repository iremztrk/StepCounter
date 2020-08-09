using System;
using System.Globalization;
using Xamarin.Forms;

namespace StepCounter.Converters
{
    public class BoolToFontAtrributeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return FontAttributes.Bold;

            return FontAttributes.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
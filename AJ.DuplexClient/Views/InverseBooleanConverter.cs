using System;
using System.Globalization;
using System.Windows.Data;

namespace AJ.DuplexClient.Views
{
    /// <summary>
    /// Converts anything to bool or visibility with inverse logic (i.e. true becomes false).
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    public class InverseBooleanConverter : IValueConverter
    {
        /// <summary>Converts a value.</summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ts = BooleanConverterHelper.ConvertFromSource(value, parameter);
            var result = BooleanConverterHelper.ConvertToTargetType(ts, targetType, invert: true);
            return result;
        }

        /// <summary>DO NOT USE!</summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <exception cref="NotSupportedException">Not implemented! NOT MEANT TO!</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Not implemented! NOT MEANT TO!");
        }
    }
}

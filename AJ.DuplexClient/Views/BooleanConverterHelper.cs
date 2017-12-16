using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace AJ.DuplexClient.Views
{
    /// <summary>Converts anything to bool or visibility.</summary>
    public static class BooleanConverterHelper
    {
        /// <summary>Converts from source to a boolean value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The boolean value,</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "parameter")]
        public static bool? ConvertFromSource(object value, object parameter)
        {
            if (value == null)
                return null;

            if (value is bool)
                return (bool)value;

            var str = value as string;
            if (str != null)
                return !string.IsNullOrEmpty(str);

            var col = value as ICollection;
            if (col != null)
                return (col.Count != 0);

            if (value is DateTime)
                return (((DateTime)value) != default(DateTime));
            if (value is TimeSpan)
                return (((TimeSpan)value) != default(TimeSpan));

            var conv = value as IConvertible;
            if (conv != null)
                return conv.ToBoolean(null);

            return true;
        }

        /// <summary>Converts a boolean value to bool or visibility.</summary>
        /// <param name="sourceValue">The source value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="invert">if set to <c>true</c> the logic is inverted.</param>
        /// <returns>The respective value.</returns>
        public static object ConvertToTargetType(bool? sourceValue, Type targetType, bool invert)
        {
            if ((targetType == typeof(bool)) || (targetType == typeof(bool?)))
                return ConvertToTargetValue(sourceValue, targetType, invert);

            if (targetType == typeof(Visibility))
            {
                var b = ConvertToTargetValue(sourceValue, typeof(bool), invert);
                return b.Value ? Visibility.Visible : Visibility.Collapsed;
            }

            return null;
        }

        private static bool? ConvertToTargetValue(bool? sourceValue, Type targetType, bool invert)
        {
            if (targetType == typeof(bool))
                return ToTargetValue(sourceValue, invert, twoState: true);
            if (targetType == typeof(bool?))
                return ToTargetValue(sourceValue, invert, twoState: false);
            return null;
        }

        static bool? ToTargetValue(bool? sourceValue, bool invert, bool twoState)
        {
            if (sourceValue == null)
                return twoState ? false : (bool?)null;
            return invert ? !sourceValue.Value : sourceValue.Value;
        }
    }
}

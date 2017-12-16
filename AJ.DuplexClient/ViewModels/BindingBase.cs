using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace AJ.DuplexClient.ViewModels
{
    /// <summary>Helper class to implement <see cref="INotifyPropertyChanged" />.</summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class BindingBase : INotifyPropertyChanged
    {
        /// <summary>Occurs when a property value changes.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Notify of a property change.</summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Sets the property value.</summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="backingField">The backing field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        protected internal void SetPropertyValue<TProperty>(ref TProperty backingField, TProperty value, [CallerMemberName] string propertyName = null)
        {
            if (!IsEqual(backingField, value))
            {
                backingField = value;
                this.NotifyPropertyChanged(propertyName);
            }
        }

        /// <summary>Gets the property value.</summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="backingField">The backing field.</param>
        /// <returns>The result.</returns>
        protected internal TProperty GetPropertyValue<TProperty>(ref TProperty backingField)
        {
            return this.GetPropertyValue<TProperty>(ref backingField, null);
        }

        /// <summary>Gets the property value.</summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="backingField">The backing field.</param>
        /// <param name="defaultValue">The default value provider.</param>
        /// <returns>The result.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Symmetry with SetPropertyValue")]
        protected internal TProperty GetPropertyValue<TProperty>(ref TProperty backingField, Func<TProperty> defaultValue)
        {
            if ((defaultValue != null) && IsEqual(backingField, default(TProperty)))
            {
                // NO changed event, this is the implied value
                backingField = defaultValue();
            }

            return backingField;
        }

        /// <summary>Determines whether the specified values are equal.</summary>
        /// <typeparam name="T">The type of the values</typeparam>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns><c>true</c> if the specified values are equal; otherwise, <c>false</c>.</returns>
        private static bool IsEqual<T>(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
    }
}

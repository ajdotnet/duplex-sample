using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AJ.DuplexClient.ViewModels
{
    /// <summary>Helpers for view models.</summary>
    static class ViewModelExtensions
    {
        /// <summary>Convert an enumeration to a<see cref="ObservableCollection{T}"/>.</summary>
        /// <typeparam name="T">The type of elements in the enumeration.</typeparam>
        /// <param name="source">The source enumeration.</param>
        /// <returns>The <see cref="ObservableCollection{T}"/>.</returns>
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return new ObservableCollection<T>();
            return new ObservableCollection<T>(source);
        }

        /// <summary>Adds a range of elements to the colletion.</summary>
        /// <typeparam name="T">The type of elements in the enumeration.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="source">The element source.</param>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> source)
        {
            foreach (var item in source)
                collection.Add(item);
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using AJ.DuplexClient.Common;

namespace AJ.DuplexClient.Views
{
    /// <summary>Auto-scrolling feature for listbox.</summary>
    public static class ListBoxExtensions
    {
        public static readonly DependencyProperty AutoScrollSelectedIntoViewProperty = DependencyProperty.RegisterAttached(
            "AutoScrollSelectedIntoView", typeof(bool), typeof(ListBox), new PropertyMetadata(AutoScrollSelectedIntoViewChanged));

        public static void SetAutoScrollSelectedIntoView(UIElement element, Boolean value)
        {
            Guard.AssertNotNull(element, nameof(element));
            element.SetValue(AutoScrollSelectedIntoViewProperty, value);
        }

        public static Boolean GetAutoScrollSelectedIntoView(UIElement element)
        {
            Guard.AssertNotNull(element, nameof(element));
            return (Boolean)element.GetValue(AutoScrollSelectedIntoViewProperty);
        }

        private static void AutoScrollSelectedIntoViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lb = (ListBox)d;

            if ((bool)e.OldValue)
                lb.SelectionChanged -= ListBox_SelectionChanged;

            if ((bool)e.NewValue)
                lb.SelectionChanged += ListBox_SelectionChanged;
        }

        private static void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lb = (ListBox)sender;
            var item = e.AddedItems.Count > 0 ? e.AddedItems[0] : null;
            lb.ScrollIntoView(item);
        }
    }
}

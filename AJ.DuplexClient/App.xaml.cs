using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace AJ.DuplexClient
{
    public partial class App : Application
    {
        public App()
        {
            InitializeCulture();
        }

        private static void InitializeCulture()
        {
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}

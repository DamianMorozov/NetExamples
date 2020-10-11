using System.Windows;

namespace WPF.Net.Examples.ViewModels
{
    internal static class Utils
    {
        /// <summary>
        /// Get application settings.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static AppSettings GetSettings(FrameworkElement element, string resource = "AppSettings")
        {
            var context = element.FindResource(resource);
            if (context is AppSettings settings)
            {
                return settings;
            }
            return null;
        }
    }
}

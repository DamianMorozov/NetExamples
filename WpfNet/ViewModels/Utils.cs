using System.Windows;

namespace WpfNet.ViewModels
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
            object context = element.FindResource(resource);
            if (context is AppSettings settings)
            {
                return settings;
            }
            return null;
        }
    }
}

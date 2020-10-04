using System.Windows;

namespace WPF.WebClient.ViewModels
{
    internal static class Utils
    {
        /// <summary>
        /// Get application settings.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static AppSettings GetSettings(FrameworkElement element, string resource = "ViewModelAppSettings")
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

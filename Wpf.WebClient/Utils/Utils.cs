using System.Windows;
using Wpf.WebClient.ViewModels;

namespace Wpf.WebClient.Utils
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

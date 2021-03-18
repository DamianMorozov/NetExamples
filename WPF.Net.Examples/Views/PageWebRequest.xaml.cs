using System.Windows;
using WPF.Net.Examples.ViewModels;
// ReSharper disable UnusedMember.Global

namespace WPF.Net.Examples.Views
{
    public partial class PageWebRequest
    {
        #region Private fields and properties

        private AppSettings AppSettings { get; set; }

        #endregion

        #region Constructor and destructor

        public PageWebRequest()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void PageWebRequest_Loaded(object sender, RoutedEventArgs e)
        {
            AppSettings = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonWebRequest_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.WebRequest.OpenTask(webBrowser, fieldOut);
        }

        #endregion
    }
}

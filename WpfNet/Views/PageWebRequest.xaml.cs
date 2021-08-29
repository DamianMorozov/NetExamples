using System.Windows;
using WpfNet.ViewModels;
// ReSharper disable UnusedMember.Global

namespace WpfNet.Views
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

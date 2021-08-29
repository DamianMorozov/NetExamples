using System.Windows;
using WpfNet.ViewModels;

// ReSharper disable UnusedMember.Global

namespace WpfNet.Views
{
    public partial class PageWebClient
    {
        #region Private fields and properties

        private AppSettings AppSettings { get; set; }

        #endregion

        #region Constructor and destructor

        public PageWebClient()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void PageWebClient_Loaded(object sender, RoutedEventArgs e)
        {
            AppSettings = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonFileDownload_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.WebClient.Download(fieldOut, false, progressBar);
        }

        private void ButtonFileDownloadAsync_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.WebClient.Download(fieldOut, true, progressBar);
        }

        private void ButtonFileDownloadWithProgress_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.WebClient.DownloadWithBuffer(fieldOut, progressBar);
        }

        #endregion
    }
}

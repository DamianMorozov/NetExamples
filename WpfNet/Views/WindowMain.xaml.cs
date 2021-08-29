using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using WpfNet.Utils;
using WpfNet.ViewModels;
// ReSharper disable UnusedMember.Global

namespace WpfNet.Views
{
    public partial class WindowMain
    {
        #region Private fields and properties

        private AppSettings AppSettings { get; set; }

        #endregion

        #region Constructor and destructor

        public WindowMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void LaunchGitHubSite(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                Process.Start("https://github.com/DamianMorozov/NetExamples");
            });
        }

        private void LaunchNuGetSite(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                Process.Start("https://www.nuget.org/packages/Net.Utils/");
            });
        }

        private void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            AppSettings = ViewModels.Utils.GetSettings(this);
            AppSettings.WindowMain = this;
            AppSettings.DefaultTheme();
        }

        private void ButtonWebClient_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.WebClient;
        }

        private void ButtonHttpClient_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.HttpClient;
        }

        private void ButtonProxy_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.Proxy;
        }

        private void ButtonPing_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.Ping;
        }

        private void ButtonWebRequest_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.WebRequest;
        }

        private void ButtonChangelog_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.Changelog;
        }

        private void buttonTheme_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.AppTheme;
        }

        private void ButtonBrowseSharp_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.BrowseSharp;
        }
        
        #endregion

        private void ButtonWebParse_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ActivePage = Enums.WpfPage.WebParse;
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using WPF.Net.Examples.Models;
using WPF.Net.Examples.ViewModels;
// ReSharper disable UnusedMember.Global

namespace WPF.Net.Examples.Views
{
    public partial class WindowMain
    {
        #region Private fields and properties

        private AppSettings _appSet;

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
                Process.Start("https://github.com/DamianMorozov/Net.Examples");
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
            _appSet = ViewModels.Utils.GetSettings(this);
            _appSet.WindowMain = this;
            _appSet.DefaultTheme();
        }

        private void ButtonWebClient_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = Enums.WpfPage.WebClient;
        }

        private void ButtonHttpClient_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = Enums.WpfPage.HttpClient;
        }

        private void ButtonProxy_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = Enums.WpfPage.Proxy;
        }

        private void ButtonPing_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = Enums.WpfPage.Ping;
        }

        private void ButtonWebRequest_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = Enums.WpfPage.WebRequest;
        }

        private void ButtonChangelog_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = Enums.WpfPage.Changelog;
        }

        private void buttonTheme_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = Enums.WpfPage.AppTheme;
        }

        #endregion
    }
}

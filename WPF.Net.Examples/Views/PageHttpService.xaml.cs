using System;
using System.Threading.Tasks;
using System.Windows;
using WPF.Net.Examples.ViewModels;
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Local

// ReSharper disable UnusedMember.Global

namespace WPF.Net.Examples.Views
{
    public partial class PageHttpService
    {
        #region Private fields and properties

        private AppSettings _appSet;

        #endregion

        #region Constructor and destructor

        public PageHttpService()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void PageHttpService_Loaded(object sender, RoutedEventArgs e)
        {
            _appSet = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonHttpGetWithoutProxy_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await _appSet.HttpService.Get(_appSet.HttpService.Url, TimeSpan.FromSeconds(0), fieldOut, false, null);
            });
        }
        private void ButtonHttpGetWithProxy_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await _appSet.HttpService.Get(_appSet.HttpService.Url, TimeSpan.FromSeconds(0), fieldOut, true, _appSet.Proxy);
            });
        }
        private void ButtonHttpGetWithoutProxyTimeout_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await _appSet.HttpService.Get(_appSet.HttpService.Url, TimeSpan.FromSeconds(_appSet.HttpService.Timeout), fieldOut, false, null);
            });
        }

        private void ButtonHttpGetWithProxyTimeout_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                await _appSet.HttpService.Get(_appSet.HttpService.Url, TimeSpan.FromSeconds(_appSet.HttpService.Timeout), fieldOut, false, null);
            });
        }

        #endregion
    }
}

using System.Windows;
using WPF.Net.Examples.ViewModels;

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
            _appSet.Proxy.Use = false;
            _appSet.HttpService.Get(_appSet.HttpService.Url, fieldOut, _appSet.Proxy, false);
        }
        private void ButtonHttpGetWithProxy_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.Proxy.Use = true;
            _appSet.HttpService.Get(_appSet.HttpService.Url, fieldOut, _appSet.Proxy, false);
        }
        private void ButtonHttpGetWithoutProxyTimeout_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.Proxy.Use = false;
            _appSet.HttpService.Get(_appSet.HttpService.Url, fieldOut, _appSet.Proxy, true);
        }

        private void ButtonHttpGetWithProxyTimeout_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.Proxy.Use = false;
            _appSet.HttpService.Get(_appSet.HttpService.Url, fieldOut, _appSet.Proxy, true);
        }

        #endregion
    }
}

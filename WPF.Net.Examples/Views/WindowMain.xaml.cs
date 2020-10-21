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

        private void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            _appSet = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonWebClient_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = EnumWpfPage.WebClient;
        }

        private void ButtonHttpService_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = EnumWpfPage.HttpService;
        }

        private void ButtonProxy_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = EnumWpfPage.Proxy;
        }
        

        private void ButtonPing_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = EnumWpfPage.Ping;
        }

        private void ButtonWebRequest_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ActivePage = EnumWpfPage.WebRequest;
        }
        
        #endregion
    }
}

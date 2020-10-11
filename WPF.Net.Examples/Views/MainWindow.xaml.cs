using System.Windows;
using WPF.Net.Examples.Models;
using WPF.Net.Examples.ViewModels;
// ReSharper disable UnusedMember.Global

namespace WPF.Net.Examples.Views
{
    public partial class MainWindow
    {
        #region Private fields and properties

        private AppSettings _appSet;

        #endregion

        #region Constructor and destructor

        public MainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _appSet = ViewModels.Utils.GetSettings(this);
        }

        private void buttonWebClient_OnClick(object sender, RoutedEventArgs e)
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
        
        #endregion
    }
}

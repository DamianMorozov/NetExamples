using System.Windows;
using WPF.Net.Examples.ViewModels;

namespace WPF.Net.Examples.Views
{
    public partial class PageHttpClient
    {
        #region Private fields and properties

        private AppSettings _appSet;

        #endregion

        #region Constructor and destructor

        public PageHttpClient()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void PageHttpService_Loaded(object sender, RoutedEventArgs e)
        {
            _appSet = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonHttpGet_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.HttpClient.OpenTask(fieldTaskWait.IsChecked == true, _appSet.Proxy);
            fieldTaskStop.IsChecked = _appSet.HttpClient.TaskStop;
            fieldStatus.Text = _appSet.HttpClient.Status;
            fieldContent.Text = _appSet.HttpClient.Content;
        }

        #endregion
    }
}

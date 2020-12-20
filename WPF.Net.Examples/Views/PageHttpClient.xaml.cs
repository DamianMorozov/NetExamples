using System.Threading.Tasks;
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
            var task = Task.Run(async () =>
            {
                await _appSet.HttpClient.OpenAsync(_appSet.Proxy).ConfigureAwait(false);
            });
            task.Wait();
        }

        #endregion
    }
}

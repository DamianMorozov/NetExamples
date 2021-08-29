using System.Threading.Tasks;
using System.Windows;
using WpfNet.ViewModels;

namespace WpfNet.Views
{
    public partial class PageHttpClient
    {
        #region Private fields and properties

        private AppSettings AppSettings { get; set; }

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
            AppSettings = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonHttpGet_OnClick(object sender, RoutedEventArgs e)
        {
            Task task = Task.Run(async () =>
            {
                await AppSettings.HttpClient.OpenAsync(AppSettings.Proxy).ConfigureAwait(false);
            });
            task.Wait();
        }

        #endregion
    }
}

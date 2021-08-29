using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfNet.ViewModels;
using Key = System.Windows.Input.Key;

// ReSharper disable NotAccessedField.Local

namespace WpfNet.Views
{
    public partial class PagePing
    {
        #region Private fields and properties

        private AppSettings AppSettings { get; set; }

        #endregion

        #region Constructor and destructor

        public PagePing()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void PageProxy_Loaded(object sender, RoutedEventArgs e)
        {
            AppSettings = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonHostAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var host = WPF.Utils.InvokeTextBox.GetText(fieldHost);
            if (!string.IsNullOrEmpty(host))
            {
                WPF.Utils.InvokeTextBox.Clear(fieldHost);
                if (!listBoxHosts.Items.Contains(host))
                    WPF.Utils.InvokeListBox.ItemAdd(listBoxHosts, host);
            }
        }

        private void ButtonPingStart_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.Ping.Hosts.Clear();
            foreach (object host in listBoxHosts.Items)
            {
                AppSettings.Ping.Hosts.Add(host.ToString());
            }
            AppSettings.Ping.OpenAsync();
        }

        private void ButtonPingStop_OnClick(object sender, RoutedEventArgs e)
        {
            Task _ = Task.Run(async () =>
            {
                await AppSettings.Ping.CloseAsync().ConfigureAwait(false);
            });
        }

        private void ButtonHostsClear_OnClick(object sender, RoutedEventArgs e)
        {
            WPF.Utils.InvokeListBox.ItemsClear(listBoxHosts);
            AppSettings.Ping.Log = string.Empty;
        }
        
        private void FieldHost_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp && e.Key == Key.Enter)
                ButtonHostAdd_OnClick(sender, e);
        }

        #endregion
    }
}

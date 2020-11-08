using System.Windows;
using WPF.Net.Examples.ViewModels;
// ReSharper disable NotAccessedField.Local

namespace WPF.Net.Examples.Views
{
    public partial class PagePing
    {
        #region Private fields and properties

        private AppSettings _appSet;

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
            _appSet = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonHostAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var host = Utils.InvokeTextBox.GetText(fieldIp);
            if (!string.IsNullOrEmpty(host))
            {
                Utils.InvokeTextBox.Clear(fieldIp);
                Utils.InvokeListBox.ItemAdd(listBoxHosts, host);
            }
        }

        private void ButtonPingStartOne_OnClick(object sender, RoutedEventArgs e)
        {
            foreach (var host in listBoxHosts.Items)
            {
                _appSet.Ping.Hosts.Add(host.ToString(), false);
            }
            _appSet.Ping.OpenTask();
            fieldIsTaskFinished.IsChecked = _appSet.HttpClient.IsTaskActive;
            fieldOut.Text = _appSet.Ping.Status;
        }

        private void ButtonPingStop_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.Ping.Close();
        }

        private void ButtonHostsClear_OnClick(object sender, RoutedEventArgs e)
        {
            Utils.InvokeListBox.ItemsClear(listBoxHosts);
        }

        #endregion
    }
}

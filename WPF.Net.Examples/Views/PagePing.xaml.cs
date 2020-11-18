using System.Windows;
using System.Windows.Input;
using WPF.Net.Examples.ViewModels;
using Key = System.Windows.Input.Key;

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
            var host = Utils.InvokeTextBox.GetText(fieldHost);
            if (!string.IsNullOrEmpty(host))
            {
                Utils.InvokeTextBox.Clear(fieldHost);
                if (!listBoxHosts.Items.Contains(host))
                    Utils.InvokeListBox.ItemAdd(listBoxHosts, host);
            }
        }

        private void ButtonPingStart_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.Ping.Hosts.Clear();
            foreach (var host in listBoxHosts.Items)
            {
                _appSet.Ping.Hosts.Add(host.ToString());
            }
            _appSet.Ping.OpenTask(fieldTaskWait.IsChecked == true);
            fieldTaskStop.IsChecked = _appSet.Ping.TaskStop;
        }

        private void ButtonPingStop_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.Ping.Close();
        }

        private void ButtonHostsClear_OnClick(object sender, RoutedEventArgs e)
        {
            Utils.InvokeListBox.ItemsClear(listBoxHosts);
        }
        
        private void FieldHost_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp && e.Key == Key.Enter)
                ButtonHostAdd_OnClick(sender, e);
        }

        private void ButtonGetStatus_OnClick(object sender, RoutedEventArgs e)
        {
            fieldOut.Text = _appSet.Ping.Status;
        }

        #endregion
    }
}

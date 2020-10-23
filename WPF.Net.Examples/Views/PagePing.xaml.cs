using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
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
            var ip = Utils.InvokeTextBox.GetText(fieldIp);
            Utils.InvokeTextBox.Clear(fieldIp);
            Utils.InvokeListBox.ItemAdd(listBoxHosts, ip);
        }

        private void ButtonPingStartOne_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.Ping.OpenTask(false, listBoxHosts, fieldOut);
        }

        private void ButtonPingStartRepeat_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.Ping.OpenTask(true, listBoxHosts, fieldOut);
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

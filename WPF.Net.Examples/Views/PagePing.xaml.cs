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
        private Task _task;
        private readonly int _timeout = 5_000;
        private bool _isActive;

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

        private void ButtonPingStart_OnClick(object sender, RoutedEventArgs e)
        {
            if (_task is null)
            {
                _task = Task.Run(async () =>
                {
                    _isActive = true;
                    while (_isActive)
                    {
                        PingWork();
                        await Task.Delay(TimeSpan.FromMilliseconds(_timeout));
                    }
                });
            }
        }

        private void PingWork()
        {
            var dt = DateTime.Now;
            Utils.InvokeTextBox.SetText(fieldOut, $"{dt.Hour}:{dt.Minute}:{dt.Second}  The task is started.{Environment.NewLine}");
            var items = Utils.InvokeListBox.ItemsGet(listBoxHosts);
            using (var ping = new Ping())
            {
                foreach (var item in items)
                {
                    var ip = string.Empty;
                    try
                    {
                        ip = item.ToString();
                        var reply = ping.Send(ip.Trim());
                        if (reply != null)
                            Utils.InvokeTextBox.AddText(fieldOut, $"Pinging {ip}: {reply.Status}");
                        Task.Delay(TimeSpan.FromMilliseconds(_timeout));
                    }
                    catch (Exception ex)
                    {
                        Utils.InvokeTextBox.AddText(fieldOut, $"Pinging {ip} exception: {ex.Message}");
                        if (!(ex.InnerException is null))
                            Utils.InvokeTextBox.AddText(fieldOut, $"Pinging {ip} inner exception: {ex.InnerException.Message}");
                    }
                }
                Utils.InvokeTextBox.AddText(fieldOut, $"Waiting {_timeout} milliseconds");
            }
        }

        private void ButtonPingStop_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(async () =>
            {
                _isActive = false;
                while (!(_task is null) && !_task.IsCompleted)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(_timeout));
                }
                _task?.Dispose();
                _task = null;
                var dt = DateTime.Now;
                Utils.InvokeTextBox.SetText(fieldOut, $"{dt.Hour}:{dt.Minute}:{dt.Second}  The task is stopped.{Environment.NewLine}");
            });
        }

        private void ButtonHostsClear_OnClick(object sender, RoutedEventArgs e)
        {
            Utils.InvokeListBox.ItemsClear(listBoxHosts);
        }

        #endregion
    }
}

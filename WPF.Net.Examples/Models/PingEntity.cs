using System;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF.Net.Examples.Models
{
    public class PingEntity
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string memberName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        #endregion

        #region Public fields and properties

        private Task _task;
        private bool _isActive;

        private int _timeout;
        public int Timeout
        {
            get => _timeout;
            set
            {
                _timeout = value;
                OnPropertyRaised();
            }
        }

        #endregion

        #region Constructor and destructor

        public PingEntity()
        {
            SetupDefault();
        }

        public void SetupDefault()
        {
            Timeout = 2_500;
        }

        #endregion

        #region Public and private methods

        public void OpenTask(bool isRepeat, ListBox listBoxHosts, TextBox fieldOut)
        {
            if (!(_task is null))
            {
                if (_task.Status == TaskStatus.RanToCompletion)
                {
                    _task.Dispose();
                    _task = null;
                }
            }
            _task = Task.Run(async () =>
            {
                await CreateTaskAsync(isRepeat, listBoxHosts, fieldOut);
            });
        }

        private async Task CreateTaskAsync(bool isRepeat, ListBox listBoxHosts, TextBox fieldOut)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(true);
            var ip = string.Empty;
            try
            {
                _isActive = true;
                do
                {
                    var dt = DateTime.Now;
                    Utils.InvokeTextBox.SetText(fieldOut,
                        $"{dt.Hour}:{dt.Minute}:{dt.Second}  The task is started.{Environment.NewLine}");
                    var items = Utils.InvokeListBox.ItemsGet(listBoxHosts);
                    using (var ping = new Ping())
                    {
                        foreach (var item in items)
                        {
                            try
                            {
                                ip = item.ToString();
                                var reply = ping.Send(ip.Trim());
                                if (reply != null)
                                    Utils.InvokeTextBox.AddText(fieldOut, $"Pinging {ip}: {reply.Status}");
                            }
                            catch (Exception ex)
                            {
                                Utils.InvokeTextBox.AddText(fieldOut, $"Ping {ip} exception: {ex.Message}");
                                if (!(ex.InnerException is null))
                                    Utils.InvokeTextBox.AddText(fieldOut, $"Ping {ip} inner exception: {ex.InnerException.Message}");
                            }
                        }
                        Utils.InvokeTextBox.AddText(fieldOut, $"Waiting {_timeout} milliseconds");
                        await Task.Delay(TimeSpan.FromMilliseconds(_timeout)).ConfigureAwait(true);
                    }
                } while (_isActive && isRepeat);
            }
            catch (Exception ex)
            {
                Utils.InvokeTextBox.AddText(fieldOut, $"Ping {ip} exception: {ex.Message}");
                if (!(ex.InnerException is null))
                    Utils.InvokeTextBox.AddText(fieldOut, $"Ping {ip} inner exception: {ex.InnerException.Message}");
            }
            finally
            {
                var dt = DateTime.Now;
                Utils.InvokeTextBox.AddText(fieldOut, $"{dt.Hour}:{dt.Minute}:{dt.Second}  The task is stopped.{Environment.NewLine}");
            }
        }

        public void Close()
        {
            _isActive = false;
        }

        #endregion
    }
}

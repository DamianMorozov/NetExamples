using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Utils;
// ReSharper disable LocalizableElement

namespace WPF.Net.Examples.Models
{
    public class HttpServiceEntity
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Public fields and properties

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

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyRaised();
            }
        }

        private Task _task;

        #endregion

        #region Constructor and destructor

        public HttpServiceEntity()
        {
            SetupDefault();
        }

        public void SetupDefault()
        {
            Timeout = 5_000;
            Url = @"http://webcode.me";
        }

        #endregion

        #region Public methods

        public void Get(string url, TextBox fieldOut, bool useProxy, Proxy proxy, bool isTimeout)
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
                await GetAsync(url, fieldOut, useProxy, proxy, isTimeout);
            });
        }

        public async Task GetAsync(string url, TextBox fieldOut, bool useProxy, Proxy proxy, bool isTimeout)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(true);
            InvokeTextBox.Clear(fieldOut);
            var sw = Stopwatch.StartNew();
            try
            {
                InvokeTextBox.AddTextFormat(fieldOut, sw, $"Get started. Use proxy = [{useProxy}]. Timeout = [{Timeout}].");
                InvokeTextBox.AddTextFormat(fieldOut, sw, $"Url = [{url}]");
                using (var httpClient = GetHttpClient(useProxy, proxy))
                {
                    if (isTimeout)
                        httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout);
                    var response = await httpClient.GetAsync(url);
                    InvokeTextBox.AddTextFormat(fieldOut, sw, $"Status code: {response.StatusCode}");
                    var rawContent = await response.Content.ReadAsStringAsync();
                    InvokeTextBox.AddTextFormat(fieldOut, sw, $"Response Content: {rawContent}");
                    InvokeTextBox.AddTextFormat(fieldOut, sw, $"response.IsSuccessStatusCode : {response.IsSuccessStatusCode }");
                }
                InvokeTextBox.AddTextFormat(fieldOut, sw, "Get finished.");
            }
            catch (Exception ex)
            {
                InvokeTextBox.AddTextFormat(fieldOut, sw, ex.Message);
                InvokeTextBox.AddTextFormat(fieldOut, sw, ex.StackTrace);
                if (ex.InnerException != null)
                {
                    InvokeTextBox.AddTextFormat(fieldOut, sw, ex.InnerException.Message);
                }
            }
            sw.Stop();
        }

        public HttpClient GetHttpClient(bool useProxy, Proxy proxy)
        {
            if (!useProxy)
            {
                return new HttpClient(new HttpClientHandler { UseProxy = false });
            }

            if (proxy is null)
                throw new ArgumentException("Poxy is empty!", nameof(proxy));
            var handler = new HttpClientHandler()
            {
                UseProxy = true,
                Proxy = new WebProxy(proxy.Host),
            };
            var httpClient = new HttpClient(handler);
            if (proxy.UseDefaultCreds)
            {
                handler.UseDefaultCredentials = true;
            }
            else if (!string.IsNullOrWhiteSpace(proxy.Username) && !string.IsNullOrWhiteSpace(proxy.Password))
            {
                handler.PreAuthenticate = false;
                handler.UseDefaultCredentials = false;
                handler.Credentials = !string.IsNullOrWhiteSpace(proxy.Domain)
                    ? new NetworkCredential(proxy.Username, proxy.Password, proxy.Domain)
                    : new NetworkCredential(proxy.Username, proxy.Password);
            }
            return httpClient;
        }

        #endregion
    }
}

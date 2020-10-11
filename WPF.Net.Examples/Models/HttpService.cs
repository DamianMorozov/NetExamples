using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Utils;

namespace WPF.Net.Examples.Models
{
    public class HttpService
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Public fields and properties

        private Proxy _proxy;
        public Proxy Proxy
        {
            get => _proxy;
            set
            {
                _proxy = value;
                OnPropertyRaised();
            }
        }

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

        #endregion

        #region Constructor and destructor

        public HttpService()
        {
            SetupDefault();
        }

        public void SetupDefault()
        {
            Proxy = new Proxy();
            Timeout = 5;
            Url = @"http://webcode.me";
        }

        #endregion

        #region Public methods

        public async Task Get(string url, bool useProxy, TimeSpan timeout, TextBox fieldOut)
        {
            InvokeTextBox.Clear(fieldOut);
            var sw = Stopwatch.StartNew();
            try
            {
                InvokeTextBox.AddTextFormat(fieldOut, sw, $"Get started. Use proxy = [{useProxy}]. Timeout = [{timeout}].");
                InvokeTextBox.AddTextFormat(fieldOut, sw, $"Url = [{url}]");
                using (var httpClient = GetHttpClient(useProxy))
                {
                    if ((int)timeout.TotalSeconds != 0)
                    {
                        httpClient.Timeout = timeout;
                    }
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

        public HttpClient GetHttpClient(bool useProxy)
        {
            if (!useProxy)
            {
                return new HttpClient(new HttpClientHandler { UseProxy = false });
            }

            var handler = new HttpClientHandler()
            {
                UseProxy = true,
                Proxy = new WebProxy(Proxy.Host),
            };
            var httpClient = new HttpClient(handler);
            if (Proxy.UseDefaultCreds)
            {
                handler.UseDefaultCredentials = true;
            }
            else if (!string.IsNullOrWhiteSpace(Proxy.Username) && !string.IsNullOrWhiteSpace(Proxy.Password))
            {
                handler.PreAuthenticate = false;
                handler.UseDefaultCredentials = false;
                handler.Credentials = !string.IsNullOrWhiteSpace(Proxy.Domain)
                    ? new NetworkCredential(Proxy.Username, Proxy.Password, Proxy.Domain)
                    : new NetworkCredential(Proxy.Username, Proxy.Password);
            }
            return httpClient;
        }

        #endregion
    }
}

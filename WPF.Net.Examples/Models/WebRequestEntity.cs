using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Utils;

namespace WPF.Net.Examples.Models
{
    public class WebRequestEntity
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Public fields and properties

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
        private int _timeout = 100;

        #endregion

        #region Constructor and destructor

        public WebRequestEntity()
        {
            SetupDefault();
        }

        public void SetupDefault()
        {
            Url = @"https://www.google.com";
        }

        #endregion

        #region Public methods

        public void OpenTask(WebBrowser webBrowser, bool isLogin, string login, string password, TextBox textBox)
        {
            if (_task is null)
            {
                _task = Task.Run(async () =>
                {
                    Work(webBrowser, isLogin, login, password, textBox);
                    await Task.Delay(TimeSpan.FromMilliseconds(_timeout));
                });
            }
        }

        private void Work(WebBrowser webBrowser, bool isLogin, string login, string password, TextBox textBox)
        {
            var sw = Stopwatch.StartNew();
            InvokeTextBox.AddTextFormat(textBox, sw, @"Work is started.");
            try
            {
                //webBrowser.Url = null;
                //webBrowser.DataBindings.Clear();
                InvokeWebBrowser.SetSource(webBrowser, null);
                //webBrowser.DataContext = null;
                InvokeTextBox.Clear(textBox);
                var webRequest = WebRequest.Create(Url);
                if (isLogin)
                    webRequest.Credentials = new NetworkCredential(login, password);

                using (var webResponse = webRequest.GetResponse())
                {
                    // Headers
                    var headers = webResponse.Headers;
                    for (var i = 0; i < headers.Count; i++)
                    {
                        InvokeTextBox.AddTextFormat(textBox, sw, $@"{headers.GetKey(i)}: {headers[i]}");
                    }

                    InvokeTextBox.AddTextFormat(textBox, sw,
                        @"StatusDescription = " + ((HttpWebResponse) webResponse).StatusDescription);

                    //using (var stream = webResponse.GetResponseStream())
                    //{
                    //    using (var streamReader = new StreamReader(stream ?? throw new InvalidOperationException()))
                    //    {
                    //        webBrowser.Text = streamReader.ReadToEnd();
                    //        streamReader.Close();
                    //    }
                    //}
                    InvokeWebBrowser.SetSource(webBrowser, webRequest.RequestUri);
                    webResponse.Close();
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var httpResponse = (HttpWebResponse) ex.Response;
                    InvokeTextBox.AddTextFormat(textBox, sw,
                        @"WebException.ProtocolError: " +
                        $@"{(int) httpResponse.StatusCode} - {httpResponse.StatusCode}");
                }
                else
                {
                    InvokeTextBox.AddTextFormat(textBox, sw, @"WebException: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                InvokeTextBox.AddTextFormat(textBox, sw, @"Exception: " + ex.Message);
            }
            finally
            {
                InvokeTextBox.AddTextFormat(textBox, sw, @"Work is finished.");
            }
            sw.Stop();
        }


        #endregion
    }
}

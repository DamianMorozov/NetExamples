using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Utils;

namespace WPF.Net.Examples.Models
{
    internal class WebRequestEntity
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

        private string _urlPost;
        public string UrlPost
        {
            get => _urlPost;
            set
            {
                _urlPost = value;
                OnPropertyRaised();
            }
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyRaised();
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyRaised();
            }
        }

        private bool _postMethod;
        public bool PostMethod
        {
            get => _postMethod;
            set
            {
                _postMethod = value;
                OnPropertyRaised();
            }
        }

        private string _postQuery;
        public string PostQuery
        {
            get => _postQuery;
            set
            {
                _postQuery = value;
                OnPropertyRaised();
            }
        }

        private Task _task;

        #endregion

        #region Constructor and destructor

        public WebRequestEntity()
        {
            SetupDefault();
        }

        public void SetupDefault()
        {
            Url = @"https://www.google.com";
            UrlPost = @"https://httpbin.org/post";
            Login = string.Empty;
            Password = string.Empty;
            PostMethod = false;
            PostQuery = @"";
        }

        #endregion

        #region Public and private methods

        public void OpenTask(WebBrowser webBrowser, TextBox textBox)
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
                await CreateTaskAsync(webBrowser, textBox);
            });
        }

        private async Task CreateTaskAsync(WebBrowser webBrowser, TextBox textBox)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(true);
            var sw = Stopwatch.StartNew();
            InvokeTextBox.AddTextFormat(textBox, sw, @"Work is started.");
            try
            {
                InvokeWebBrowser.SetSource(webBrowser, null);
                InvokeTextBox.Clear(textBox);
                WebRequest webRequest;
                
                if (PostMethod)
                {
                    webRequest = WebRequest.Create(UrlPost);
                    webRequest.Method = @"POST";
                    var byteArray = System.Text.Encoding.UTF8.GetBytes(PostQuery);
                    webRequest.ContentType = @"application/x-www-form-urlencoded";
                    webRequest.ContentLength = byteArray.Length;
                    using var dataStream = await webRequest.GetRequestStreamAsync();
                    await dataStream.WriteAsync(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                else
                {
                    webRequest = WebRequest.Create(Url);
                }

                if (!string.IsNullOrEmpty(Login))
                    webRequest.Credentials = new NetworkCredential(Login, Password);

                using var webResponse = await webRequest.GetResponseAsync();
                var headers = webResponse.Headers;
                for (var i = 0; i < headers.Count; i++)
                {
                    InvokeTextBox.AddTextFormat(textBox, sw, $@"{headers.GetKey(i)}: {headers[i]}");
                }
                InvokeTextBox.AddTextFormat(textBox, sw, @"StatusDescription = " + ((HttpWebResponse)webResponse).StatusDescription);
                InvokeWebBrowser.SetSource(webBrowser, webRequest.RequestUri);
                webResponse.Close();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var httpResponse = (HttpWebResponse)ex.Response;
                    InvokeTextBox.AddTextFormat(textBox, sw,
                        @"WebException.ProtocolError: " +
                        $@"{(int)httpResponse.StatusCode} - {httpResponse.StatusCode}");
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

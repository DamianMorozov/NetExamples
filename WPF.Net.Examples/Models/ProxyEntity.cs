using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF.Net.Examples.Models
{
    internal class ProxyEntity : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string memberName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        #endregion

        #region Public fields and properties

        private bool _use;
        public bool Use
        {
            get => _use;
            set
            {
                _use = value;
                OnPropertyRaised();
            }
        }
        private bool _useDefaultCredentials;
        public bool UseDefaultCredentials
        {
            get => _useDefaultCredentials;
            set
            {
                _useDefaultCredentials = value;
                OnPropertyRaised();
            }
        }
        private Uri _host;
        public Uri Host
        {
            get => _host;
            set
            {
                if (!value.ToString().Contains("http://") && !value.ToString().Contains("https://"))
                    value = new Uri("http://" + value);
                _host = value;
                OnPropertyRaised();
            }
        }
        private int _port;
        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                OnPropertyRaised();
            }
        }
        private string _domain;
        public string Domain
        {
            get => _domain;
            set
            {
                _domain = value;
                OnPropertyRaised();
            }
        }
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
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

        #endregion

        #region Constructor and destructor

        public ProxyEntity()
        {
            SetupDefault();
        }

        public ProxyEntity(bool use, bool useDefaultCredentials, Uri host, int port,
            string domain, string username, string password)
        {
            Setup(use, useDefaultCredentials, host, port, domain, username, password);
        }

        public void SetupDefault()
        {
            Setup(false, false, new Uri(@"http://localhost"), 8080,
                    string.Empty, string.Empty, string.Empty);
        }

        public void Setup(bool use, bool useDefaultCredentials, Uri host, int port,
            string domain, string username, string password)
        {
            UseDefaultCredentials = useDefaultCredentials;
            Host = host;
            Port = port;
            Use = use;
            Domain = domain;
            Username = username;
            Password = password;
        }

        #endregion
    }
}

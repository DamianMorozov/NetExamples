using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF.WebClient.ViewModels
{
    /// <summary>
    /// Программные настройки.
    /// </summary>
    public class AppSettings : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Constructor

        public AppSettings()
        {
            Default();
        }

        #endregion

        #region Private fields and properties

        private Uri _link;
        public Uri Link
        {
            get => _link;
            set
            {
                _link = value;
                OnPropertyRaised();
            }
        }

        private long _bufferSize;
        public long BufferSize
        {
            get => _bufferSize;
            set
            {
                _bufferSize = value;
                OnPropertyRaised();
            }
        }

        #endregion

        #region Public methods

        public void Default()
        {
            Link = new Uri(@"https://downloadmaster.ru/dm/download/dmaster.exe");
            BufferSize = 10_240;
        }

        #endregion
    }
}


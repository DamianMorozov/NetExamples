using Net.Utils;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Navigation;
using ControlzEx.Theming;
using WPF.Net.Examples.Models;
using WPF.Net.Examples.Views;

namespace WPF.Net.Examples.ViewModels
{
    internal class AppSettings : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Public and private fields and properties

        public HttpClientEntity HttpClient { get; set; }
        public ProxyEntity Proxy { get; set; }
        public WebClientEntity WebClient { get; set; }
        public WebRequestEntity WebRequest { get; set; }
        public PingEntity Ping { get; set; }

        private string _changeLog;
        public string ChangeLog
        {
            get => _changeLog;
            set
            {
                _changeLog = value;
                OnPropertyRaised();
            }
        }

        private Enums.ThemePrimary _themePrimary;
        public Enums.ThemePrimary  ThemePrimary
        {
            get => _themePrimary;
            set
            {
                _themePrimary = value;
                OnPropertyRaised();
                if (!(WindowMain is null))
                {
                    ThemeManager.Current.ChangeTheme(WindowMain, $"{_themePrimary}.{_themeColor}");
                }
            }
        }

        private Enums.ThemeColor _themeColor;
        public Enums.ThemeColor ThemeColor
        {
            get => _themeColor;
            set
            {
                _themeColor = value;
                if (!(WindowMain is null))
                {
                    ThemeManager.Current.ChangeTheme(WindowMain, $"{_themePrimary}.{_themeColor}");
                }
                OnPropertyRaised();
            }
        }

        #endregion

        #region Constructor and destructor

        public AppSettings() { SetupDefault(); }

        public void SetupDefault()
        {
            Frame = new Frame
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden
            };
            // MahApps theme.
            DefaultTheme();
            HttpClient = new HttpClientEntity { Host = new Uri("http://google.com/") };
            Proxy = new ProxyEntity();
            WebClient = new WebClientEntity();
            WebRequest = new WebRequestEntity();
            Ping = new PingEntity();
            // ChangeLog
            var fileName = "CHANGELOG.md";
            if (File.Exists(fileName))
            {
                using var sr = new StreamReader(fileName);
                ChangeLog = sr.ReadToEnd();
            }
        }

        #endregion

        #region Private fields and properties - GUI

        private Frame _frame;
        public Frame Frame
        {
            get => _frame;
            set
            {
                _frame = value;
                OnPropertyRaised();
            }
        }

        private PageWebClient _pageWebClient;
        public PageWebClient PageWebClient
        {
            get => _pageWebClient;
            set
            {
                _pageWebClient = value;
                OnPropertyRaised();
            }
        }

        private PageHttpClient _pageHttpService;
        public PageHttpClient PageHttpService
        {
            get => _pageHttpService;
            set
            {
                _pageHttpService = value;
                OnPropertyRaised();
            }
        }

        private PageProxy _pageProxy;

        private PagePing _pagePing;
        public PagePing PagePing
        {
            get => _pagePing;
            set
            {
                _pagePing = value;
                OnPropertyRaised();
            }
        }

        private PageWebRequest _pageWebRequest;
        public PageWebRequest PageWebRequest
        {
            get => _pageWebRequest;
            set
            {
                _pageWebRequest = value;
                OnPropertyRaised();
            }
        }

        private PageChangelog _pageChangelog;
        public PageChangelog PageChangelog
        {
            get => _pageChangelog;
            set
            {
                _pageChangelog = value;
                OnPropertyRaised();
            }
        }

        private PageAppTheme _pageAppTheme;
        public PageAppTheme PageAppTheme
        {
            get => _pageAppTheme;
            set
            {
                _pageAppTheme = value;
                OnPropertyRaised();
            }
        }

        private System.Windows.Window _window;
        public System.Windows.Window WindowMain
        {
            get => _window;
            set
            {
                _window = value;
                OnPropertyRaised();
            }
        }

        private Enums.WpfPage _activePage;
        public Enums.WpfPage ActivePage
        {
            get => _activePage;
            set
            {
                switch (_activePage = value)
                {
                    case Enums.WpfPage.WebClient:
                        PageWebClient ??= new PageWebClient();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageWebClient))
                                Frame.Navigate(PageWebClient);
                        }
                        else
                            Frame.Navigate(PageWebClient);
                        break;
                    case Enums.WpfPage.HttpClient:
                        PageHttpService ??= new PageHttpClient();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageHttpClient))
                                Frame.Navigate(PageHttpService);
                        }
                        else
                            Frame.Navigate(PageHttpService);
                        break;
                    case Enums.WpfPage.Proxy:
                        _pageProxy ??= new PageProxy();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageProxy))
                                Frame.Navigate(_pageProxy);
                        }
                        else
                            Frame.Navigate(_pageProxy);
                        break;
                    case Enums.WpfPage.Ping:
                        PagePing ??= new PagePing();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PagePing))
                                Frame.Navigate(PagePing);
                        }
                        else
                            Frame.Navigate(PagePing);
                        break;
                    case Enums.WpfPage.WebRequest:
                        PageWebRequest ??= new PageWebRequest();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageWebRequest))
                                Frame.Navigate(PageWebRequest);
                        }
                        else
                            Frame.Navigate(PageWebRequest);
                        break;
                    case Enums.WpfPage.Changelog:
                        PageChangelog ??= new PageChangelog();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageChangelog))
                                Frame.Navigate(PageChangelog);
                        }
                        else
                            Frame.Navigate(PageChangelog);
                        break;
                    case Enums.WpfPage.AppTheme:
                        PageAppTheme ??= new PageAppTheme();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageAppTheme))
                                Frame.Navigate(PageAppTheme);
                        }
                        else
                            Frame.Navigate(PageAppTheme);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                OnPropertyRaised();
            }
        }

        #endregion

        #region Public and private methods

        public void DefaultTheme()
        {
            ThemePrimary = Enums.ThemePrimary.Light;
            ThemeColor = Enums.ThemeColor.Cobalt;
        }

        #endregion
    }
}


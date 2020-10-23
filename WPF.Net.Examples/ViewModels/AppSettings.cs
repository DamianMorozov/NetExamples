using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Navigation;
using WPF.Net.Examples.Models;
using WPF.Net.Examples.Views;

namespace WPF.Net.Examples.ViewModels
{
    public class AppSettings : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Public and private fields and properties

        private HttpServiceEntity _httpService;
        public HttpServiceEntity HttpService
        {
            get => _httpService;
            set
            {
                _httpService = value;
                OnPropertyRaised();
            }
        }

        private ProxyEntity _proxy;
        public ProxyEntity Proxy
        {
            get => _proxy;
            set
            {
                _proxy = value;
                OnPropertyRaised();
            }
        }

        private WebClientEntity _webClient;
        public WebClientEntity WebClient
        {
            get => _webClient;
            set
            {
                _webClient = value;
                OnPropertyRaised();
            }
        }

        private WebRequestEntity _webRequest;
        public WebRequestEntity WebRequest
        {
            get => _webRequest;
            set
            {
                _webRequest = value;
                OnPropertyRaised();
            }
        }

        private PingEntity _ping;
        public PingEntity Ping
        {
            get => _ping;
            set
            {
                _ping = value;
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
            HttpService = new HttpServiceEntity();
            Proxy = new ProxyEntity();
            WebClient = new WebClientEntity();
            WebRequest = new WebRequestEntity();
            Ping = new PingEntity();
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

        private PageHttpService _pageHttpService;
        public PageHttpService PageHttpService
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

        private EnumWpfPage _activePage;

        public EnumWpfPage ActivePage
        {
            get => _activePage;
            set
            {
                switch (_activePage = value)
                {
                    case EnumWpfPage.WebClient:
                        if (PageWebClient == null)
                            PageWebClient = new PageWebClient();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageWebClient))
                                Frame.Navigate(PageWebClient);
                        }
                        else
                            Frame.Navigate(PageWebClient);
                        break;
                    case EnumWpfPage.HttpService:
                        if (PageHttpService == null)
                            PageHttpService = new PageHttpService();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageHttpService))
                                Frame.Navigate(PageHttpService);
                        }
                        else
                            Frame.Navigate(PageHttpService);
                        break;
                    case EnumWpfPage.Proxy:
                        if (_pageProxy == null)
                            _pageProxy = new PageProxy();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageProxy))
                                Frame.Navigate(_pageProxy);
                        }
                        else
                            Frame.Navigate(_pageProxy);
                        break;
                    case EnumWpfPage.Ping:
                        if (PagePing == null)
                            PagePing = new PagePing();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PagePing))
                                Frame.Navigate(PagePing);
                        }
                        else
                            Frame.Navigate(PagePing);
                        break;
                    case EnumWpfPage.WebRequest:
                        if (PageWebRequest == null)
                            PageWebRequest = new PageWebRequest();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageWebRequest))
                                Frame.Navigate(PageWebRequest);
                        }
                        else
                            Frame.Navigate(PageWebRequest);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                OnPropertyRaised();
            }
        }

        #endregion

        #region Public and private methods

        //

        #endregion
    }
}


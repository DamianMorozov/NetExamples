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

        private HttpService _httpService;
        public HttpService HttpService
        {
            get => _httpService;
            set
            {
                _httpService = value;
                OnPropertyRaised();
            }
        }

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

        #endregion

        #region Constructor and destructor

        public AppSettings() { SetupDefault(); }

        public void SetupDefault()
        {
            Frame = new Frame
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden
            };
            HttpService = new HttpService();
            Proxy = new Proxy();
            WebClient = new WebClientEntity();
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
        public PageProxy PageProxy
        {
            get => _pageProxy;
            set
            {
                _pageProxy = value;
                OnPropertyRaised();
            }
        }

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
                        if (PageProxy == null)
                            PageProxy = new PageProxy();
                        if (Frame.Content != null)
                        {
                            if (!(Frame.Content is PageProxy))
                                Frame.Navigate(PageProxy);
                        }
                        else
                            Frame.Navigate(PageProxy);
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
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                OnPropertyRaised();
            }
        }

        #endregion
    }
}


using System;
using System.Threading.Tasks;
using System.Windows.Threading;
// ReSharper disable UnusedMember.Global

namespace Wpf.WebClient.Utils
{
    public static class InvokeContentControl
    {
        public static class Properties
        {
            public static Task SetContent(System.Windows.Controls.ContentControl item, string value)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Content = value;
                        });
                    }
                    else
                    {
                            item.Content = value;
                    }
                });
            }

            public static Task AddContent(System.Windows.Controls.ContentControl item, string value)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Content += value + Environment.NewLine;
                        });
                    }
                    else
                    {
                        item.Content += value + Environment.NewLine;
                    }
                });
            }
        }
    }
}

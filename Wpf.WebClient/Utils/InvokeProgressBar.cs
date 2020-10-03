using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Wpf.WebClient.Utils
{
    public static class InvokeProgressBar
    {
        public static class Methods
        {
            public static Task SetValue(System.Windows.Controls.ProgressBar item, int value)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Value = value;
                        });
                    }
                    else
                    {
                        item.Value = value;
                    }
                });
            }

            public static Task SetMinimum(System.Windows.Controls.ProgressBar item, int value)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Minimum = value;
                        });
                    }
                    else
                    {
                        item.Minimum = value;
                    }
                });
            }

            public static Task SetMaximum(System.Windows.Controls.ProgressBar item, int value)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Maximum = value;
                        });
                    }
                    else
                    {
                        item.Maximum = value;
                    }
                });
            }
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Windows.Threading;

// ReSharper disable UnusedMember.Global

namespace Wpf.WebClient.Utils
{
    internal static class InvokeControl
    {
        public static class Properties
        {
            public static class SetVisibility
            {
                public static Task Async(System.Windows.Controls.Control item, System.Windows.Visibility value)
                {
                    return Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Visibility = value;
                        });
                    });
                }
            }

            public static class SetIsEnabled
            {
                public static Task Async(System.Windows.Controls.Control item, bool value)
                {
                    return Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.IsEnabled = value;
                        });
                    });
                }
            }

            public static class SetBackground
            {
                public static Task Async(System.Windows.Controls.Control item, System.Windows.Media.Brush value)
                {
                    return Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Background = value;
                        });
                    });
                }
            }

            public static class SetForeground
            {
                public static Task Async(System.Windows.Controls.Control item, System.Windows.Media.Brush value)
                {
                    return Task.Run(() =>
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Foreground = value;
                        });
                    });
                }
            }
        }

        public static class Methods
        {
            public static Task Focus(System.Windows.Controls.Control item)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                           item.Focus();
                        });
                    }
                    else
                    {
                           item.Focus();
                    }
                });
            }
        }
    }
}

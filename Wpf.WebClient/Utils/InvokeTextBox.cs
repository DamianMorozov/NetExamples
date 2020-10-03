using System;
using System.Threading.Tasks;
using System.Windows.Threading;
// ReSharper disable UnusedMember.Global

namespace Wpf.WebClient.Utils
{
    public static class InvokeTextBox
    {
        public static class Methods
        {
            public static Task Clear(System.Windows.Controls.TextBox item)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)item.Clear);
                    else
                        item.Clear();
                });
            }

            public static Task SetText(System.Windows.Controls.TextBox item, string text)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            item.Text = text;
                        });
                    }
                    else
                    {
                        item.Text = text;
                    }
                });
            }

            public static Task<string> GetText(System.Windows.Controls.TextBox item)
            {
                return Task.Run(() =>
                {
                    var result = string.Empty;
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                            result = item.Text;
                        });
                    }
                    else
                    {
                        result = item.Text;
                    }
                    return result;
                });
            }

            public static Task AddText(System.Windows.Controls.TextBox item, string text)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                           item.Text += text + Environment.NewLine;
                        });
                    }
                    else
                    {
                        item.Text += text + Environment.NewLine;
                    }
                });
            }

            public static Task AddTextFormat(System.Windows.Controls.TextBox item, System.Diagnostics.Stopwatch sw, string text)
            {
                return Task.Run(() =>
                {
                    if (!item.CheckAccess())
                    {
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate
                        {
                           item.Text += $@"[{sw.Elapsed}] {text}" + Environment.NewLine;
                        });
                    }
                    else
                    {
                        item.Text += $@"[{sw.Elapsed}] {text}" + Environment.NewLine;
                    }
                });
            }
        }
    }
}

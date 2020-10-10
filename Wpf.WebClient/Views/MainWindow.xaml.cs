using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using WPF.Utils;
using WPF.WebClient.ViewModels;
// ReSharper disable UnusedMember.Global

namespace WPF.WebClient.Views
{
    public partial class MainWindow
    {
        // Программные настройки.
        private AppSettings _appSet;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _appSet = ViewModels.Utils.GetSettings(this);
        }

        private void ButtonFileDownload_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(_appSet.Link.ToString()))
                    {
                        var sw = Stopwatch.StartNew();
                        var fileDialog = new SaveFileDialog();
                        {
                            fileDialog.FileName = Path.GetFileName(_appSet.Link.ToString());
                            fileDialog.DefaultExt = @".*";
                            fileDialog.Filter = @"All files|*.*";
                            if (fileDialog.ShowDialog() == true)
                            {
                                InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Start downloading the file ""{_appSet.Link}"".");
                                using (var webClient = new System.Net.WebClient())
                                {
                                    InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File to save ""{fileDialog.FileName}"".");
                                    webClient.DownloadFile(_appSet.Link.ToString(), fileDialog.FileName);
                                }
                                InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Finish downloading the file ""{_appSet.Link}"".");
                                InvokeControl.Focus(fieldOut);
                                sw.Stop();
                            }
                        }
                    }
                    else
                    {
                        InvokeTextBox.AddText(fieldOut, @"FileName is empty!.");
                    }
                }
                catch (Exception ex)
                {
                    InvokeTextBox.AddText(fieldOut, $@"Error: ""{ex.Message}"".");
                }
                InvokeTextBox.AddText(fieldOut, @"----------------------------------------------------------------------------------------------------");
            });
        }

        private void ButtonFileDownloadAsync_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(_appSet.Link.ToString()))
                    {
                        var sw = Stopwatch.StartNew();
                        var fileDialog = new SaveFileDialog();
                        {
                            fileDialog.FileName = Path.GetFileName(_appSet.Link.ToString());
                            fileDialog.DefaultExt = @".*";
                            fileDialog.Filter = @"All files|*.*";
                            if (fileDialog.ShowDialog() == true)
                            {
                                InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Start downloading the file ""{_appSet.Link}"".");
                                using (var webClient = new System.Net.WebClient())
                                {
                                    InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File to save ""{fileDialog.FileName}"".");
                                    webClient.DownloadFileAsync(_appSet.Link, fileDialog.FileName);
                                }
                                InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Finish downloading the file ""{_appSet.Link}"".");
                                InvokeControl.Focus(fieldOut);
                                sw.Stop();
                            }
                        }
                    }
                    else
                    {
                        InvokeTextBox.AddText(fieldOut, @"FileName is empty!.");
                    }
                }
                catch (Exception ex)
                {
                    InvokeTextBox.AddText(fieldOut, $@"Error: ""{ex.Message}"".");
                }
                InvokeTextBox.AddText(fieldOut, @"----------------------------------------------------------------------------------------------------");
            });
        }

        private void ButtonFileDownloadWithProgress_OnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    long bytesCursor = 0;
                    if (!int.TryParse(InvokeTextBox.GetText(fieldBufferSize).ToString(CultureInfo.InvariantCulture),
                        NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out var bufferSize))
                        bufferSize = 1024;

                    if (!string.IsNullOrEmpty(_appSet.Link.ToString()))
                    {
                        InvokeProgressBar.SetMinimum(progressBar, 0);
                        InvokeProgressBar.SetMaximum(progressBar, 100);
                        InvokeProgressBar.SetValue(progressBar, 0);
                        var sw = Stopwatch.StartNew();
                        var fileDialog = new SaveFileDialog();
                        {
                            fileDialog.FileName = Path.GetFileName(_appSet.Link.ToString());
                            fileDialog.DefaultExt = @".*";
                            fileDialog.Filter = @"All files|*.*";
                            if (fileDialog.ShowDialog() == true)
                            {
                                InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Start downloading the file ""{_appSet.Link}"".");
                                using (var webClient = new System.Net.WebClient())
                                {
                                    InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File to save ""{fileDialog.FileName}"".");
                                    //webClient.DownloadFileAsync(new Uri(url), fileDialog.FileName);
                                    using (var webRead = webClient.OpenRead(_appSet.Link.ToString()))
                                    {
                                        using (var binaryReader = new BinaryReader(webRead ?? throw new InvalidOperationException()))
                                        {
                                            var bytesTotal = Convert.ToInt64(webClient.ResponseHeaders["Content-Length"]);
                                            InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File size {bytesTotal:### ### ###} bytes.");
                                            if (bytesTotal > 0)
                                            {
                                                using (var binaryWriter = new BinaryWriter(File.Open(fileDialog.FileName, FileMode.Create)))
                                                {
                                                    while (bytesCursor < bytesTotal)
                                                    {
                                                        binaryWriter.Write(binaryReader.ReadBytes(bufferSize));
                                                        bytesCursor += bufferSize;
                                                        var setValue = (int)(bytesCursor * 100 / bytesTotal);
                                                        if (setValue > 100) setValue = 100;
                                                        InvokeProgressBar.SetValue(progressBar, setValue);
                                                    }
                                                    InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Finish downloading the file ""{_appSet.Link}"".");
                                                }
                                            }
                                            else
                                            {
                                                InvokeTextBox.AddTextFormat(fieldOut, sw, @"Download can not be started!");
                                            }
                                        }
                                    }
                                }
                                InvokeControl.Focus(fieldOut);
                                sw.Stop();
                            }
                        }
                    }
                    else
                    {
                        InvokeTextBox.AddText(fieldOut, @"FileName is empty!.");
                    }
                }
                catch (Exception ex)
                {
                    InvokeTextBox.AddText(fieldOut, $@"Error: ""{ex.Message}"".");
                }
                InvokeTextBox.AddText(fieldOut, @"----------------------------------------------------------------------------------------------------");
            });
        }
    }
}

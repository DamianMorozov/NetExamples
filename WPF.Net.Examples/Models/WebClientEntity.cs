using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Utils;

namespace WPF.Net.Examples.Models
{
    public class WebClientEntity
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string caller = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        #endregion

        #region Public fields and properties

        private string _url;
        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                OnPropertyRaised();
            }
        }

        private int _bufferSize;
        public int BufferSize
        {
            get => _bufferSize;
            set
            {
                _bufferSize = value;
                OnPropertyRaised();
            }
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyRaised();
            }
        }

        #endregion

        #region Constructor and destructor

        public WebClientEntity()
        {
            SetupDefault();
        }

        public void SetupDefault()
        {
            Url = @"https://downloadmaster.ru/dm/download/dmaster.exe";
            BufferSize = 102_400;
            FileName = Environment.CurrentDirectory + @"\dmaster.exe";
        }

        #endregion

        #region Public methods

        public async Task Download(TextBox fieldOut, bool isAsync)
        {
            await Task.Run(() =>
            {
                var sw = Stopwatch.StartNew();
                try
                {
                    InvokeTextBox.Clear(fieldOut);
                    if (!string.IsNullOrEmpty(Url))
                    {
                        InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Start downloading the file ""{Url}"".");
                        using (var webClient = new System.Net.WebClient())
                        {
                            InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File to save ""{FileName}"".");
                            if (isAsync)
                                webClient.DownloadFileAsync(new Uri(Url), FileName);
                            else
                                webClient.DownloadFile(new Uri(Url), FileName);
                        }
                        InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Finish downloading the file ""{Url}"".");
                        InvokeControl.Focus(fieldOut);
                    }
                    else
                    {
                        InvokeTextBox.AddText(fieldOut, @"URL is empty!.");
                    }
                }
                catch (Exception ex)
                {
                    InvokeTextBox.AddText(fieldOut, $@"Error: ""{ex.Message}"".");
                }
                InvokeTextBox.AddText(fieldOut, @"----------------------------------------------------------------------------------------------------");
                sw.Stop();
            });
        }

        public async Task DownloadWithBuffer(TextBox fieldOut, ProgressBar progressBar)
        {
            await Task.Run(() =>
            {
                var sw = Stopwatch.StartNew();
                try
                {
                    InvokeTextBox.Clear(fieldOut);
                    InvokeProgressBar.SetMinimum(progressBar, 0);
                    InvokeProgressBar.SetMaximum(progressBar, 100);
                    InvokeProgressBar.SetValue(progressBar, 0);
                    if (!string.IsNullOrEmpty(Url))
                    {
                        long bytesCursor = 0;
                        InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Start downloading the file ""{Url}"".");
                        using (var webClient = new System.Net.WebClient())
                        {
                            InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File to save ""{FileName}"".");
                            //webClient.DownloadFile(new Uri(Url), FileName);
                            using (var webRead = webClient.OpenRead(Url))
                            {
                                using (var binaryReader = new BinaryReader(webRead ?? throw new InvalidOperationException()))
                                {
                                    var bytesTotal = Convert.ToInt64(webClient.ResponseHeaders["Content-Length"]);
                                    InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File size {bytesTotal:### ### ###} bytes.");
                                    if (bytesTotal > 0)
                                    {
                                        using (var binaryWriter = new BinaryWriter(File.Open(FileName, FileMode.Create)))
                                        {
                                            while (bytesCursor < bytesTotal)
                                            {
                                                binaryWriter.Write(binaryReader.ReadBytes(BufferSize));
                                                bytesCursor += BufferSize;
                                                var setValue = (int)(bytesCursor * 100 / bytesTotal);
                                                if (setValue > 100) setValue = 100;
                                                InvokeProgressBar.SetValue(progressBar, setValue);
                                            }
                                            InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Finish downloading the file ""{Url}"".");
                                        }
                                    }
                                    else
                                    {
                                        InvokeTextBox.AddTextFormat(fieldOut, sw, @"Download can not be started!");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        InvokeTextBox.AddText(fieldOut, @"URL is empty!.");
                    }
                }
                catch (Exception ex)
                {
                    InvokeTextBox.AddText(fieldOut, $@"Error: ""{ex.Message}"".");
                }
                InvokeTextBox.AddText(fieldOut, @"----------------------------------------------------------------------------------------------------");
                sw.Stop();
            });
        }

        #endregion
    }
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPF.Utils;

namespace WpfNet.Models
{
    internal class WebClientEntity
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

        private Task _task;

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

        #region Public and private methods

        public void Download(TextBox fieldOut, bool isAsync, ProgressBar progressBar)
        {
            if (!(_task is null))
            {
                if (_task.Status == TaskStatus.RanToCompletion)
                {
                    _task.Dispose();
                    _task = null;
                }
            }
            _task = Task.Run(async () =>
            {
                await DownloadAsync(fieldOut, isAsync, progressBar);
            });
        }

        [SuppressMessage("ReSharper", "MethodHasAsyncOverload")]
        private async Task DownloadAsync(TextBox fieldOut, bool isAsync, ProgressBar progressBar)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(true);
            Stopwatch sw = Stopwatch.StartNew();
            try
            {
                InvokeProgressBar.SetMinimum(progressBar, 0);
                InvokeProgressBar.SetMaximum(progressBar, 100);
                InvokeProgressBar.SetValue(progressBar, 0);
                InvokeTextBox.Clear(fieldOut);
                if (!string.IsNullOrEmpty(Url))
                {
                    InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Start downloading the file ""{Url}"".");
                    using (System.Net.WebClient webClient = new System.Net.WebClient())
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
                    InvokeTextBox.AddTextFormat(fieldOut, sw, @"URL is empty!.");
                }
                InvokeProgressBar.SetValue(progressBar, 100);
            }
            catch (Exception ex)
            {
                InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Error: ""{ex.Message}"".");
            }
            InvokeTextBox.AddTextFormat(fieldOut, sw, @"----------------------------------------------------------------------------------------------------");
            sw.Stop();
        }

        public void DownloadWithBuffer(TextBox fieldOut, ProgressBar progressBar)
        {
            if (!(_task is null))
            {
                if (_task.Status == TaskStatus.RanToCompletion)
                {
                    _task.Dispose();
                    _task = null;
                }
            }
            _task = Task.Run(async () =>
            {
                await DownloadWithBufferAsync(fieldOut, progressBar);
            });
        }

        private async Task DownloadWithBufferAsync(TextBox fieldOut, ProgressBar progressBar)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(true);
            Stopwatch sw = Stopwatch.StartNew();
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
                    using System.Net.WebClient webClient = new System.Net.WebClient();
                    InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File to save ""{FileName}"".");
                    //webClient.DownloadFile(new Uri(Url), FileName);
                    using Stream webRead = webClient.OpenRead(Url);
                    using BinaryReader binaryReader = new BinaryReader(webRead ?? throw new InvalidOperationException());
                    long bytesTotal = Convert.ToInt64(webClient.ResponseHeaders["Content-Length"]);
                    InvokeTextBox.AddTextFormat(fieldOut, sw, $@"File size {bytesTotal:### ### ###} bytes.");
                    if (bytesTotal > 0)
                    {
                        using BinaryWriter binaryWriter = new BinaryWriter(File.Open(FileName, FileMode.Create));
                        while (bytesCursor < bytesTotal)
                        {
                            binaryWriter.Write(binaryReader.ReadBytes(BufferSize));
                            bytesCursor += BufferSize;
                            int setValue = (int)(bytesCursor * 100 / bytesTotal);
                            if (setValue > 100) setValue = 100;
                            InvokeProgressBar.SetValue(progressBar, setValue);
                        }
                        InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Finish downloading the file ""{Url}"".");
                    }
                    else
                    {
                        InvokeTextBox.AddTextFormat(fieldOut, sw, @"Download can not be started!");
                    }
                }
                else
                {
                    InvokeTextBox.AddTextFormat(fieldOut, @"URL is empty!.");
                }
            }
            catch (Exception ex)
            {
                InvokeTextBox.AddTextFormat(fieldOut, sw, $@"Error: ""{ex.Message}"".");
            }
            InvokeTextBox.AddTextFormat(fieldOut, sw, @"----------------------------------------------------------------------------------------------------");
            sw.Stop();
        }

        #endregion
    }
}

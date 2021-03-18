using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using WPF.Net.Examples.ViewModels;

namespace WPF.Net.Examples.Views
{
    public partial class PageWebParse
    {
        #region Private fields and properties

        private AppSettings AppSettings { get; set; }

        #endregion

        #region Constructor and destructor

        public PageWebParse()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void PageWebParse_Loaded(object sender, RoutedEventArgs e)
        {
            AppSettings = ViewModels.Utils.GetSettings(this);
        }

        public async void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            AppSettings.WebParseLog = string.Empty;
            AppSettings.WebParseResult = string.Empty;
        }

        public Dictionary<string, List<List<string>>> Parsing()
        {
            var result = new Dictionary<string, List<List<string>>>();

            using var httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.None | DecompressionMethods.Deflate | DecompressionMethods.GZip
            };
            using var httpClient = new HttpClient(httpClientHandler);
            using var response = httpClient.GetAsync(AppSettings.WebParseUrl).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                var html = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(html))
                {
                    var doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    var tables = doc.DocumentNode.SelectNodes(
                        ".//div[@class='block_content']//div[@class='two-table-row']//div[@class]");
                    if (tables == null)
                    {
                        AppSettings.WebParseLog += "Tables is null!" + Environment.NewLine;
                    }
                    else
                    {
                        foreach (var table in tables)
                        {
                            var tableTitle = table.SelectSingleNode(".//div[@class='head_tb']");
                            if (tableTitle == null)
                            {
                                //AppSettings.WebParseLog += "Table title is null!" + Environment.NewLine;
                            }
                            else
                            {
                                var tableLigue = table.SelectSingleNode(".//div[@class='tab_champ']//table[@class='tab_ligue']");
                                if (tableLigue == null)
                                {
                                    AppSettings.WebParseLog += "Table is null!" + Environment.NewLine;
                                }
                                else
                                {
                                    var rows = tableLigue.SelectNodes(".//tr");
                                    if (rows == null)
                                    {
                                        AppSettings.WebParseLog += "Table ligue is null!" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        var res = new List<List<string>>();
                                        foreach (var row in rows)
                                        {
                                            var cells = row.SelectNodes(".//td");
                                            if (cells == null)
                                            {
                                                //AppSettings.WebParseLog += "Cells is null!" + Environment.NewLine;
                                            }
                                            else
                                            {
                                                if (cells.Count > 0)
                                                {
                                                    //res.Add(new List<string>(cells.Select(x => x.InnerText)));
                                                    var list = cells.Select(x => x.InnerText).ToList();
                                                    res.Add(list);
                                                }
                                            }
                                        }
                                        result[tableTitle.InnerText] = res;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public async void ButtonWebParse_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            ButtonClear_OnClick(sender, e);

            AppSettings.WebParseStopwatch = Stopwatch.StartNew();
            var items = Parsing();
            
            foreach (var item in items)
            {
                var str = string.Empty;
                foreach (List<string> item2 in item.Value)
                {
                    foreach (string item3 in item2)
                    {
                        str += $"{item3} | ";
                    }
                    str += Environment.NewLine;
                }
                AppSettings.WebParseResult += $"{item.Key.TrimStart('\r', ' ', '\n').TrimEnd('\r', ' ', '\n')}" + Environment.NewLine +
                                              $"{str}" + Environment.NewLine;
            }
            AppSettings.WebParseStopwatch.Stop();
        }

        #endregion
    }
}

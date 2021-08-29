using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using WpfNet.ViewModels;

namespace WpfNet.Views
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
            Dictionary<string, List<List<string>>> result = new Dictionary<string, List<List<string>>>();

            using HttpClientHandler httpClientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.None | DecompressionMethods.Deflate | DecompressionMethods.GZip
            };
            using HttpClient httpClient = new HttpClient(httpClientHandler);
            using HttpResponseMessage response = httpClient.GetAsync(AppSettings.WebParseUrl).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                string html = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (!string.IsNullOrEmpty(html))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    HtmlNodeCollection tables = doc.DocumentNode.SelectNodes(
                        ".//div[@class='block_content']//div[@class='two-table-row']//div[@class]");
                    if (tables == null)
                    {
                        AppSettings.WebParseLog += "Tables is null!" + Environment.NewLine;
                    }
                    else
                    {
                        foreach (HtmlNode table in tables)
                        {
                            HtmlNode tableTitle = table.SelectSingleNode(".//div[@class='head_tb']");
                            if (tableTitle == null)
                            {
                                //AppSettings.WebParseLog += "Table title is null!" + Environment.NewLine;
                            }
                            else
                            {
                                HtmlNode tableLigue = table.SelectSingleNode(".//div[@class='tab_champ']//table[@class='tab_ligue']");
                                if (tableLigue == null)
                                {
                                    AppSettings.WebParseLog += "Table is null!" + Environment.NewLine;
                                }
                                else
                                {
                                    HtmlNodeCollection rows = tableLigue.SelectNodes(".//tr");
                                    if (rows == null)
                                    {
                                        AppSettings.WebParseLog += "Table ligue is null!" + Environment.NewLine;
                                    }
                                    else
                                    {
                                        List<List<string>> res = new List<List<string>>();
                                        foreach (HtmlNode row in rows)
                                        {
                                            HtmlNodeCollection cells = row.SelectNodes(".//td");
                                            if (cells == null)
                                            {
                                                //AppSettings.WebParseLog += "Cells is null!" + Environment.NewLine;
                                            }
                                            else
                                            {
                                                if (cells.Count > 0)
                                                {
                                                    //res.Add(new List<string>(cells.Select(x => x.InnerText)));
                                                    List<string> list = cells.Select(x => x.InnerText).ToList();
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
            Dictionary<string, List<List<string>>> items = Parsing();
            
            foreach (KeyValuePair<string, List<List<string>>> item in items)
            {
                string str = string.Empty;
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

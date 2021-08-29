using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using AngleSharp.Io;
using BrowseSharp;
using BrowseSharp.Common;
using BrowseSharp.Common.Html;
using WpfNet.ViewModels;

// ReSharper disable UnusedMember.Global

namespace WpfNet.Views
{
    public partial class PageBrowseSharp
    {
        #region Private fields and properties

        private AppSettings AppSettings { get; set; }

        #endregion

        #region Constructor and destructor

        public PageBrowseSharp()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void PageBrowseSharp_Loaded(object sender, RoutedEventArgs e)
        {
            AppSettings = ViewModels.Utils.GetSettings(this);
        }

        public async void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            AppSettings.WebParseLog = string.Empty;
            AppSettings.WebParseResult = string.Empty;
        }

        public async void ButtonBrowseSharp_OnClick(object sender, RoutedEventArgs e)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            ButtonClear_OnClick(sender, e);

            AppSettings.WebParseStopwatch = Stopwatch.StartNew();
            
            // Initialize Browser
            Browser browser = new Browser();

            // Navigate to a website
            IDocument document = browser.NavigateAsync(AppSettings.BrowseSharpUrl).Result;

            // Navigate with headers
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("x-csrf-token", "2342342");
            IDocument response1 = browser.NavigateAsync(AppSettings.BrowseSharpUrl, headers).GetAwaiter().GetResult();
            AppSettings.BrowseSharpLog += $"Response.RequestUri: {response1.RequestUri}" + Environment.NewLine;
            AppSettings.BrowseSharpLog += $"Response.Head: {response1.Head}" + Environment.NewLine;
            AppSettings.BrowseSharpLog += $"Response.Body: {response1.Body}" + Environment.NewLine;

            // Navigate with data
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("Username", "FakeUserName");
            formData.Add("Password", "FakePassword123");
            formData.Add("SecretMessage", "This is a secret message");
            IDocument response2 = browser.NavigateAsync(AppSettings.BrowseSharpUrl, headers, formData).GetAwaiter().GetResult();
            AppSettings.BrowseSharpLog += $"Response.RequestUri: {response2.RequestUri}" + Environment.NewLine;
            AppSettings.BrowseSharpLog += $"Response.Head: {response2.Head}" + Environment.NewLine;
            AppSettings.BrowseSharpLog += $"Response.Body: {response2.Body}" + Environment.NewLine;

            // SubmitForm
            browser.NavigateAsync("https://www.browsesharp.org/testsitesforms.html").GetAwaiter().GetResult();
            Form postForm = browser.Document.Forms[0];
            postForm.SetValue("UserName", "TestUser");
            postForm.SetValue("Password", "TestPassword");
            IDocument postResponse = browser.SubmitFormAsync(postForm).GetAwaiter().GetResult();

            // Form as dictionary
            IDocument response3 = await browser.SubmitAsync("https://www.hashemian.com/tools/form-post-tester.php", formData);

            // With headers
            IDocument response4 = await browser.SubmitAsync("https://requesttester.com", formData, headers);

            // Async calls
            IDocument response5 = await browser.NavigateAsync("https://www.browsesharp.org/testsitesforms.html");

            // Get current document
            IDocument lastDocument = browser.Document;

            // Typed Responses
            // Note: Request is a custom class unrelated to this library
            // Make request that is deserialized into a request object
            IDocument<Request> response = browser.Navigate<Request>(AppSettings.BrowseSharpUrl, headers, formData);
            Request request = response.Data; /* Get the Request object from the response */

            // Run script - This will be cleaned up!
            browser.JavascriptEngine.Document = browser.Documents[0].Scripts[2].JavascriptString;
            object result = browser.JavascriptEngine.Execute("w3CodeColor();");

            // Output
            IDocument requestedDocument = browser.Documents[0];
            AppSettings.BrowseSharpLog += $"Scraped scripts: {requestedDocument.Scripts.Count}" + Environment.NewLine;
            AppSettings.BrowseSharpLog += $"Scraped styles: {requestedDocument.Styles.Count}" + Environment.NewLine;
            AppSettings.BrowseSharpLog += $"Begining of document: {requestedDocument.Response.Content.Substring(0, 60)}" + Environment.NewLine;
            
            AppSettings.WebParseStopwatch.Stop();
        }

        #endregion
    }
}

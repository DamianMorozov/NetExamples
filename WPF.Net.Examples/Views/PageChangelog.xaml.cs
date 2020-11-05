using System.IO;
using System.Windows;
using WPF.Net.Examples.ViewModels;

// ReSharper disable UnusedMember.Global

namespace WPF.Net.Examples.Views
{
    public partial class PageChangelog
    {
        #region Private fields and properties

        private AppSettings _appSet;

        #endregion

        #region Constructor and destructor

        public PageChangelog()
        {
            InitializeComponent();
        }

        #endregion

        #region Private methods

        private void PageChangelog_Loaded(object sender, RoutedEventArgs e)
        {
            _appSet = ViewModels.Utils.GetSettings(this);
            
            var fileName = "CHANGELOG.md";
            if (File.Exists(fileName))
            {
                using (var sr = new StreamReader(fileName))
                {
                    TextBlockMain.Text = sr.ReadToEnd();
                }
            }
        }

        #endregion
    }
}

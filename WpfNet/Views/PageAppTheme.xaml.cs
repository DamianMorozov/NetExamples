// ReSharper disable UnusedMember.Global

using System.Windows;
using WpfNet.Utils;
using WpfNet.ViewModels;

namespace WpfNet.Views
{
    public partial class PageAppTheme
    {
        #region Private fields and properties

        private AppSettings AppSettings { get; set; }

        #endregion

        #region Constructor and destructor

        public PageAppTheme()
        {
            InitializeComponent();
        }

        #endregion

        #region Public and private methods

        private void PageAppTheme_OnLoaded(object sender, RoutedEventArgs e)
        {
            AppSettings = ViewModels.Utils.GetSettings(this);

            GetThemeGui();
        }

        private void GetThemeGui()
        {
            fieldThemePrimary.Items.Clear();
            int i = 0;
            int s = 0;
            foreach (object themePrimary in System.Enum.GetValues(typeof(Enums.ThemePrimary)))
            {
                fieldThemePrimary.Items.Add(themePrimary);
                if (themePrimary.Equals(AppSettings.ThemePrimary))
                    s = i;
                i++;
            }
            fieldThemePrimary.SelectedIndex = s;

            fieldThemeColor.Items.Clear();
            i = 0;
            s = 0;
            foreach (object themeColor in System.Enum.GetValues(typeof(Enums.ThemeColor)))
            {
                fieldThemeColor.Items.Add(themeColor);
                if (themeColor.Equals(AppSettings.ThemeColor))
                    s = i;
                i++;
            }
            fieldThemeColor.SelectedIndex = s;
        }

        private void ButtonThemeDefault_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.DefaultTheme();
            GetThemeGui();
        }

        private void ButtonThemeApply_OnClick(object sender, RoutedEventArgs e)
        {
            AppSettings.ThemePrimary = (Enums.ThemePrimary)fieldThemePrimary.Items[fieldThemePrimary.SelectedIndex];
            AppSettings.ThemeColor = (Enums.ThemeColor)fieldThemeColor.Items[fieldThemeColor.SelectedIndex];
        }

        #endregion
    }
}

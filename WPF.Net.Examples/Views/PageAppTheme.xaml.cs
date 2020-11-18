// ReSharper disable UnusedMember.Global

using System.Windows;
using System.Windows.Controls;
using WPF.Net.Examples.Models;
using WPF.Net.Examples.ViewModels;

namespace WPF.Net.Examples.Views
{
    public partial class PageAppTheme
    {
        #region Private fields and properties

        private AppSettings _appSet;

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
            _appSet = ViewModels.Utils.GetSettings(this);

            GetThemeGui();
        }

        private void GetThemeGui()
        {
            fieldThemePrimary.Items.Clear();
            var i = 0;
            var s = 0;
            foreach (var themePrimary in System.Enum.GetValues(typeof(Enums.ThemePrimary)))
            {
                fieldThemePrimary.Items.Add(themePrimary);
                if (themePrimary.Equals(_appSet.ThemePrimary))
                    s = i;
                i++;
            }
            fieldThemePrimary.SelectedIndex = s;

            fieldThemeColor.Items.Clear();
            i = 0;
            s = 0;
            foreach (var themeColor in System.Enum.GetValues(typeof(Enums.ThemeColor)))
            {
                fieldThemeColor.Items.Add(themeColor);
                if (themeColor.Equals(_appSet.ThemeColor))
                    s = i;
                i++;
            }
            fieldThemeColor.SelectedIndex = s;
        }

        private void ButtonThemeDefault_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.DefaultTheme();
            GetThemeGui();
        }

        private void ButtonThemeApply_OnClick(object sender, RoutedEventArgs e)
        {
            _appSet.ThemePrimary = (Enums.ThemePrimary)fieldThemePrimary.Items[fieldThemePrimary.SelectedIndex];
            _appSet.ThemeColor= (Enums.ThemeColor)fieldThemeColor.Items[fieldThemeColor.SelectedIndex];
        }

        #endregion
    }
}

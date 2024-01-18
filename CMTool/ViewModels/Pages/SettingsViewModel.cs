// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using CMTool.Views.Pages;
using CMTool.Views.Settings;
using System.Collections.ObjectModel;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;


        [ObservableProperty]
        private Wpf.Ui.Appearance.ApplicationTheme _currentTheme = Wpf.Ui.Appearance.ApplicationTheme.Unknown;

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            CurrentTheme = ApplicationThemeManager.GetAppTheme();

            _isInitialized = true;
        }


        [RelayCommand]
        private void OnChangeTheme(string parameter)
        {
            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == Wpf.Ui.Appearance.ApplicationTheme.Light)
                        break;

                    ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Light);
                    CurrentTheme = Wpf.Ui.Appearance.ApplicationTheme.Light;

                    break;

                default:
                    if (CurrentTheme == Wpf.Ui.Appearance.ApplicationTheme.Dark)
                        break;

                    ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Dark);
                    CurrentTheme = Wpf.Ui.Appearance.ApplicationTheme.Dark;

                    break;
            }
        }

        [ObservableProperty]
        private ICollection<object> _menuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("倒计时设置", SymbolRegular.Timer16, typeof(DateSettings)),
            new NavigationViewItem("课表设置", SymbolRegular.BoardSplit16, typeof(ClassSettings)),
            new NavigationViewItem("值日设置", SymbolRegular.People16, typeof(ClassSettings)),
            new NavigationViewItem("其他设置", SymbolRegular.MoreCircle16, typeof(ClassSettings)),
            new NavigationViewItem("关于", SymbolRegular.Info16, typeof(About))
        };
    }
}

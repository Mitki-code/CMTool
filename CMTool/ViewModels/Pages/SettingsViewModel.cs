﻿using CMTool.Views.Settings;
using System.Collections.ObjectModel;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ICollection<object> _menuItems = new ObservableCollection<object>
        {
            new NavigationViewItem("倒计时设置", SymbolRegular.Timer16, typeof(DateSettings)),
            new NavigationViewItem("课表设置", SymbolRegular.BoardSplit16, typeof(ClassSettings)),
            new NavigationViewItem("值日设置", SymbolRegular.People16, typeof(WorkSettings)),
            new NavigationViewItem("其他设置", SymbolRegular.MoreCircle16, typeof(MoreSettings)),
            new NavigationViewItem("关于", SymbolRegular.Info16, typeof(About))
        };
    }
}

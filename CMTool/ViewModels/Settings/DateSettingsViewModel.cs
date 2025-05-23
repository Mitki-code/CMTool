﻿using CMTool.Module;
using CMTool.ViewModels.Windows;
using Newtonsoft.Json;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Settings
{
    public partial class DateSettingsViewModel : ObservableObject
    {
        private static readonly ISnackbarService _snackbarService = App.GetService<ISnackbarService>();

        [ObservableProperty]
        public static string _EventName = FileIO.TimeData.Event;
        [ObservableProperty]
        public static DateTime _EventTime = Convert.ToDateTime(FileIO.TimeData.Time);
        [ObservableProperty]
        private string _Tips = "";


        [RelayCommand]
        private void OnSave()
        {
            FileIO.TimeData.Event = EventName;
            FileIO.TimeData.Time = EventTime.ToString();
            FileIO.WriteJsonFile("Assets/Data/DataTime.json", JsonConvert.SerializeObject(FileIO.TimeData, Formatting.Indented));
            App.GetService<SubWindowViewModel>().Refresh("Time");

            _snackbarService.Show("保存成功", "更改已应用", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
        }
    }
}

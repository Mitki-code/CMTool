using CMTool.Module;
using CMTool.ViewModels.Windows;
using CMTool.Views.Settings;
using CMTool.Views.Windows;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Settings
{
    public partial class DateSettingsViewModel : ObservableObject
    {
        public static JObject jObject = FileIO.GetData("Time");
        private static readonly ISnackbarService _snackbarService = App.GetService<ISnackbarService>();

        [ObservableProperty]
        public static string _EventName = jObject["Event"].ToString();
        [ObservableProperty]
        public static DateTime _EventTime = Convert.ToDateTime(jObject["Time"].ToString());
        [ObservableProperty]
        private string _Tips = "";


        [RelayCommand]
        private void OnSave()
        {
            jObject["Event"] = EventName;
            jObject["Time"] = EventTime.ToString();

            FileIO.WriteJsonFile("Assets/Data/DataTime.json", jObject);
            FileIO.TimeData.Event = EventName;
            FileIO.TimeData.Time = EventTime.ToString();
            //SubWindowViewModel.TimeJson = jObject;
            App.GetService<SubWindowViewModel>().Refresh("Time");

            _snackbarService.Show("保存成功", "更改已应用", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
        }
    }
}

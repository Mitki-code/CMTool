using CMTool.Module;
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
        public static JObject jObject = JsonRW.Readjson("Assets/MianData.json");

        [ObservableProperty]
        public static string _EventName = jObject["Event"].ToString();
        [ObservableProperty]
        public static DateTime _EventTime = Convert.ToDateTime(jObject["Time"].ToString());
        [ObservableProperty]
        private string _Tips = "";

        private readonly ISnackbarService _snackbarService;
        private ControlAppearance _snackbarAppearance = ControlAppearance.Success;

        public DateSettingsViewModel(ISnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
        }

        [RelayCommand]
        private void OnSave()
        {
            jObject["Event"] = EventName;
            jObject["Time"] = EventTime.ToString();

            JsonRW.Writejson("Assets/MianData.json", jObject);

            _snackbarService.Show(
                "保存成功",
                "重启后生效",
                _snackbarAppearance,
                new SymbolIcon(SymbolRegular.CheckmarkCircle16),
                TimeSpan.FromSeconds(2)
            );
        }
    }
}

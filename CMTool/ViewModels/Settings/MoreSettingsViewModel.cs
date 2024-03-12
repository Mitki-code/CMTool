using CMTool.Models;
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
    public partial class MoreSettingsViewModel : ObservableObject
    {
        private static JObject jObject = JsonRW.Readjson("Assets/MianData.json");
        //private static JObject jObject = JsonRW.Readjson("Assets/MianData.json");
        private readonly ISnackbarService _snackbarService;
        private ControlAppearance _snackbarAppearance = ControlAppearance.Success;

        public MoreSettingsViewModel(ISnackbarService snackbarService)
        {
            _snackbarService = snackbarService;
        }

        [ObservableProperty]
        private DateTime _WeekStart = Convert.ToDateTime(jObject["WeekStart"].ToString());

        [RelayCommand]
        private void OnClose()
        {
            Application.Current.Shutdown();
        }

        [RelayCommand]
        private void OnReSettings()
        {
            JsonRW.Writejson("Assets/MianData.json", JsonRW.Readjson("Assets/ReData.json"));

            _snackbarService.Show(
                "重置成功",
                "重启后生效",
                _snackbarAppearance,
                new SymbolIcon(SymbolRegular.CheckmarkCircle16),
                TimeSpan.FromSeconds(2)
            );
        }

        [RelayCommand]
        private void OnSave()
        {
            jObject["WeekStart"] = WeekStart.ToString();

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

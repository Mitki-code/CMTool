using CMTool.Module;
using CMTool.Views.Settings;
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
        private void OnSetPowerStart(object state)
        {
            string textOK;
            bool isOk;
            if ((bool)state)
            {
                isOk = PowerStartManger.SetAutoStart(true);
                textOK = "设置开机自启动";
            }
            else
            {
                isOk = PowerStartManger.SetAutoStart(false);
                textOK = "移除开机自启动";

                
            }

            if ( isOk )
            {
                _snackbarService.Show(
                "操作成功",
                "已"+textOK,
                _snackbarAppearance,
                new SymbolIcon(SymbolRegular.CheckmarkCircle16),
                TimeSpan.FromSeconds(2)
            );
            }
            else
            {
                _snackbarService.Show(
                "操作失败",
                "未能" + textOK,
                ControlAppearance.Danger,
                new SymbolIcon(SymbolRegular.DismissCircle16),
                TimeSpan.FromSeconds(2)
            );
            }
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

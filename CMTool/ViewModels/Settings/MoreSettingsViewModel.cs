using CMTool.Module;
using CMTool.Views.Settings;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Settings
{
    public partial class MoreSettingsViewModel : ObservableObject, INavigationAware
    {
        private static JObject jObject = JsonRW.Readjson("Assets/DataTime.json");
        private static readonly ISnackbarService _snackbarService = App.GetService<ISnackbarService>();

        private bool _isInitialized = false;

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

        [ObservableProperty]
        private DateTime _WeekStart = Convert.ToDateTime(jObject["WeekStart"].ToString());
        [ObservableProperty]
        private ApplicationTheme _currentTheme = ApplicationTheme.Unknown;


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
                ControlAppearance.Success,
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
                ControlAppearance.Success,
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

            JsonRW.Writejson("Assets/DataTime.json", jObject);

            _snackbarService.Show(
                "保存成功",
                "重启后生效",
                ControlAppearance.Success,
                new SymbolIcon(SymbolRegular.CheckmarkCircle16),
                TimeSpan.FromSeconds(2)
            );
        }

        [RelayCommand]
        public void OnChangeTheme(object state)
        {
            switch (state)
            {
                case true:
                    if (CurrentTheme == ApplicationTheme.Light)
                        break;

                    ApplicationThemeManager.Apply(ApplicationTheme.Light);
                    CurrentTheme = ApplicationTheme.Light;
                    _snackbarService.Show("切换成功","已切换到日间模式",ControlAppearance.Success,new SymbolIcon(SymbolRegular.CheckmarkCircle16),TimeSpan.FromSeconds(2));
                    break;

                default:
                    if (CurrentTheme == ApplicationTheme.Dark)
                        break;

                    ApplicationThemeManager.Apply(ApplicationTheme.Dark);
                    CurrentTheme = ApplicationTheme.Dark;
                    _snackbarService.Show("切换成功", "已切换到夜间模式", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
                    break;
            }
        }

        [RelayCommand]
        public async Task OnChangeProtectAsync(object state)
        {
            switch (state)
            {
                case true:
                    try
                    {
                        Process[] processes = Process.GetProcesses();
                        bool pstate = true;
                        bool corestate = false;
                        bool ability = false;
                        foreach (Process p in processes) { if (p.ProcessName == "SeewoAbility" || p.ProcessName == "SeewoCore") { p.Kill(); }}

                        while (pstate) 
                        {
                            processes = Process.GetProcesses();
                            foreach (Process p in processes) 
                            { 
                                if (p.ProcessName == "SeewoAbility") { ProcessMgr.SuspendProcess(p.Id); ability = true; }
                                if (p.ProcessName == "SeewoCore") { ProcessMgr.SuspendProcess(p.Id); corestate = true; }
                            }
                            if (ability && corestate) { pstate = false; }
                            await Task.Delay(2000);
                        }
                        _snackbarService.Show("操作成功", "", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
                    }
                    catch (Exception)
                    {
                        _snackbarService.Show("操作失败", "发生错误", ControlAppearance.Danger, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
                    }
                    break;

                default:
                    break;
            }
        }
    }
}

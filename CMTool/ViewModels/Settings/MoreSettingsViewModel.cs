using CMTool.Module;
using CMTool.ViewModels.Windows;
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
        private static JObject jObject = FileIO.GetData("Time");
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
            if (FileIO.ReData("Time") && FileIO.ReData("Class") && FileIO.ReData("Work") && FileIO.ReData("Settings"))
            {
                _snackbarService.Show("重置成功", "建议立刻重启以完全生效", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
                Process.Start(Environment.ProcessPath);
                Application.Current.Shutdown();
            }
            else
            {
                _snackbarService.Show("重置失败", "发生错误", ControlAppearance.Danger, new SymbolIcon(SymbolRegular.DismissCircle16), TimeSpan.FromSeconds(2));
            }
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

            if (isOk) { _snackbarService.Show("操作成功", "已" + textOK, ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2)); }
            else { _snackbarService.Show("操作失败", "未能" + textOK, ControlAppearance.Danger, new SymbolIcon(SymbolRegular.DismissCircle16), TimeSpan.FromSeconds(2)); }
        }

        [RelayCommand]
        private void OnSave()
        {
            jObject["WeekStart"] = WeekStart.ToString();

            FileIO.WriteJsonFile("Assets/Data/DataTime.json", jObject);
            SubWindowViewModel.TimeJson = jObject;
            App.GetService<SubWindowViewModel>().Refresh("Time");

            _snackbarService.Show("保存成功", "更改已应用", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
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
                    _snackbarService.Show("切换成功", "已切换到日间模式", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
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
        public void OnChangeProtect(object state)
        {
            JObject jobject = FileIO.GetData("Settings");
            switch (state)
            {
                case true:
                    ProtectionControl.Start();
                    jobject["Safe"] = "true";
                    FileIO.WriteJsonFile("Assets/Data/DataSettings.json", jobject);
                    _snackbarService.Show("启用成功", "安全保护已启用，将拦截不友好的恶意程序", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
                    break;

                default:
                    ProtectionControl.Stop();
                    jobject["Safe"] = "false";
                    FileIO.WriteJsonFile("Assets/Data/DataSettings.json", jobject);
                    _snackbarService.Show("禁用成功", "安全保护已关闭，将不会再拦截不友好的恶意程序", ControlAppearance.Danger, new SymbolIcon(SymbolRegular.DismissCircle16), TimeSpan.FromSeconds(2));
                    break;
            }
        }
    }
}

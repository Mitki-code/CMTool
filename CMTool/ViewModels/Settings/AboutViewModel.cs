using CMTool.Module;
using CMTool.Views.Settings;
using Newtonsoft.Json;
using System.Diagnostics;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Settings
{
    public partial class AboutViewModel : ObservableObject
    {
        private static string appVersionO = Application.ResourceAssembly.GetName().Version.ToString();
        private static string appRing = "正式版";

        [ObservableProperty]
        private string _appVersion = appVersionO.Remove(appVersionO.LastIndexOf(".0"), 2);
        [ObservableProperty]
        private string _appVersionCopyright = "版本 " + appVersionO.Remove(appVersionO.LastIndexOf(".0"), 2) + "  |  "+ appRing + "  |  © 2023~2024 米缇";
        [ObservableProperty]
        private string _updateState = "";
        private bool updateStateBool = false;
        [ObservableProperty]
        private string _updateButtonState = "检查更新";
        [ObservableProperty]
        private ControlAppearance _updateButtonAState = ControlAppearance.Primary;
        [ObservableProperty]
        private string _updateVersion = "";

        [RelayCommand]
        private async Task OnCheckUpdate()
        {
            UpdateButtonAState = ControlAppearance.Secondary;
            App.GetService<About>().UpdateStateBar.IsOpen = false;
            App.GetService<About>().UpdateStateRing.Visibility = Visibility.Visible;

            if (!updateStateBool)
            {
                UpdateButtonState = "正在检查更新";
                try
                {
                    if (await Update.Check(AppVersion))
                    {
                        UpdateState = "检测到新版本";
                        UpdateVersion = AppVersion + " -> " + Update.newVer;
                        updateStateBool = true;
                        UpdateButtonState = "下载并安装";
                        App.GetService<About>().UpdateStateBar.Severity = InfoBarSeverity.Informational;
                    }
                    else
                    {
                        UpdateState = "当前已是最新版本";
                        UpdateVersion = "";
                        UpdateButtonState = "检查更新";
                        App.GetService<About>().UpdateStateBar.Severity = InfoBarSeverity.Success;
                    }
                }
                catch
                {
                    UpdateState = "发生错误";
                    UpdateVersion = "检查更新失败，请稍后重试";
                    UpdateButtonState = "检查更新";
                    App.GetService<About>().UpdateStateBar.Severity = InfoBarSeverity.Error;
                }

                App.GetService<About>().UpdateStateRing.Visibility = Visibility.Hidden;
                App.GetService<About>().UpdateStateBar.IsOpen = true;

            }
            else
            {
                await Update.Down();
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"UpdatePack.msi");
                Application.Current.Shutdown();
            }

            UpdateButtonAState = ControlAppearance.Primary;
        }

        [RelayCommand]
        private void OnChangeUpdateRing(object state)
        {
            switch (state)
            {
                case true:
                    FileIO.SettingsData.UpdateRing = "dev";
                    FileIO.WriteJsonFile("Assets/Data/DataSettings.json", JsonConvert.SerializeObject(FileIO.SettingsData, Formatting.Indented));
                    break;
                case false:
                    FileIO.SettingsData.UpdateRing = "release";
                    FileIO.WriteJsonFile("Assets/Data/DataSettings.json", JsonConvert.SerializeObject(FileIO.SettingsData, Formatting.Indented));
                    break;
            }
        }
    }
}

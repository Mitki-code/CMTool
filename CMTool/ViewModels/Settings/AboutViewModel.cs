using CMTool.Module;
using CMTool.Views.Settings;
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
    public partial class AboutViewModel : ObservableObject
    {
        private static string appVersionO = Application.ResourceAssembly.GetName().Version.ToString();
        private static readonly ISnackbarService _snackbarService = App.GetService<ISnackbarService>();

        [ObservableProperty]
        private string _appVersion = appVersionO.Remove(appVersionO.LastIndexOf(".0"), 2);
        [ObservableProperty]
        private string _appVersionCopyright = "版本 " + appVersionO.Remove(appVersionO.LastIndexOf(".0"), 2) + "  |  © 2023~2024 米缇";
        [ObservableProperty]
        private string _updateState = "";
        private bool updateStateBool = false;
        [ObservableProperty]
        private string _updateButtonState = "检查更新";
        [ObservableProperty]
        private string _updateVersion = "";

        [RelayCommand]
        private async void OnCheckUpdate() 
        {
            App.GetService<About>().UpdateStateRing.Visibility = Visibility.Visible;

            if (!updateStateBool)
            {
                try
                {
                    if (await AutoUpdate.Check(AppVersion))
                    {
                        UpdateState = "检测到新版本";
                        UpdateVersion = AppVersion + " -> " + AutoUpdate.newVer;
                        updateStateBool = true;
                        UpdateButtonState = "获取更新";
                        //await AutoUpdate.Down();
                    }
                    else
                    {
                        UpdateState = "当前已是最新版本";
                        UpdateVersion = "";
                    }
                    App.GetService<About>().UpdateStateBar.Severity = InfoBarSeverity.Informational;
                }
                catch
                {
                    UpdateState = "发生错误";
                    UpdateVersion = "检查更新失败，请稍后重试";
                    App.GetService<About>().UpdateStateBar.Severity = InfoBarSeverity.Error;
                }

                App.GetService<About>().UpdateStateRing.Visibility = Visibility.Hidden;
                App.GetService<About>().UpdateStateBar.IsOpen = true;
            }
            else
            {
                await AutoUpdate.Down();
                Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"temp.exe");
                Application.Current.Shutdown();
            }
            
        }
    }
}

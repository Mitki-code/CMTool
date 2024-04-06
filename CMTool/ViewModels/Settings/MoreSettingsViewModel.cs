using CMTool.Module;
using Newtonsoft.Json.Linq;
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
            JsonRW.Writejson("Assets/DataClass.json", JsonRW.Readjson("Assets/Re/DataClass.json"));
            JsonRW.Writejson("Assets/DataTime.json", JsonRW.Readjson("Assets/Re/DataTime.json"));
            JsonRW.Writejson("Assets/DataWork.json", JsonRW.Readjson("Assets/Re/DataWork.json"));

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

            if (isOk)
            {
                _snackbarService.Show(
                "操作成功",
                "已" + textOK,
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
    }
}

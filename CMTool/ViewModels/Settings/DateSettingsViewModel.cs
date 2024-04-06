using CMTool.Module;
using Newtonsoft.Json.Linq;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Settings
{
    public partial class DateSettingsViewModel : ObservableObject
    {
        public static JObject jObject = JsonRW.Readjson("Assets/DataTime.json");
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

            JsonRW.Writejson("Assets/DataTime.json", jObject);

            _snackbarService.Show(
                "保存成功",
                "重启后生效",
                ControlAppearance.Success,
                new SymbolIcon(SymbolRegular.CheckmarkCircle16),
                TimeSpan.FromSeconds(2)
            );
        }
    }
}

using CMTool.Models;
using CMTool.Services;
using CMTool.Views.Windows;
using Newtonsoft.Json.Linq;

namespace CMTool.ViewModels.Windows
{
    public partial class SubWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "CMTool - Dev";

        /// private string JsonData = new JsonRW.Readjson("pack://application:,,,/Assets/wpfui-icon-256.png");

        private static JObject jObject = JsonRW.Readjson("Assets/MianData.json");
        private static DateTime ETime = Convert.ToDateTime(jObject["Time"].ToString());

        [ObservableProperty]
        private string _EventText = "距离" + jObject["Event"].ToString() + "还有";
        [ObservableProperty]
        private string _EventDateTime = DateTimeM.GetTime(ETime);


        private readonly WindowsProviderService _windowsProviderService;
        public SubWindowViewModel(WindowsProviderService windowsProviderService)
        {
            _windowsProviderService = windowsProviderService;
        }

        [RelayCommand]
        private void OnOpenWindow()
        {
            ///ApplicationHostService.HandleActivationAsyncMain();
            _windowsProviderService.Show<MainWindow>();
        }
    }
}

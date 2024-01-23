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
        private string _EventDateTime = DateTimeM.GetTime(ETime,"Days",false) + "天";
        [ObservableProperty]
        private string _ClassTable = ReadClassTable(jObject);
        [ObservableProperty]
        private string _WorkTable = "\n\n\n\n\n\n\n\n\n";

        private readonly WindowsProviderService _windowsProviderService;
        public SubWindowViewModel(WindowsProviderService windowsProviderService)
        {
            _windowsProviderService = windowsProviderService;
        }

        [RelayCommand]
        private void OnOpenWindow()
        {
            _windowsProviderService.Show<MainWindow>();
        }
        [RelayCommand]
        private void OnClash()
        {
            System.Diagnostics.Process.Start("D:\\Program Files\\clash\\clash.exe");
        }

        private static string ReadClassTable(JObject jObject)
        {
            string ClassTable = "";
            string Week = DateTime.Today.DayOfWeek.ToString();
            string OTWeekString = DateTimeM.GetTime(Convert.ToDateTime(jObject["WeekStart"].ToString()), "Weeks", true);
            int OTWeek = Math.Abs(int.Parse(OTWeekString));

            foreach (JValue property in jObject["ClassTable"][Week])
            {
                if (property.ToString().Contains("|"))
                {
                    string[] ClassTableWeek = property.ToString().Split('|');
                    if (OTWeek % 2 == 0) { ClassTable = ClassTable + ClassTableWeek[1] + "\n"; }
                    else {  ClassTable = ClassTable + ClassTableWeek[0] + "\n"; }
                    ;
                }
                else
                {
                    ClassTable = ClassTable + property.ToString() + "\n";
                }                                 
            }
            return ClassTable;
        }
    }
}

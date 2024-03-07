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
        private string _WorkTable = ReadWorkTable(jObject)[0];
        [ObservableProperty]
        private string _NameTable = ReadWorkTable(jObject)[1];

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
                    else {  ClassTable = ClassTable + ClassTableWeek[0] + "\n"; };
                }
                else
                {
                    ClassTable = ClassTable + property.ToString() + "\n";
                }                                 
            }
            return ClassTable;
        }

        private static string[] ReadWorkTable(JObject jObject)
        {
            string WorkTable = "";
            string NameTable = "";
            string Work = "0";
            string Week = DateTime.Today.DayOfWeek.ToString();
            string OTWeekString = DateTimeM.GetTime(Convert.ToDateTime(jObject["WeekStart"].ToString()), "Weeks", true);
            int OTWeek = Math.Abs(int.Parse(OTWeekString));
            int start = 0;
            int end = 0;


            foreach (JValue property in jObject["WorkTable"]["Work"])
            {
                if (property.ToString() != Work && Work != "0") 
                {
                    WorkTable = WorkTable + Work + "\n";

                    for (int i = start; i < end;i ++)
                    {
                        JValue WorkValue = (JValue)jObject["WorkTable"][Week][i];

                        if (WorkValue.ToString().Contains("|"))
                        {
                            string[] WorkTableWeek = WorkValue.ToString().Split('|');
                            if (OTWeek % 2 == 0) { NameTable = NameTable + WorkTableWeek[1] + "\n"; }
                            else { NameTable = NameTable + WorkTableWeek[0] + "\n"; };
                        }
                        else
                        {
                            NameTable = NameTable + WorkValue.ToString() + "\n";
                        }
                    }

                    while (WorkTable.Split("\n").Length < end+1) { WorkTable += "\n"; }
                    start = end;
                }
                end++;
                Work = property.ToString();
            }

            while (WorkTable.Split("\n").Length < 9) { WorkTable += "\n"; }
            while (NameTable.Split("\n").Length < 9) { NameTable += "\n"; }
            string[] WNList = {WorkTable,NameTable};

            return WNList;
        }
    }
}

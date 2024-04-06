using CMTool.Models.SubWindow;
using CMTool.Module;
using CMTool.Services;
using CMTool.Views.Windows;
using Newtonsoft.Json.Linq;
using PropertyChanged;


namespace CMTool.ViewModels.Windows
{
    [AddINotifyPropertyChangedInterface]
    public partial class SubWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "CMTool";

        public static JObject TimeJson = JsonRW.Readjson("Assets/DataTime.json");
        public static JObject ClassJson = JsonRW.Readjson("Assets/DataClass.json");
        public static JObject WorkJson = JsonRW.Readjson("Assets/DataWork.json");
        private static DateTime ETime = Convert.ToDateTime(TimeJson["Time"].ToString());

        [ObservableProperty]
        private string _EventText = "距离" + TimeJson["Event"].ToString() + "还有";
        [ObservableProperty]
        private string _EventDateTime = Time.GetTimeDifference("D", ETime) + "天";
        [ObservableProperty]
        private string _ClassTable = ReadClassTable(ClassJson, TimeJson["WeekStart"].ToString());

        private SubWindowModels _subWindowModels;
        public SubWindowModels subWindowModels { get { _subWindowModels ??= new SubWindowModels(); return _subWindowModels; } set { _subWindowModels = value; } }

        public void RefreshTable()
        {
            subWindowModels.WorkTable = ReadWorkTable(WorkJson, TimeJson["WeekStart"].ToString())[0];
            subWindowModels.NameTable = ReadWorkTable(WorkJson, TimeJson["WeekStart"].ToString())[1];
            subWindowModels = subWindowModels;
        }


        [RelayCommand]
        private void OnOpenWindow()
        {
            WindowsProviderService _windowsProviderService = App.GetService<WindowsProviderService>();
            _windowsProviderService.Show<MainWindow>();
        }

        internal static string ReadClassTable(JObject jObject, string WeekStart)
        {
            string ClassTable = "";
            string Week = DateTime.Today.DayOfWeek.ToString();
            string OTWeekString = Math.Abs(Time.GetTimeDifference("W", Convert.ToDateTime(WeekStart)) - 1).ToString();
            int OTWeek = Math.Abs(int.Parse(OTWeekString));

            foreach (JValue property in jObject[Week])
            {
                ClassTable = SearchList(ClassTable, OTWeek, property);
            }
            return ClassTable;
        }

        internal static string[] ReadWorkTable(JObject jObject, string WeekStart)
        {
            string WorkTable = "";
            string NameTable = "";
            string Work = "0";
            string Week = DateTime.Today.DayOfWeek.ToString();
            string OTWeekString = Math.Abs(Time.GetTimeDifference("W", Convert.ToDateTime(WeekStart)) - 1).ToString();
            int OTWeek = Math.Abs(int.Parse(OTWeekString));
            int start = 0;
            int end = 0;


            foreach (JValue property in jObject["Work"])
            {
                if (property.ToString() != Work && Work != "0")
                {
                    for (int i = start; i < end; i++)
                    {
                        JValue WorkValue = (JValue)jObject[Week][i];
                        if (WorkValue.ToString() != "")
                        {
                            if (start - i == 0) { WorkTable = WorkTable + Work + "\n"; }

                            NameTable = SearchList(NameTable, OTWeek, WorkValue);

                            if (i == end - 1)
                            {
                                while (WorkTable.Split("\n").Length < end + 1) { WorkTable += "\n"; }
                                i++;
                            }
                        }
                    }
                    start = end;
                }
                end++;
                Work = property.ToString();
            }

            while (WorkTable.Split("\n").Length < 9) { WorkTable += "\n"; }
            while (NameTable.Split("\n").Length < 9) { NameTable += "\n"; }
            string[] WNList = { WorkTable, NameTable };

            return WNList;
        }

        private static string SearchList(string Table, int OTWeek, JValue JsonValue)
        {
            if (JsonValue.ToString().Contains("|"))
            {
                string[] TableWeek = JsonValue.ToString().Split("|");
                if (OTWeek % 2 == 0) { Table = Table + TableWeek[1] + "\n"; }
                else { Table = Table + TableWeek[0] + "\n"; };
            }
            else
            {
                Table = Table + JsonValue.ToString() + "\n";
            }


            return Table;
        }
    }
}

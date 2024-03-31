using CMTool.Models.SubWindow;
using CMTool.Module;
using CMTool.Resources;
using CMTool.Services;
using CMTool.Views.Windows;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System.ComponentModel;
using System.Xml;

namespace CMTool.ViewModels.Windows
{
    //[AddINotifyPropertyChangedInterface]
    public partial class SubWindowViewModel : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty]
        private string _applicationTitle = "CMTool";

        /// private string JsonData = new JsonRW.Readjson("pack://application:,,,/Assets/wpfui-icon-256.png");

        public static JObject jObject = JsonRW.Readjson("Assets/MianData.json");
        private static DateTime ETime = Convert.ToDateTime(jObject["Time"].ToString());

        [ObservableProperty]
        private string _EventText = "距离" + jObject["Event"].ToString() + "还有";
        [ObservableProperty]
        private string _EventDateTime = DateTimeM.GetTime(ETime, "Days", false) + "天";
        [ObservableProperty]
        private string _ClassTable = ReadClassTable(jObject);
        //[ObservableProperty]
        //private static string _WorkTable = ReadWorkTable(jObject)[0];
        //[ObservableProperty]
        //private static string _NameTable = ReadWorkTable(jObject)[1];
        //public static event PropertyChangedEventHandler StaticProgressChanged;
        //private static string workTable;
        //private static string nameTable;
        //public static string NameTable { get { return nameTable; } set { nameTable = value; StaticProgressChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(NameTable))); } }
        //public static string WorkTable { get { return workTable; } set { workTable = value; StaticProgressChanged?.Invoke(null, new PropertyChangedEventArgs(nameof(WorkTable))); } }

        private SubWindowModels _subWindowModels;
        public SubWindowModels subWindowModels{ get { _subWindowModels ??= new SubWindowModels(); return _subWindowModels; } set{ _subWindowModels = value; RaisePropertyChanged("subWindowModels"); } }

        public void RefreshTable()
        {
            subWindowModels.WorkTable = ReadWorkTable(jObject)[0];
            subWindowModels.NameTable = ReadWorkTable(jObject)[1];
            subWindowModels = subWindowModels;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyChanged)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyChanged));

        }

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

        internal static string ReadClassTable(JObject jObject)
        {
            string ClassTable = "";
            string Week = DateTime.Today.DayOfWeek.ToString();
            string OTWeekString = DateTimeM.GetTime(Convert.ToDateTime(jObject["WeekStart"].ToString()), "Weeks", true);
            int OTWeek = Math.Abs(int.Parse(OTWeekString));

            foreach (JValue property in jObject["ClassTable"][Week])
            {
                ClassTable = SearchList(ClassTable, OTWeek, property);
            }
            return ClassTable;
        }

        internal static string[] ReadWorkTable(JObject jObject)
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
                    for (int i = start; i < end; i++)
                    {
                        JValue WorkValue = (JValue)jObject["WorkTable"][Week][i];
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

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

        public static JObject TimeJson = FileIO.GetData("Time");
        public static JObject ClassJson = FileIO.GetData("Class");
        public static JObject WorkJson = FileIO.GetData("Work");
        private static DateTime ETime = Convert.ToDateTime(TimeJson["Time"].ToString());

        [ObservableProperty]
        private string _EventText = "距离" + TimeJson["Event"].ToString() + "还有";
        [ObservableProperty]
        private string _EventDateTime = Time.GetTimeDifference("D", ETime) + "天";
        [ObservableProperty]
        private string _ClassTable = ReadClassTable(ClassJson,TimeJson["WeekStart"].ToString());
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
            subWindowModels.WorkTable = ReadWorkTable(WorkJson, TimeJson["WeekStart"].ToString())[0];
            subWindowModels.NameTable = ReadWorkTable(WorkJson, TimeJson["WeekStart"].ToString())[1];
            subWindowModels = subWindowModels;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyChanged)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyChanged));

        }

        [RelayCommand]
        private void OnOpenWindow()
        {
            WindowsProviderService _windowsProviderService = App.GetService<WindowsProviderService>();
            _windowsProviderService.Show<MainWindow>();
        }
        [RelayCommand]
        private void OnGenshin()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + @"Assets/GFX/OPGo.wav");
            player.Play();
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://ys.mihoyo.com/cloud/?autobegin=1#/") { UseShellExecute = true });
        }
        [RelayCommand]
        private void OnStarRail()
        {
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + @"Assets/GFX/OPGo.wav");
            //player.Play();
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://sr.mihoyo.com/cloud/?from_channel=adbdsem#/") { UseShellExecute = true });
        }

        internal static string ReadClassTable(JObject jObject,string WeekStart)
        {
            string ClassTable = "";
            string Week = DateTime.Today.DayOfWeek.ToString();
            string OTWeekString = Math.Abs(Time.GetTimeDifference("W", Convert.ToDateTime(WeekStart))-1).ToString();
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
            string OTWeekString = Math.Abs(Time.GetTimeDifference("W", Convert.ToDateTime(WeekStart))-1).ToString();
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

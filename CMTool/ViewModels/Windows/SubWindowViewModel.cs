using CMTool.Module;
using CMTool.Resources;
using CMTool.Services;
using CMTool.Views.Windows;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Net.Http.Headers;


namespace CMTool.ViewModels.Windows
{
    //[AddINotifyPropertyChangedInterface]
    public partial class SubWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "CMTool";

        //public static JObject TimeJson = FileIO.GetData("Time");
        public static JObject ClassJson = FileIO.GetData("Class");
        public static JObject WorkJson = FileIO.GetData("Work");
        private static DateTime ETime = Convert.ToDateTime(FileIO.TimeData.Time);

        [ObservableProperty]
        private string _EventText = "距离" + FileIO.TimeData.Event + "还有";
        [ObservableProperty]
        private string _EventDateTime = Time.GetTimeDifference("D", ETime) + "天";
        [ObservableProperty]
        private string _ClassTable = ReadClassTable(ClassJson, FileIO.TimeData.WeekStart);
        [ObservableProperty]
        private string _WorkTable = ReadWorkTable(WorkJson, FileIO.TimeData.WeekStart)[0];
        [ObservableProperty]
        private string _NameTable = ReadWorkTable(WorkJson, FileIO.TimeData.WeekStart)[1];

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

        private static string ReadClassTable(JObject jObject, string WeekStart)
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

        private static string[] ReadWorkTable(JObject jObject, string WeekStart)
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
            if (JsonValue.ToString().Contains('|'))
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

        internal void Refresh(string category)
        {
            switch (category)
            {
                case "Time":
                    ETime = Convert.ToDateTime(FileIO.TimeData.Time);
                    EventText = "距离" + FileIO.TimeData.Event + "还有";
                    EventDateTime = Time.GetTimeDifference("D", ETime) + "天";
                    break;
                case "Class":
                    ClassTable = ReadClassTable(ClassJson, FileIO.TimeData.WeekStart);
                    break;
                case "Work":
                    WorkTable = ReadWorkTable(WorkJson, FileIO.TimeData.WeekStart)[0];
                    NameTable = ReadWorkTable(WorkJson, FileIO.TimeData.WeekStart)[1];
                    break;
            }
        }
    }
}

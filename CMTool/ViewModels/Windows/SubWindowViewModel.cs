using CMTool.Models.Data;
using CMTool.Module;
using CMTool.Services;
using CMTool.Views.Windows;
using System.Reflection;


namespace CMTool.ViewModels.Windows
{
    //[AddINotifyPropertyChangedInterface]
    public partial class SubWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "CMTool";

        private static DateTime ETime = Convert.ToDateTime(FileIO.TimeData.Time);

        [ObservableProperty]
        private string _EventText = "距离" + FileIO.TimeData.Event + "还有";
        [ObservableProperty]
        private string _EventDateTime = Time.GetTimeDifference("D", ETime) + "天";
        [ObservableProperty]
        private string _ClassTable = ReadClassTable(FileIO.TimeData.WeekStart);
        [ObservableProperty]
        private string _WorkTable = ReadWorkTable(FileIO.TimeData.WeekStart)[0];
        [ObservableProperty]
        private string _NameTable = ReadWorkTable(FileIO.TimeData.WeekStart)[1];

        [RelayCommand]
        private static void OnOpenWindow()
        {
            WindowsProviderService _windowsProviderService = App.GetService<WindowsProviderService>();
            _windowsProviderService.Show<MainWindow>();
        }
        [RelayCommand]
        private static void OnGenshin()
        {
            System.Media.SoundPlayer player = new(AppDomain.CurrentDomain.BaseDirectory + @"Assets/GFX/OPGo.wav");
            player.Play();
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://ys.mihoyo.com/cloud/?autobegin=1#/") { UseShellExecute = true });
        }
        [RelayCommand]
        private static void OnStarRail()
        {
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + @"Assets/GFX/OPGo.wav");
            //player.Play();
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://sr.mihoyo.com/cloud/?from_channel=adbdsem#/") { UseShellExecute = true });
        }

        private static string ReadClassTable(string WeekStart)
        {
            string classTable = "";
            int week = (int)DateTime.Today.DayOfWeek;
            int weekOT = (int)Math.Abs(Time.GetTimeDifference("W", Convert.ToDateTime(WeekStart)) - 1);

            PropertyInfo[] properties = typeof(DataClass).GetProperties();
            var classData = FileIO.ClassData;

            foreach (string property in (properties[week].GetValue(classData) as string[]))
            {
                classTable = SearchList(classTable, weekOT, property);
            }
            return classTable;
        }

        private static string[] ReadWorkTable(string WeekStart)
        {
            string workTable = "";
            string nameTable = "";
            string lastWork = "0";
            int week = (int)DateTime.Today.DayOfWeek;
            int weekOT = (int)Math.Abs(Time.GetTimeDifference("W", Convert.ToDateTime(WeekStart)) - 1);
            int start = 0;
            int end = 0;

            PropertyInfo[] properties = typeof(DataWork).GetProperties();
            var workData = FileIO.WorkData;

            foreach (string work in properties[1].GetValue(workData) as string[])
            {
                if (work != lastWork && lastWork != "0")
                {
                    for (int i = start; i < end; i++)
                    {
                        string name = (properties[week + 1].GetValue(workData) as string[])[i];
                        if (name != "")
                        {
                            if (start - i == 0)
                                workTable += lastWork + "\n";

                            nameTable = SearchList(nameTable, weekOT, name);

                            if (i == end - 1)
                                while (workTable.Split("\n").Length < end + 1) { workTable += "\n"; }; i++;
                        }
                    }
                    start = end;
                }
                end++;
                lastWork = work;
            }
            while (workTable.Split("\n").Length < 9) { workTable += "\n"; }
            while (nameTable.Split("\n").Length < 9) { nameTable += "\n"; }
            string[] table = [workTable, nameTable];
            return table;
        }

        private static string SearchList(string Table, int OTWeek, string Value)
        {
            if (Value.Contains('|'))
            {
                string[] TableWeek = Value.Split("|");
                if (OTWeek % 2 == 0) { Table += TableWeek[1] + "\n"; }
                else { Table += TableWeek[0] + "\n"; };
            }
            else
            {
                Table += Value + "\n";
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
                    ClassTable = ReadClassTable(FileIO.TimeData.WeekStart);
                    break;
                case "Work":
                    WorkTable = ReadWorkTable(FileIO.TimeData.WeekStart)[0];
                    NameTable = ReadWorkTable(FileIO.TimeData.WeekStart)[1];
                    break;
            }
        }
    }
}

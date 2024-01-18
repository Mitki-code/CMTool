using CMTool.Services;
using CMTool.Views.Settings;
using CMTool.Views.Windows;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.ViewModels.Settings
{
    public partial class DateSettingsViewModel : ObservableObject
    {
        private static JObject jObject = JsonRW.Readjson("Assets/MianData.json");

        [ObservableProperty]
        private string _EventName = jObject["Event"].ToString();
        [ObservableProperty]
        private DateTime _EventTime = Convert.ToDateTime(jObject["Time"].ToString());
        [ObservableProperty]
        private string _Tips = "";

        [RelayCommand]
        private void OnSave()
        {
            jObject["Event"] = EventName;
            jObject["Time"] = EventTime.ToString();

            JsonRW.Writejson("Assets/MianData.json", jObject);
            Tips = "需要重启";
        }
    }
}

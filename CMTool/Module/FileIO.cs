using CMTool.Models.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace CMTool.Module
{
    internal class FileIO
    {
        private static readonly string OVersion = Application.ResourceAssembly.GetName().Version.ToString();
        private static readonly string Version = OVersion.Remove(OVersion.LastIndexOf(".0"), 2);

        internal static DataTime TimeData = GetDataTime();
        internal static DataClass ClassData = GetDataClass();
        internal static DataWork WorkData = GetDataWork();
        internal static DataSettings SettingsData = GetDataSettings();

        /// <summary>
        /// 从Json文件中获取JObject
        /// </summary>
        /// <param name="path">Json文件路径</param>
        /// <returns></returns>
        internal static JObject ReadJsonFile(string path)
        {
            StreamReader file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @path);
            JsonTextReader reader = new(file);

            JObject jObject = (JObject)JToken.ReadFrom(reader);
            reader.Close();
            return jObject;
        }

        /// <summary>
        /// 向Json文件中写入JObject
        /// </summary>
        /// <param name="path">Json文件路径</param>
        /// <param name="jobject">写入的对象</param>
        /// <returns></returns>
        internal static bool WriteJsonFile(string path, JObject jobject)
        {
            if (WriteJsonFile(path, jobject.ToString()))
                return true;
            return false;
        }

        internal static bool WriteJsonFile(string path, string json)
        {
            try
            {
                StreamWriter file = new(AppDomain.CurrentDomain.BaseDirectory + @path);
                file.Write(json);
                file.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static JObject CheckDataVersion(string name, JObject jobject)
        {
            try
            {
                if (jobject["Version"].ToString() != Version)
                {
                    switch (name)
                    {
                        case "Time":
                            if (jobject["Version"].ToString() == "2.3.0") { jobject["Version"] = "2.3.0"; }
                            break;
                        case "Class":
                            if (jobject["Version"].ToString() == "2.3.0") { jobject["Version"] = "2.3.0"; }
                            break;
                        case "Work":
                            if (jobject["Version"].ToString() == "2.3.0") { jobject["Version"] = "2.3.0"; }
                            break;
                        case "Settings":
                            if (jobject["Version"].ToString() == "2.3.0") { jobject["Version"] = "2.3.0"; }
                            break;
                    }
                    WriteJsonFile("Assets/Data" + name + ".json", jobject);
                }
            }
            catch
            {
                ReData(name);
            }
            return jobject;
        }

        /// <summary>
        /// 重置各类数据
        /// </summary>
        /// <param name="name">数据名称(Time/Class/Work/Settings)</param>
        /// <returns></returns>
        internal static bool ReData(string name)
        {
            try
            {
                WriteJsonFile("Assets/Data/Data" + name + ".json", ReadJsonFile("Assets/ReData/Data" + name + ".json"));
            }
            catch
            {
                return false;
            }
            return true;
        }

        internal static DataTime GetDataTime()
        {
            DataTime data = new();
            JObject jobject = [];
            try { jobject = ReadJsonFile("Assets/Data/DataTime.json"); }
            catch { }

            data.Version = jobject.SelectToken("Version")?.ToString() ?? Version;
            data.Time = jobject.SelectToken("Time")?.ToString() ?? data.Time;
            data.Event = jobject.SelectToken("Event")?.ToString() ?? data.Event;
            data.WeekStart = jobject.SelectToken("WeekStart")?.ToString() ?? data.WeekStart;

            return data;
        }

        internal static DataClass GetDataClass()
        {
            DataClass data = new();
            JObject jobject = [];
            try { jobject = ReadJsonFile("Assets/Data/DataClass.json"); }
            catch { }

            data.Version = jobject.SelectToken("Version")?.ToString() ?? Version;
            data.Monday = jobject.SelectToken("Monday")?.ToObject<string[]>() ?? data.Monday;
            data.Tuesday = jobject.SelectToken("Tuesday")?.ToObject<string[]>() ?? data.Tuesday;
            data.Wednesday = jobject.SelectToken("Wednesday")?.ToObject<string[]>() ?? data.Wednesday;
            data.Thursday = jobject.SelectToken("Thursday")?.ToObject<string[]>() ?? data.Thursday;
            data.Friday = jobject.SelectToken("Friday")?.ToObject<string[]>() ?? data.Friday;
            data.Saturday = jobject.SelectToken("Saturday")?.ToObject<string[]>() ?? data.Saturday;
            data.Sunday = jobject.SelectToken("Sunday")?.ToObject<string[]>() ?? data.Sunday;

            return data;
        }

        internal static DataWork GetDataWork()
        {
            DataWork data = new();
            JObject jobject = [];
            try { jobject = ReadJsonFile("Assets/Data/DataWork.json"); }
            catch { }

            data.Version = jobject.SelectToken("Version")?.ToString() ?? Version;
            data.Work = jobject.SelectToken("Work")?.ToObject<string[]>() ?? data.Work;
            data.Monday = jobject.SelectToken("Monday")?.ToObject<string[]>() ?? data.Monday;
            data.Tuesday = jobject.SelectToken("Tuesday")?.ToObject<string[]>() ?? data.Tuesday;
            data.Wednesday = jobject.SelectToken("Wednesday")?.ToObject<string[]>() ?? data.Wednesday;
            data.Thursday = jobject.SelectToken("Thursday")?.ToObject<string[]>() ?? data.Thursday;
            data.Friday = jobject.SelectToken("Friday")?.ToObject<string[]>() ?? data.Friday;
            data.Saturday = jobject.SelectToken("Saturday")?.ToObject<string[]>() ?? data.Saturday;
            data.Sunday = jobject.SelectToken("Sunday")?.ToObject<string[]>() ?? data.Sunday;

            return data;
        }

        internal static DataSettings GetDataSettings()
        {
            DataSettings data = new();
            JObject jobject = [];
            try { jobject = ReadJsonFile("Assets/Data/DataSettings.json"); }
            catch { }

            data.Version = jobject.SelectToken("Version")?.ToString() ?? Version;
            data.Safe = jobject.SelectToken("Safe")?.ToString() ?? data.Safe;
            data.Theme = jobject.SelectToken("Theme")?.ToString() ?? data.Theme;

            return data;
        }
    }
}

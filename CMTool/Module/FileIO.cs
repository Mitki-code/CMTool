using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMTool.Module
{
    internal class FileIO
    {
        private static readonly string OVersion = Application.ResourceAssembly.GetName().Version.ToString();
        private static readonly string Version = OVersion.Remove(OVersion.LastIndexOf(".0"), 2);

        /// <summary>
        /// 从Json文件中获取JObject
        /// </summary>
        /// <param name="path">Json文件路径</param>
        /// <returns></returns>
        internal static JObject ReadJsonFile(string path)
        {
            StreamReader file = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @path);
            JsonTextReader reader = new JsonTextReader(file);

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
        internal static bool WriteJsonFile(string path,JObject jobject)
        {
            try { 
                StreamWriter file = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + @path);
                file.Write(jobject.ToString());
                file.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static JObject CheckDataVersion(string name,JObject jobject)
        {
            try {
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

        /// <summary>
        /// 获取各类数据
        /// </summary>
        /// <param name="name">数据名称(Time/Class/Work/Settings)</param>
        /// <returns></returns>
        internal static JObject GetData(string name)
        {
            JObject jobject;
            try
            {
                jobject = ReadJsonFile("Assets/Data/Data" + name + ".json");
                jobject = CheckDataVersion(name, jobject);

                return jobject;
            }
            catch
            {
                ReData(name);
                jobject = ReadJsonFile("Assets/Data/Data" + name + ".json");
                return jobject;
            }
        }
    }
}

﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CMTool.Module
{
    internal static class JsonRW
    {
        public static JObject Readjson(string jsonfile)
        {
            using (System.IO.StreamReader file = System.IO.File.OpenText(AppDomain.CurrentDomain.BaseDirectory + @jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject jObject = (JObject)JToken.ReadFrom(reader);
                    return jObject;
                }
            }
        }
        public static void Writejson(string jsonfile, JObject jObject)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@jsonfile))
            {
                file.Write(jObject.ToString());
            }
        }
    }
}

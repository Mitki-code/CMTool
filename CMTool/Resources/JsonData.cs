using CMTool.Module;
using Newtonsoft.Json.Linq;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Resources
{
    //暂时停用
    internal class JsonData
    {
        public static JObject jObject = JsonRW.Readjson("Assets/MianData.json");


        public static void Refresh()
        {
            jObject = JsonRW.Readjson("Assets/MianData.json");
        }
    }
}

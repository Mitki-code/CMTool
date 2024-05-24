using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models.Data
{
    internal class DataSettings
    {
        [JsonProperty("Version")]
        internal string? Version { get; set; } = "";
        [JsonProperty("Safe")]
        internal string? Safe { get; set; } = "false";
        [JsonProperty("Theme")]
        internal string? Theme { get; set; } = "false";
    }
}

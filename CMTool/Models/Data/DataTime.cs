using Newtonsoft.Json;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models.Data
{
    internal class DataTime
    {
        [JsonProperty("Version")]
        internal string? Version { get; set; } = "";

        [JsonProperty("Event")]
        internal string? Event { get; set; } = " ";

        [JsonProperty("Time")]
        internal string? Time { get; set; } = "2024/1/24 0:00:00";

        [JsonProperty("WeekStart")]
        internal string? WeekStart { get; set; } = "2023/8/27 0:00:00";
    }
}

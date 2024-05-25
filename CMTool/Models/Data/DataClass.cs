using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models.Data
{
    internal class DataClass
    {
        [JsonProperty("Version")]
        public string? Version { get; set; } = "";

        [JsonProperty("Monday")]
        public string[]? Monday { get; set; } = new string[9];

        [JsonProperty("Tuesday")]
        public string[]? Tuesday { get; set; } = new string[9];

        [JsonProperty("Wednesday")]
        public string[]? Wednesday { get; set; } = new string[9];

        [JsonProperty("Thursday")]
        public string[]? Thursday { get; set; } = new string[9];

        [JsonProperty("Friday")]
        public string[]? Friday { get; set; } = new string[9];

        [JsonProperty("Saturday")]
        public string[]? Saturday { get; set; } = new string[9];

        [JsonProperty("Sunday")]
        public string[]? Sunday { get; set; } = new string[9];
    }
}

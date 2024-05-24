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
        internal string? Version { get; set; } = "";

        [JsonProperty("Monday")]
        internal string[]? Monday { get; set; } = new string[9];

        [JsonProperty("Tuesday")]
        internal string[]? Tuesday { get; set; } = new string[9];

        [JsonProperty("Wednesday")]
        internal string[]? Wednesday { get; set; } = new string[9];

        [JsonProperty("Thursday")]
        internal string[]? Thursday { get; set; } = new string[9];

        [JsonProperty("Friday")]
        internal string[]? Friday { get; set; } = new string[9];

        [JsonProperty("Saturday")]
        internal string[]? Saturday { get; set; } = new string[9];

        [JsonProperty("Sunday")]
        internal string[]? Sunday { get; set; } = new string[9];
    }
}

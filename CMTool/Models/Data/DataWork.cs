using Newtonsoft.Json;

namespace CMTool.Models.Data
{
    internal class DataWork
    {
        [JsonProperty("Version")]
        public string? Version { get; set; } = "";

        [JsonProperty("Work")]
        public string[]? Work { get; set; } = new string[9];

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

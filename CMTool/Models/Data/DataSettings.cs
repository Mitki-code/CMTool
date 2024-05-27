using Newtonsoft.Json;

namespace CMTool.Models.Data
{
    internal class DataSettings
    {
        [JsonProperty("Version")]
        internal string? Version { get; set; } = "";
        [JsonProperty("UpdateRing")]
        internal string? UpdateRing { get; set; } = "release";
        [JsonProperty("Safe")]
        internal string? Safe { get; set; } = "false";
        [JsonProperty("Theme")]
        internal string? Theme { get; set; } = "false";
    }
}

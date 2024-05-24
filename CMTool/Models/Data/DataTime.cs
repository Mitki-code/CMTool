using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models.Data
{
    internal class DataTime: IEnumerable
    {
        internal string? Version { get; set; } = "";
        internal string? Event { get; set; } = " ";
        internal string? Time { get; set; } = "2024/1/24 0:00:00";
        internal string? WeekStart { get; set; } = "2023/8/27 0:00:00";

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Version;
            yield return Event;
            yield return Time;
            yield return WeekStart;
        }
    }
}

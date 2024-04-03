using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Module
{
    internal class Time
    {
        public static string GetTime(DateTime timeA, string Mode, bool Abs)
        {
            DateTime timeB = DateTime.Now;	//获取当前时间
            TimeSpan ts = timeA - timeB;	//计算时间差
            double timenum = 0;
            if (Mode == "Days") { timenum = ts.TotalDays; }
            else if (Mode == "Weeks") { timenum = ts.TotalDays / 7; }

            string time;
            if (Abs == true && Mode == "Weeks") { time = Math.Ceiling(Math.Abs(timenum)).ToString(); }
            else if (Abs == true) { time = Math.Floor(Math.Abs(timenum)).ToString(); }
            else { time = Math.Floor(timenum).ToString(); }

            return time;
        }

        internal static double GetTimeDifference(string mode,DateTime startTime,DateTime endTime)
        {
            TimeSpan timeSpan = endTime - startTime;
            double timeDifference = 0;

            switch (mode)
            {
                case "D":
                    timeDifference = timeSpan.TotalDays;
                    break ;
                case "W":
                    timeDifference = timeSpan.TotalDays / 7;
                    break ;
            }

            if (timeDifference <= 0) { timeDifference = Math.Ceiling(timeDifference); }
            else { timeDifference = Math.Floor(timeDifference); }
            return timeDifference;
        }

        internal static double GetTimeDifference(string mode, DateTime endTime)
        {
            DateTime startTime = DateTime.Now;
            double timeDifference = GetTimeDifference(mode, startTime, endTime);
            return timeDifference;
        }

    }
}

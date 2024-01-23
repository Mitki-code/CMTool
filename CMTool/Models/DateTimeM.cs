using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models
{
    internal class DateTimeM
    {
        public static string GetTime(DateTime timeA,string Mode,bool Abs)
        {
            DateTime timeB = DateTime.Now;	//获取当前时间
            TimeSpan ts = timeA - timeB;	//计算时间差
            double timenum = 0;
            if (Mode == "Days") { timenum = ts.TotalDays; }
            else if (Mode =="Weeks") { timenum = ts.TotalDays / 7; }

            string time;
            if (Abs == true && Mode == "Weeks") { time = Math.Ceiling(Math.Abs(timenum)).ToString(); }
            else if (Abs == true) { time = Math.Floor(Math.Abs(timenum)).ToString(); }
            else { time = Math.Floor(timenum).ToString(); }
            
            return time;
        }

    }
}

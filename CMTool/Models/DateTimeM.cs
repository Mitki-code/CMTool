using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models
{
    internal class DateTimeM
    {
        public static string GetTime(DateTime timeA,string Mode)
        {
            DateTime timeB = DateTime.Now;	//获取当前时间
            TimeSpan ts = timeA - timeB;	//计算时间差
            string time = "";
            if (Mode == "Days")
            {
                time = Math.Floor(ts.TotalDays).ToString();	
            }
            else if (Mode =="Weeks")
            {
                time = Math.Ceiling(ts.TotalDays).ToString();	
            }
            
            return time;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models
{
    internal class DateTimeM
    {
        public static string GetTime(DateTime timeA)
        {
            //timeA 表示需要计算
            DateTime timeB = DateTime.Now;	//获取当前时间
            TimeSpan ts = timeA - timeB;	//计算时间差
            string time = Math.Round(ts.TotalDays).ToString();	//将时间差转换为秒
            return time;
        }

    }
}

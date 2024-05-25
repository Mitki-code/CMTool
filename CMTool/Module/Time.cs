namespace CMTool.Module
{
    internal class Time
    {
        /// <summary>
        /// 检查指定时间点是否处于指定时间段内
        /// </summary>
        /// <param name="startTime">起始时间点</param>
        /// <param name="endTime">结束时间点</param>
        /// <param name="time">时间点</param>
        /// <returns></returns>
        internal static bool IsTimeQuantum(DateTime startTime, DateTime endTime, DateTime time)
        {
            if (startTime < time && time < endTime)
                return true;
            return false;
        }
        /// <summary>
        /// 检查指定时间点是否处于指定时间段内
        /// </summary>
        /// <param name="startTime">起始时间点</param>
        /// <param name="endTime">结束时间点</param>
        /// <param name="time">时间点</param>
        /// <returns></returns>
        internal static bool IsTimeQuantum(string startTime, string endTime, string time)
        {
            if (IsTimeQuantum(Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + startTime),
                              Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + endTime),
                              Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + time)))
                return true;
            return false;
        }
        /// <summary>
        /// 检查当前时间点是否处于指定时间段内
        /// </summary>
        /// <param name="startTime">起始时间点</param>
        /// <param name="endTime">结束时间点</param>
        /// <returns></returns>
        internal static bool IsTimeQuantum(string startTime, string endTime)
        {
            if (IsTimeQuantum(Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + startTime),
                              Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + endTime),
                              DateTime.Now))
                return true;
            return false;
        }

        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="mode">模式 D:日计算 W:周计算</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        internal static double GetTimeDifference(string mode, DateTime endTime, DateTime startTime)
        {
            TimeSpan timeSpan = endTime - startTime;
            double timeDifference = 0;

            switch (mode)
            {
                case "D":
                    timeDifference = timeSpan.TotalDays;
                    break;
                case "W":
                    timeDifference = timeSpan.TotalDays / 7;
                    break;
            }

            //if (timeDifference <= 1) { timeDifference = Math.Ceiling(timeDifference); }
            //else { timeDifference = Math.Floor(timeDifference); }
            timeDifference = Math.Ceiling(timeDifference);
            return timeDifference;
        }

        /// <summary>
        /// 计算时间差
        /// </summary>
        /// <param name="mode">模式 D:日计算 W:周计算</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        internal static double GetTimeDifference(string mode, DateTime endTime)
        {
            DateTime startTime = DateTime.Now;
            double timeDifference = GetTimeDifference(mode, endTime, startTime);
            return timeDifference;
        }

        internal static int GetWeek(DateTime time)
        {
            int week = (int)time.DayOfWeek;
            if (week == 0)
                week = 7;
            return week;
        }
    }
}

﻿namespace CMTool.Module
{
    internal class Time
    {

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

    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace CMTool.Module
{
    internal class ProtectionControl
    {
        private static bool controlState = true;
        private static bool runState = true;
        private static System.Timers.Timer timer = new System.Timers.Timer();

        //internal static string state = "";
        internal static void Start()
        {
            timer.Enabled = true;
            timer.Interval = 60000;
            timer.Start();
            timer.Elapsed += new ElapsedEventHandler(TimeManger);
        }
        internal static void Stop()
        {
            try
            {
                timer.Stop();
            }
            catch (Exception)
            {

            }
        }
        private static void TimeManger(object sender, ElapsedEventArgs e)
        {
            if (controlState)
            {
                controlState = false;
                if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 20)
                    Control();
                else if (DateTime.Now.Hour == 22 && DateTime.Now.Minute == 00)
                    Control();
                else if (DateTime.Now.Hour == 14 && DateTime.Now.Minute == 10)
                    UnControl();
                //Control();
            }
        }

        private static async void Control()
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                int coreid = 0;
                int abilityid = 0;
                int whilenum = 0;
                bool pstate = true;
                bool corestate = false;
                bool ability = false;
                foreach (Process p in processes)
                {
                    if (p.ProcessName == "SeewoAbility") { abilityid = p.Id; p.Kill(); }
                    if (p.ProcessName == "SeewoCore") { coreid = p.Id; p.Kill(); }
                    //if (p.ProcessName == "Notepad") { p.Kill(); }
                }

                while (pstate)
                {
                    processes = Process.GetProcesses();
                    foreach (Process p in processes)
                    {
                        if (p.ProcessName == "SeewoAbility" && p.Id != abilityid) { ProcessMgr.SuspendProcess(p.Id); ability = true; }
                        if (p.ProcessName == "SeewoCore" && p.Id != coreid) { ProcessMgr.SuspendProcess(p.Id); corestate = true; }
                        //if (p.ProcessName == "Notepad") { ProcessMgr.SuspendProcess(p.Id); }
                    }
                    if ((ability && corestate) || whilenum > 400) { pstate = false; }
                    Console.WriteLine("111");
                    whilenum++;
                    await Task.Delay(1000);
                }
            }
            catch (Exception)
            {

            }
            controlState = true;
        }

        private static void UnControl()
        {
            try
            {
                Process[] processes = Process.GetProcesses();
                foreach (Process p in processes)
                {
                    if (p.ProcessName == "SeewoAbility") { ProcessMgr.ResumeProcess(p.Id); }
                    if (p.ProcessName == "SeewoCore") { ProcessMgr.ResumeProcess(p.Id); }
                    //if (p.ProcessName == "Notepad") { ProcessMgr.ResumeProcess(p.Id); }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}

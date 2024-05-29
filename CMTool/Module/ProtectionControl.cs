using System.Diagnostics;
using System.Timers;

namespace CMTool.Module
{
    internal class ProtectionControl
    {
        private static bool runState = false;
        private static bool controlState = false;
        private static System.Timers.Timer timer = new();

        //internal static string state = "";
        internal static void Start()
        {
            timer.Enabled = true;
            timer.Interval = 6000;
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
            if (!runState)
            {
                runState = true;
                if (!controlState && Time.IsTimeQuantum("12:20", "14:10"))
                    Control();
                else if (!controlState && Time.IsTimeQuantum("20:00", "23:59"))
                    Control();
                else if (controlState && Time.IsTimeQuantum("14:10", "14:20"))
                    UnControl();
                else
                    runState = false;
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
                    if (p.ProcessName == "SeewoServiceAssistant") { p.Kill(); }
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
            runState = false;
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
                Process.Start(@"C:\Program Files (x86)\Seewo\SeewoService\SeewoHugoLauncher\SeewoHugoLauncher.exe");
            }
            catch (Exception)
            {

            }
            controlState = false;
            runState = false;
        }
    }
}

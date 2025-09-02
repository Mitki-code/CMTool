using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Timers;

namespace CMTool.Module
{
    internal class ProtectionControl
    {
        private static bool runState = false;
        private static bool controlState = false;
        private static System.Timers.Timer timer = new();
        private static bool networkState = true;
        
        private static NetworkConnectionMonitor _networkMonitor = new NetworkConnectionMonitor();

        //internal static string state = "";
        internal static void Start()
        {
            timer.Enabled = true;
            timer.Interval = 6000;
            timer.Start();
            timer.Elapsed += new ElapsedEventHandler(TimeManger);
            _networkMonitor.NetworkStatusChanged += OnNetworkStatusChanged;
            _networkMonitor.Start(15);
        }
        
        private static void OnNetworkStatusChanged(object sender, bool isConnected)
        {
            networkState = isConnected;
        }

        internal static void Stop()
        {
            try
            {
                timer.Stop();
                _networkMonitor.Stop();
                _networkMonitor.NetworkStatusChanged -= OnNetworkStatusChanged;
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
                if (!controlState && Time.IsTimeQuantum("12:00", "14:10") && !networkState)
                    Control();
                else if (!controlState && Time.IsTimeQuantum("17:00", "19:00") && !networkState)
                    Control();
                else if (!controlState && Time.IsTimeQuantum("20:00", "23:59") && networkState)
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
                Dictionary<string, int> killedProcessIds = new Dictionary<string, int>();
                Dictionary<string, bool> processStates = new Dictionary<string, bool>();
                
                //int coreid = 0;
                //int abilityid = 0;
                int whilenum = 0;
                bool pstate = true;
                //bool corestate = false;
                //bool ability = false;
                // foreach (Process p in processes)
                // {
                //     if (p.ProcessName == "SeewoAbility") { abilityid = p.Id; p.Kill(); }
                //     if (p.ProcessName == "SeewoCore") { coreid = p.Id; p.Kill(); }
                //     //if (p.ProcessName == "Notepad") { p.Kill(); }
                // }
                
                foreach (Process p in processes)
                {
                    if (p.ProcessName.StartsWith("Seewo", StringComparison.OrdinalIgnoreCase))
                    {
                        killedProcessIds[p.ProcessName] = p.Id;
                        p.Kill();
                    }
                }


                while (pstate)
                {
                    processes = Process.GetProcesses();
                    // foreach (Process p in processes)
                    // {
                    //     //if (p.ProcessName == "SeewoAbility" && p.Id != abilityid) { ProcessMgr.SuspendProcess(p.Id); ability = true; }
                    //     //if (p.ProcessName == "SeewoCore" && p.Id != coreid) { ProcessMgr.SuspendProcess(p.Id); corestate = true; }
                    //     //if (p.ProcessName == "Notepad") { ProcessMgr.SuspendProcess(p.Id); }
                    // }
                    foreach (Process p in processes)
                    {
                        if (p.ProcessName.StartsWith("Seewo", StringComparison.OrdinalIgnoreCase))
                        {
                            if (!killedProcessIds.ContainsKey(p.ProcessName) || p.Id != killedProcessIds[p.ProcessName])
                            {
                                ProcessMgr.SuspendProcess(p.Id);
                                processStates[p.ProcessName] = true;
                            }
                        }
                    }

                    // if ((ability && corestate) || whilenum > 400) { pstate = false; }
                    
                    if ((processStates.Count > 0 && whilenum > 400) || whilenum > 1000)
                    {
                        pstate = false;
                    }


                    whilenum++;
                    await Task.Delay(100);
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
            }
            catch (Exception)
            {

            }
            controlState = false;
            runState = false;
        }
    }
}

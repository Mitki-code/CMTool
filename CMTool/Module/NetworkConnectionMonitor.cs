using System.Net.Http;
using System.Net.NetworkInformation;
using System.Timers;

public class NetworkConnectionMonitor
{
    private System.Timers.Timer _timer = new();
    public event EventHandler<bool> NetworkStatusChanged;
    private bool _lastNetworkStatus = false;

    public void Start(int intervalInSeconds = 10)
    {
        _timer.Interval = intervalInSeconds * 1000;
        _timer.Elapsed += OnTimerElapsed;
        _timer.Start();
        
        // 立即检查一次当前状态
        CheckNetworkStatus();
    }

    public void Stop()
    {
        _timer.Stop();
        _timer.Elapsed -= OnTimerElapsed;
    }

    private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        await CheckNetworkStatus();
    }

    private async Task CheckNetworkStatus()
    {
        bool currentStatus = CheckInternetConnectionAsync();
        
        // 只有当状态改变时才触发事件
        if (currentStatus != _lastNetworkStatus)
        {
            _lastNetworkStatus = currentStatus;
            NetworkStatusChanged?.Invoke(this, currentStatus);
            Console.WriteLine($"网络连接状态: {(currentStatus ? "已连接" : "已断开")}");
        }
    }

    // 检测网络连接的方法
    private bool CheckInternetConnectionAsync()
    {
        return NetworkInterface.GetIsNetworkAvailable();
    }
}

namespace CMTool.ViewModels.Settings
{
    public partial class AboutViewModel : ObservableObject
    {
        private static string appVersionO = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString();

        [ObservableProperty]
        private string _appVersion = appVersionO.Remove(appVersionO.LastIndexOf("0"), 1).Remove(appVersionO.LastIndexOf("."), 1);

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Appearance;

namespace CMTool.ViewModels.Settings
{
    public partial class AboutViewModel : ObservableObject
    {
        private static string appVersionO = Application.ResourceAssembly.GetName().Version.ToString();

        [ObservableProperty]
        private string _appVersion = appVersionO.Remove(appVersionO.LastIndexOf(".0"), 2);

    }
}

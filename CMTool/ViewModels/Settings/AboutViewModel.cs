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

        [ObservableProperty]
        private string _appVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString();

    }


}

using CMTool.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Settings
{
    public partial class MoreSettingsViewModel : ObservableObject
    {
        [RelayCommand]
        private void OnClose()
        {
            Application.Current.Shutdown();
        }
    }
}

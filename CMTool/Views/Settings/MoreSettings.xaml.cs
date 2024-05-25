using CMTool.Module;
using CMTool.ViewModels.Settings;
using System.Windows.Controls;

namespace CMTool.Views.Settings
{
    /// <summary>
    /// MoreSettings.xaml 的交互逻辑
    /// </summary>
    public partial class MoreSettings : Page
    {
        public MoreSettingsViewModel ViewModel { get; }
        public MoreSettings(MoreSettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
            Check();
        }
        private void Check()
        {
            if (FileIO.SettingsData.Safe == "true")
                ProtectButton.IsChecked = true;
            if (FileIO.SettingsData.Theme == "true")
                ThemeButton.IsChecked = true;
            if (PowerStartManger.IsAutoStart())
                PowerStartButton.IsChecked = true;
            PowerStartButton.IsChecked = false;
        }
    }
}

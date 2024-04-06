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
            CheckAutoStart();
        }

        private void CheckAutoStart()
        {
            if (PowerStartManger.IsAutoStart()) { PowerStartButton.IsChecked = true; }
            else { PowerStartButton.IsChecked = false; }
        }
    }
}

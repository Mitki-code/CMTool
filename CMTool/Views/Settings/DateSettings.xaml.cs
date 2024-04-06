using CMTool.ViewModels.Settings;
using System.Windows.Controls;

namespace CMTool.Views.Settings
{
    /// <summary>
    /// DateSettings.xaml 的交互逻辑
    /// </summary>
    public partial class DateSettings : Page
    {
        public DateSettingsViewModel ViewModel { get; }
        public DateSettings(DateSettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}

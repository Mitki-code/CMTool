using CMTool.Module;
using CMTool.ViewModels.Windows;
using Wpf.Ui.Appearance;

namespace CMTool.Views.Windows
{
    public partial class SubWindow
    {
        public SubWindowViewModel ViewModel { get; set; }
        public SubWindow(SubWindowViewModel viewModel)
        {
            DataContext = this;
            ViewModel = viewModel;

            InitializeComponent();
            Run();

            ShowInTaskbar = false;
            Left = SystemParameters.WorkArea.Width - Width;
            Top = 0;
        }

        private void Run()
        {
            if (FileIO.SettingsData.Safe == "true")
                ProtectionControl.Start();
            if (FileIO.SettingsData.Theme == "true")
                ApplicationThemeManager.Apply(ApplicationTheme.Light);
        }
    }
}

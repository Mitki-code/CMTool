using CMTool.ViewModels.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CMTool.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {

        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel, IServiceProvider serviceProvider, INavigationService navigationService)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            navigationService.SetNavigationControl(RootNavigation);
            RootNavigation.SetServiceProvider(serviceProvider);
        }
    }
}

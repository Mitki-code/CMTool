using CMTool.ViewModels.Windows;
using System.ComponentModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CMTool.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindowViewModel ViewModel { get; }

        protected override void OnClosing(CancelEventArgs a)
        {
            a.Cancel = true;
            this.Hide();
        }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService
        )
        {
            ///Wpf.Ui.Appearance.Watcher.Watch(this);

            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            navigationService.SetNavigationControl(NavigationView);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            contentDialogService.SetContentPresenter(RootContentDialog);

            NavigationView.SetServiceProvider(serviceProvider);
        }
    }
}

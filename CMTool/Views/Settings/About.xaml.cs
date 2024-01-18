using CMTool.ViewModels.Settings;

namespace CMTool.Views.Settings
{
    public partial class About
    {
        public AboutViewModel ViewModel { get; }
        public About(AboutViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}

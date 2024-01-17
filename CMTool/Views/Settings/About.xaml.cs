using CMTool.ViewModels.Settings;
using Wpf.Ui.Controls;

namespace CMTool.Views.Settings
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : INavigableView<AboutViewModel>
    {
        public enum NavigationCacheMode { About }
        public AboutViewModel ViewModel { get; }
        public About(AboutViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}

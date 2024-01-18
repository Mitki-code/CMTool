using CMTool.ViewModels.Windows;

namespace CMTool.Views.Windows
{
    public partial class SubWindow
    {
        public SubWindowViewModel ViewModel { get; }
        public SubWindow(SubWindowViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();


            Left = System.Windows.SystemParameters.WorkArea.Width - Width;
            Top = 0;
        }
    }
}

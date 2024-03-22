using CMTool.Models.SubWindow;
using CMTool.ViewModels.Windows;

namespace CMTool.Views.Windows
{
    public partial class SubWindow
    {
        public SubWindowViewModel ViewModel { get; }
        public SubWindow(SubWindowViewModel viewModel)
        {
            InitializeComponent();
            
            ViewModel = viewModel;
            DataContext = this;
            viewModel.RefreshTable();

            ShowInTaskbar = false;
            Left = System.Windows.SystemParameters.WorkArea.Width - Width;
            Top = 0;
        }

        public static void RefreshTable(SubWindowViewModel viewModel)
        {
            viewModel.RefreshTable();
        }
    }
}

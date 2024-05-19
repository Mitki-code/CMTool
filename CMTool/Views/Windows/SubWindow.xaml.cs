using CMTool.Models.SubWindow;
using CMTool.Module;
using CMTool.ViewModels.Windows;

namespace CMTool.Views.Windows
{
    public partial class SubWindow
    {
        public SubWindowViewModel ViewModel { get;set; }
        public SubWindow(SubWindowViewModel viewModel)
        {
            DataContext = this;
            ViewModel = viewModel;


            InitializeComponent();

            
            RunProtect();

            ShowInTaskbar = false;
            Left = System.Windows.SystemParameters.WorkArea.Width - Width;
            Top = 0;
        }

        private void RunProtect()
        {
            if (FileIO.GetData("Settings")["Safe"].ToString() == "true")
                ProtectionControl.Start();

        }
    }
}

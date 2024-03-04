using CMTool.ViewModels.Settings;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Windows.Controls;
using System.Reflection;

namespace CMTool.Views.Settings
{
    /// <summary>
    /// ClassSettings.xaml 的交互逻辑
    /// </summary>
    public partial class ClassSettings : Page
    {
        public ClassSettingsViewModel ViewModel { get; }
        public ClassSettings(ClassSettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
        private void Date_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var result = e.PropertyName;
            var p = (e.PropertyDescriptor as PropertyDescriptor).ComponentType.GetProperties().FirstOrDefault(x => x.Name == e.PropertyName);

            if (p != null)
            {
                var found = p.GetCustomAttribute<DisplayAttribute>();
                if (found != null) result = found.Name;
            }

            e.Column.Header = result;
        }
    }
}

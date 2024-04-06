using CMTool.ViewModels.Settings;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Controls;

namespace CMTool.Views.Settings
{
    /// <summary>
    /// WorkSettings.xaml 的交互逻辑
    /// </summary>
    public partial class WorkSettings : Page
    {
        public WorkSettingsViewModel ViewModel { get; }

        //private ComboBox SettingWorkMode { get; }
        public WorkSettings(WorkSettingsViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;


            //SettingWorkMode.SelectedIndex = 1;
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

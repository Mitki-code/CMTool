using CMTool.Models;
using CMTool.Models.Data;
using CMTool.Module;
using CMTool.ViewModels.Windows;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using Wpf.Ui.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMTool.ViewModels.Settings
{
    public partial class ClassSettingsViewModel : ObservableObject
    {
        //private static JObject jObject = FileIO.GetData("Class");
        private static readonly ISnackbarService _snackbarService = App.GetService<ISnackbarService>();

        [ObservableProperty]
        private ObservableCollection<ClassList> _ClassTable = GenerateClassList(FileIO.ClassData);

        private static ObservableCollection<ClassList> GenerateClassList(DataClass dataClass)
        {
            var classList = new ObservableCollection<ClassList> { };

            for (int i = 0; i < 9; i++)
            {
                classList.Add(
                    new ClassList
                    {
                        ClassNum = i+1,
                        Monday = dataClass.Monday[i].ToString(),
                        Tuesday = dataClass.Sunday[i].ToString(),
                        Wednesday = dataClass.Wednesday[i].ToString(),
                        Thursday = dataClass.Thursday[i].ToString(),
                        Friday = dataClass.Friday[i].ToString(),
                        Saturday = dataClass.Saturday[i].ToString(),
                        Sunday = dataClass.Sunday[i].ToString(),
                    }
                );
            }
            return classList;
        }
        [RelayCommand]
        private void OnReread()
        {
            ClassTable = GenerateClassList(FileIO.ClassData);
        }
        [RelayCommand]
        private void OnSave()
        {
            try
            {
                if (ClassTable.Count > 9) { throw Error(); }
                int i = 0;
                foreach (ClassList classList in ClassTable)
                {
                    FileIO.ClassData.Monday[i] = classList.Monday;
                    FileIO.ClassData.Tuesday[i] = classList.Tuesday;
                    FileIO.ClassData.Wednesday[i] = classList.Wednesday;
                    FileIO.ClassData.Thursday[i] = classList.Thursday;
                    FileIO.ClassData.Friday[i] = classList.Friday;
                    FileIO.ClassData.Saturday[i] = classList.Saturday;
                    FileIO.ClassData.Sunday[i] = classList.Sunday;

                    i++;
                }
                FileIO.WriteJsonFile("Assets/Data/DataClass.json", JsonConvert.SerializeObject(FileIO.ClassData, Formatting.Indented));
                App.GetService<SubWindowViewModel>().Refresh("Class");

                _snackbarService.Show("保存成功", "更改已应用", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
            }
            catch
            {
                _snackbarService.Show("保存失败", "课程数大于9节", ControlAppearance.Danger, new SymbolIcon(SymbolRegular.ErrorCircle16), TimeSpan.FromSeconds(2));
            }
        }

        private Exception Error()
        {
            throw new NotImplementedException();
        }
    }
}

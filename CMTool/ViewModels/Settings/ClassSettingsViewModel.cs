using CMTool.Models;
using CMTool.Services;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Settings
{
    public partial class ClassSettingsViewModel : ObservableObject
    {
        private static JObject jObject = JsonRW.Readjson("Assets/MianData.json");
        private readonly ISnackbarService _snackbarService;
        private ControlAppearance _snackbarAppearance = ControlAppearance.Success;

        [ObservableProperty]
        private ObservableCollection<ClassList> _ClassTable;

        public ClassSettingsViewModel(ISnackbarService snackbarService)
        {
            _ClassTable = GenerateClassList(jObject);
            _snackbarService = snackbarService;
        }

        private ObservableCollection<ClassList> GenerateClassList(JObject jObject)
        {
            var classList = new ObservableCollection<ClassList> { };

            for (int i = 1; i < 10; i++)
            {
                classList.Add(
                    new ClassList
                    {
                        ClassNum = i,
                        Monday = jObject["ClassTable"]["Monday"][i-1].ToString(),
                        Tuesday = jObject["ClassTable"]["Tuesday"][i-1].ToString(),
                        Wednesday = jObject["ClassTable"]["Wednesday"][i - 1].ToString(),
                        Thursday = jObject["ClassTable"]["Thursday"][i - 1].ToString(),
                        Friday = jObject["ClassTable"]["Friday"][i - 1].ToString(),
                        Saturday = jObject["ClassTable"]["Saturday"][i - 1].ToString(),
                        Sunday = jObject["ClassTable"]["Sunday"][i - 1].ToString(),
                    }
                );
            }
            return classList;
        }

        [RelayCommand]
        private void OnSave()
        {
            foreach (ClassList classList in ClassTable)
            {
                jObject["ClassTable"]["Monday"][classList.ClassNum - 1] = classList.Monday;
                jObject["ClassTable"]["Tuesday"][classList.ClassNum - 1] = classList.Tuesday;
                jObject["ClassTable"]["Wednesday"][classList.ClassNum - 1] = classList.Wednesday;
                jObject["ClassTable"]["Thursday"][classList.ClassNum - 1] = classList.Thursday;
                jObject["ClassTable"]["Friday"][classList.ClassNum - 1] = classList.Friday;
                jObject["ClassTable"]["Saturday"][classList.ClassNum - 1] = classList.Saturday;
                jObject["ClassTable"]["Sunday"][classList.ClassNum - 1] = classList.Sunday;
            }

            JsonRW.Writejson("Assets/MianData.json", jObject);

            _snackbarService.Show(
                "保存成功",
                "重启后生效",
                _snackbarAppearance,
                new SymbolIcon(SymbolRegular.CheckmarkCircle16),
                TimeSpan.FromSeconds(2)
            );
        }

    }
}

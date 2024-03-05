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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMTool.ViewModels.Settings
{
    public partial class ClassSettingsViewModel : ObservableObject
    {
        private static JObject jObject = JsonRW.Readjson("Assets/MianData.json");
        private readonly ISnackbarService _snackbarService;

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
        private void OnReread()
        {
            ClassTable = GenerateClassList(jObject);
        }
        [RelayCommand]
        private void OnSave()
        {
            try{
                if (ClassTable.Count > 9) { throw Error(); }
                int i = 0;
                foreach (ClassList classList in ClassTable)
                {
                    jObject["ClassTable"]["Monday"][i] = classList.Monday;
                    jObject["ClassTable"]["Tuesday"][i] = classList.Tuesday;
                    jObject["ClassTable"]["Wednesday"][i] = classList.Wednesday;
                    jObject["ClassTable"]["Thursday"][i] = classList.Thursday;
                    jObject["ClassTable"]["Friday"][i] = classList.Friday;
                    jObject["ClassTable"]["Saturday"][i] = classList.Saturday;
                    jObject["ClassTable"]["Sunday"][i] = classList.Sunday;

                    i++;
                }

                JsonRW.Writejson("Assets/MianData.json", jObject);

                _snackbarService.Show(
                    "保存成功",
                    "重启后生效",
                    ControlAppearance.Success,
                    new SymbolIcon(SymbolRegular.CheckmarkCircle16),
                    TimeSpan.FromSeconds(2)
                );
            }
            catch{
                _snackbarService.Show(
                    "保存失败",
                    "课程数大于9节",
                    ControlAppearance.Danger,
                    new SymbolIcon(SymbolRegular.ErrorCircle16),
                    TimeSpan.FromSeconds(2)
                );
            }
        }

        private Exception Error()
        {
            throw new NotImplementedException();
        }
    }
}

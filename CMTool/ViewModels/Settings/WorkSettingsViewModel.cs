using CMTool.Models;
using CMTool.Services;
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
    public partial class WorkSettingsViewModel : ObservableObject
    {
        private static JObject jObject = JsonRW.Readjson("Assets/MianData.json");
        private readonly ISnackbarService _snackbarService;

        [ObservableProperty]
        private ObservableCollection<WorkList> _WorkTable;

        public WorkSettingsViewModel(ISnackbarService snackbarService)
        {
            _WorkTable = GenerateWorkList(jObject);
            _snackbarService = snackbarService;
        }
        private ObservableCollection<WorkList> GenerateWorkList(JObject jObject)
        {
            var worklist = new ObservableCollection<WorkList> { };

            for (int i = 1; i < 10; i++)
            {
                worklist.Add(
                    new WorkList
                    {
                        WorkNum = i,
                        Monday = jObject["WorkTable"]["Monday"][i - 1].ToString(),
                        Tuesday = jObject["WorkTable"]["Tuesday"][i - 1].ToString(),
                        Wednesday = jObject["WorkTable"]["Wednesday"][i - 1].ToString(),
                        Thursday = jObject["WorkTable"]["Thursday"][i - 1].ToString(),
                        Friday = jObject["WorkTable"]["Friday"][i - 1].ToString(),
                        Saturday = jObject["WorkTable"]["Saturday"][i - 1].ToString(),
                        Sunday = jObject["WorkTable"]["Sunday"][i - 1].ToString(),
                    }
                );
            }
            return worklist;
        }
        [RelayCommand]
        private void OnReread()
        {
            //ClassTable = GenerateClassList(jObject);
        }
        [RelayCommand]
        private void OnSave()
        {

        }
    }
}

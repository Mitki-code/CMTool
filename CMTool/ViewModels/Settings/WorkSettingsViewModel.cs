using CMTool.Models;
using CMTool.Resources;
using CMTool.ViewModels.Windows;
using CMTool.Views.Settings;
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

        [ObservableProperty]
        private IList<string> _WorkMode = new ObservableCollection<string>
        {
            "分组",
            "随机(WIP)",
            "轮流(WIP)"
        };

        private ObservableCollection<WorkList> GenerateWorkList(JObject jObject)
        {
            var worklist = new ObservableCollection<WorkList> { };

            for (int i = 1; i < 10; i++)
            {
                worklist.Add(
                    new WorkList
                    {
                        Work = jObject["WorkTable"]["Work"][i - 1].ToString(),
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
            WorkTable = GenerateWorkList(jObject);
        }
        [RelayCommand]
        private void OnSave()
        {
            try
            {
                if (WorkTable.Count > 9) { throw Error(); }
                int i = 0;
                foreach (WorkList workList in WorkTable)
                {
                    jObject["WorkTable"]["Work"][i] = workList.Work;
                    jObject["WorkTable"]["Monday"][i] = workList.Monday;
                    jObject["WorkTable"]["Tuesday"][i] = workList.Tuesday;
                    jObject["WorkTable"]["Wednesday"][i] = workList.Wednesday;
                    jObject["WorkTable"]["Thursday"][i] = workList.Thursday;
                    jObject["WorkTable"]["Friday"][i] = workList.Friday;
                    jObject["WorkTable"]["Saturday"][i] = workList.Saturday;
                    jObject["WorkTable"]["Sunday"][i] = workList.Sunday;

                    i++;
                }

                JsonRW.Writejson("Assets/MianData.json", jObject);
                JsonData.Refresh();

                SubWindowViewModel.jObject = JsonRW.Readjson("Assets/MianData.json");
                SubWindowViewModel.RefreshTable();





                _snackbarService.Show(
                    "保存成功",
                    "重启后生效",
                    ControlAppearance.Success,
                    new SymbolIcon(SymbolRegular.CheckmarkCircle16),
                    TimeSpan.FromSeconds(2)
                );
            }
            catch
            {
                _snackbarService.Show(
                    "保存失败",
                    "单天值日人数大于9人",
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

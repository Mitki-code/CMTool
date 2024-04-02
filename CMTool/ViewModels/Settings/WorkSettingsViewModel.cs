using CMTool.Module;
using CMTool.Resources;
using CMTool.Services;
using CMTool.ViewModels.Windows;
using CMTool.Views.Settings;
using CMTool.Views.Windows;
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
        private static JObject jObject = JsonRW.Readjson("Assets/DataWork.json");
        private static readonly ISnackbarService _snackbarService = App.GetService<ISnackbarService>();

        //public SubWindowViewModel ViewModel { get; }
        [ObservableProperty]
        private ObservableCollection<WorkList> _WorkTable = GenerateWorkList(jObject);

        [ObservableProperty]
        private IList<string> _WorkMode = new ObservableCollection<string>
        {
            "分组",
            "随机(WIP)",
            "轮流(WIP)"
        };

        private static ObservableCollection<WorkList> GenerateWorkList(JObject jObject)
        {
            var worklist = new ObservableCollection<WorkList> { };

            for (int i = 1; i < 10; i++)
            {
                worklist.Add(
                    new WorkList
                    {
                        Work = jObject["Work"][i - 1].ToString(),
                        Monday = jObject["Monday"][i - 1].ToString(),
                        Tuesday = jObject["Tuesday"][i - 1].ToString(),
                        Wednesday = jObject["Wednesday"][i - 1].ToString(),
                        Thursday = jObject["Thursday"][i - 1].ToString(),
                        Friday = jObject["Friday"][i - 1].ToString(),
                        Saturday = jObject["Saturday"][i - 1].ToString(),
                        Sunday = jObject["Sunday"][i - 1].ToString(),
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
                    jObject["Work"][i] = workList.Work;
                    jObject["Monday"][i] = workList.Monday;
                    jObject["Tuesday"][i] = workList.Tuesday;
                    jObject["Wednesday"][i] = workList.Wednesday;
                    jObject["Thursday"][i] = workList.Thursday;
                    jObject["Friday"][i] = workList.Friday;
                    jObject["Saturday"][i] = workList.Saturday;
                    jObject["Sunday"][i] = workList.Sunday;

                    i++;
                }

                JsonRW.Writejson("Assets/DataWork.json", jObject);
                JsonData.Refresh();
                //SubWindowViewModel.jObject = JsonRW.Readjson("Assets/MianData.json");
                //App.GetService<SubWindow>().RefreshTable();

                


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

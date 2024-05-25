using CMTool.Models;
using CMTool.Models.Data;
using CMTool.Module;
using CMTool.ViewModels.Windows;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CMTool.ViewModels.Settings
{
    public partial class WorkSettingsViewModel : ObservableObject
    {
        private static readonly ISnackbarService _snackbarService = App.GetService<ISnackbarService>();

        [ObservableProperty]
        private ObservableCollection<WorkList> _WorkTable = GenerateWorkList(FileIO.WorkData);

        [ObservableProperty]
        private IList<string> _WorkMode = new ObservableCollection<string>
        {
            "分组",
            "随机(WIP)",
            "轮流(WIP)"
        };

        private static ObservableCollection<WorkList> GenerateWorkList(DataWork dataWork)
        {
            var workList = new ObservableCollection<WorkList> { };

            for (int i = 0; i < 9; i++)
            {
                workList.Add(
                    new WorkList
                    {
                        Work = dataWork.Work[i].ToString(),
                        Monday = dataWork.Monday[i].ToString(),
                        Tuesday = dataWork.Tuesday[i].ToString(),
                        Wednesday = dataWork.Wednesday[i].ToString(),
                        Thursday = dataWork.Thursday[i].ToString(),
                        Friday = dataWork.Friday[i].ToString(),
                        Saturday = dataWork.Saturday[i].ToString(),
                        Sunday = dataWork.Sunday[i].ToString(),
                    }
                );
            }
            return workList;
        }
        [RelayCommand]
        private void OnReread()
        {
            WorkTable = GenerateWorkList(FileIO.WorkData);
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
                    FileIO.WorkData.Work[i] = workList.Work;
                    FileIO.WorkData.Monday[i] = workList.Monday;
                    FileIO.WorkData.Tuesday[i] = workList.Tuesday;
                    FileIO.WorkData.Wednesday[i] = workList.Wednesday;
                    FileIO.WorkData.Thursday[i] = workList.Thursday;
                    FileIO.WorkData.Friday[i] = workList.Friday;
                    FileIO.WorkData.Saturday[i] = workList.Saturday;
                    FileIO.WorkData.Sunday[i] = workList.Sunday;

                    i++;
                }

                FileIO.WriteJsonFile("Assets/Data/DataWork.json", JsonConvert.SerializeObject(FileIO.WorkData, Formatting.Indented));
                App.GetService<SubWindowViewModel>().Refresh("Work");

                _snackbarService.Show("保存成功", "更改已应用", ControlAppearance.Success, new SymbolIcon(SymbolRegular.CheckmarkCircle16), TimeSpan.FromSeconds(2));
            }
            catch
            {
                _snackbarService.Show("保存失败", "单天值日人数大于9人", ControlAppearance.Danger, new SymbolIcon(SymbolRegular.ErrorCircle16), TimeSpan.FromSeconds(2));
            }
        }

        private Exception Error()
        {
            throw new NotImplementedException();
        }
    }
}

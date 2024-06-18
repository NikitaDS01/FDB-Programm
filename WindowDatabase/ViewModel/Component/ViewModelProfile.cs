using FileDB.Core.Data;
using FileDB.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.ViewModel.RootVM;
using WindowDatabase.Windows.OtherWindow;
using WindowDatabase.Windows;
using FileDB.Core.Data.Tables;
using WindowDatabase.Core.Data.TableValue;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelProfile : INotifyPropertyChanged, IViewModel, IEntityTable<Profile>
    {
        private Table _tableProfile;
        private Profile _item;

        public ViewModelProfile(Table tableSquareIn)
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");

            AddCommand = new RelayCommand(AddContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            OpenCommand = new RelayCommand(OpenSelectItem);
            CreateGraphic1Command = new RelayCommand(CreateGraphic1Window);
            CreateGraphic2Command = new RelayCommand(CreateGraphic2Window);
            CreateGraphic3Command = new RelayCommand(CreateGraphic3Window);
            CreateGraphic4Command = new RelayCommand(CreateGraphic4Window);
            CreateDiscrepancies1Command = new RelayCommand(CreateDiscrepancies1);
            CreateDiscrepancies2Command = new RelayCommand(CreateDiscrepancies2);
            ReturnCommand = new RelayCommand(ReturnWindow);

            _tableProfile = tableSquareIn;

        }
        public string Name => Database.CurrentDatabase.Name;
        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public ICommand CreateGraphic1Command { get; private set; }
        public ICommand CreateGraphic2Command { get; private set; }
        public ICommand CreateGraphic3Command { get; private set; }
        public ICommand CreateGraphic4Command { get; private set; }
        public ICommand CreateDiscrepancies1Command { get; private set; }
        public ICommand CreateDiscrepancies2Command { get; private set; }
        public ICommand ReturnCommand { get; private set; }
        public Profile SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Profile> Items
        { get { return GetData(); } }


        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreateProfileWindow(),
                                     new ViewModelCreateProfile(_tableProfile));
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreateProfileWindow(),
                                     new ViewModelCreateProfile(_tableProfile, SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            if (_tableProfile.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                tbl.DirectoryTable.Delete(true);
                _tableProfile.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
                _tableProfile.RemoveChildTable(SelectedItem.Name);
                OnPropertyChanged(nameof(Items));
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        private void CreateGraphic1Window(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            if (_tableProfile.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var recordProjects = tbl.Select(new FileDB.Core.Data.RecordSearch(0));
                var picket = FileSerializer.DeserializeArray<Picket>(recordProjects);
                var vm = new ViewModelGraphic();
                var list = new Result[picket.Length];
                for(int index = 0; index < picket.Length; index++)
                {
                    list[index] = picket[index].Result;
                }
                vm.AddPoints(list, tbl.Name);
                WindowManager.OpenDialog(new GraphicWindow(), vm);
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        private void CreateGraphic2Window(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            if (_tableProfile.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var recordProjects = tbl.Select(new FileDB.Core.Data.RecordSearch(0));
                var picket = FileSerializer.DeserializeArray<Picket>(recordProjects);
                var vm = new ViewModelGraphic();
                var list = new Result[picket.Length];
                for (int index = 0; index < picket.Length; index++)
                {
                    list[index] = picket[index].Transformant_1;
                }
                vm.AddPoints(list, tbl.Name);
                WindowManager.OpenDialog(new GraphicWindow(), vm);
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        private void CreateGraphic3Window(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            if (_tableProfile.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var record1Projects = tbl.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "рядовое", RecordSearch.ConditionType.Equals));
                var record2Projects = tbl.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "контрольное", RecordSearch.ConditionType.Equals));
                
                var picket1 = FileSerializer.DeserializeArray<Picket>(record1Projects);
                var picket2 = FileSerializer.DeserializeArray<Picket>(record2Projects);

                var vm = new ViewModelGraphic();
                var list1 = new Result[picket1.Length];
                var list2 = new Result[picket2.Length];

                for (int index = 0; index < picket1.Length; index++)
                    list1[index] = picket1[index].Result;
                for (int index = 0; index < picket2.Length; index++)
                    list2[index] = picket2[index].Result;

                vm.AddPoints(list1, tbl.Name + " - Рядовые");
                vm.AddPoints(list2, tbl.Name + " - Контрольные");
                WindowManager.OpenDialog(new GraphicWindow(), vm);
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        private void CreateGraphic4Window(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            if (_tableProfile.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var record1Projects = tbl.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "рядовое", RecordSearch.ConditionType.Equals));
                var record2Projects = tbl.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "опытное", RecordSearch.ConditionType.Equals));
                
                var picket1 = FileSerializer.DeserializeArray<Picket>(record1Projects);
                var picket2 = FileSerializer.DeserializeArray<Picket>(record2Projects);

                var vm = new ViewModelGraphic();
                var list1 = new Result[picket1.Length];
                var list2 = new Result[picket2.Length];

                for (int index = 0; index < picket1.Length; index++)
                    list1[index] = picket1[index].Result;
                for (int index = 0; index < picket2.Length; index++)
                    list2[index] = picket2[index].Result;

                vm.AddPoints(list1, tbl.Name + " - Рядовые");
                vm.AddPoints(list2, tbl.Name + " - Опытные");
                WindowManager.OpenDialog(new GraphicWindow(), vm);
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        //опытное
        private void CreateDiscrepancies1(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            var path = SelectPathDiscrepancies();
            if(string.IsNullOrEmpty(path))
            {
                ShowDialog.Warning("Вы не выбрали путь сохранения");
                return;
            }

            if (_tableProfile.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var record1Projects = tbl.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "рядовое", RecordSearch.ConditionType.Equals));
                var record2Projects = tbl.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "контрольное", RecordSearch.ConditionType.Equals));
                var picketsCommon = FileSerializer.DeserializeArray<Picket>(record1Projects);
                var picketsCheck = FileSerializer.DeserializeArray<Picket>(record2Projects);
                var picketsCommonUnion = picketsCommon.IntersectBy(picketsCheck.Select(p => p.Result.Time), p => p.Result.Time).ToArray();
                var picketsCheckUnion = picketsCheck.IntersectBy(picketsCommon.Select(p => p.Result.Time), p => p.Result.Time).ToArray();

                int count = picketsCommonUnion.Count();
                if (count != picketsCheckUnion.Count())
                {
                    count = count < picketsCheckUnion.Count() ? count : picketsCheckUnion.Count();
                }
                if (count == 0)
                {
                    ShowDialog.Error("Нет точек для создания отчета.\nДобавьте контр.точки");
                    return;
                }

                var builder = new StringBuilder($"Отчет разницы рядовых и контрольных точек\nПрофиль - {tbl.Name}\n");
                for(int index = 0; index < count; index++)
                {
                    var picketCommon = picketsCommonUnion[index];
                    var picketCheck = picketsCheckUnion[index];

                    float munis = picketCommon.Result.Value - picketCheck.Result.Value;
                    float plus = picketCommon.Result.Value + picketCheck.Result.Value;
                    float result = MathF.Pow((munis / plus) / 2, 2);
                    builder.AppendLine(string.Format("Время: {0} Разница: {1}", picketCommon.Result.Time, result));
                }
                Write(path, builder.ToString());
                ShowDialog.Info("Файл успешно сохранён!");
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        private void CreateDiscrepancies2(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            var path = SelectPathDiscrepancies();
            if (string.IsNullOrEmpty(path))
            {
                ShowDialog.Warning("Вы не выбрали путь сохранения");
                return;
            }

            if (_tableProfile.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var record1Projects = tbl.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "рядовое", RecordSearch.ConditionType.Equals));
                var record2Projects = tbl.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "опытное", RecordSearch.ConditionType.Equals));
                var picketsCommon = FileSerializer.DeserializeArray<Picket>(record1Projects);
                var picketsCheck = FileSerializer.DeserializeArray<Picket>(record2Projects);
                var picketsCommonUnion = picketsCommon.IntersectBy(picketsCheck.Select(p => p.Result.Time), p => p.Result.Time).ToArray();
                var picketsCheckUnion = picketsCheck.IntersectBy(picketsCommon.Select(p => p.Result.Time), p => p.Result.Time).ToArray();

                int count = picketsCommonUnion.Count();
                if (count != picketsCheckUnion.Count())
                {
                    count = count < picketsCheckUnion.Count() ? count : picketsCheckUnion.Count();
                }
                if (count == 0)
                {
                    ShowDialog.Error("Нет точек для создания отчета.\nДобавьте контр.точки");
                    return;
                }

                var builder = new StringBuilder($"Отчет разницы рядовых и контрольных точек\nПрофиль - {tbl.Name}\n");
                for (int index = 0; index < count; index++)
                {
                    var picketCommon = picketsCommonUnion[index];
                    var picketCheck = picketsCheckUnion[index];

                    float munis = picketCommon.Result.Value - picketCheck.Result.Value;
                    float plus = picketCommon.Result.Value + picketCheck.Result.Value;
                    float result = MathF.Pow((munis / plus) / 2, 2);
                    builder.AppendLine(string.Format("Время: {0} Разница: {1}", picketCommon.Result.Time, result));
                }
                Write(path, builder.ToString());
                ShowDialog.Info("Файл успешно сохранён!");
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        private void OpenSelectItem(object args)
        {
            string name = (string)args;
            if (_tableProfile.TryGetTable(name, out Table? tbl))
            {
                var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
                if (rootVM == null)
                    throw new ArgumentNullException(nameof(rootVM));
                WindowManager.AddHistory(this);
                rootVM.ChangeVM(new ViewModelPicket(tbl));
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }

        private string SelectPathDiscrepancies()
        {
            var dialog = new FileDiscrepanciesDialog();
            if (dialog.SaveFileDialog())
                return dialog.FilePath;
            return string.Empty;
        }
        private void Write(string pathIn,string valueIn)
        {
            FileStream? file = null;
            try
            {
                file = new FileStream(pathIn, FileMode.Create, FileAccess.Write);
                byte[] buffer = Encoding.Default.GetBytes(valueIn);

                file.Write(buffer, 0, buffer.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                file?.Close();
            }
        }

        private void ReturnWindow(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            IViewModel? vm;
            if (!WindowManager.PopHistory(out vm))
                throw new ArgumentNullException(nameof(vm));
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeVM(vm);
        }

        public ObservableCollection<Profile> GetData()
        {
            var recordProjects = _tableProfile.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Profile>(recordProjects);
            return new ObservableCollection<Profile>(projects);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

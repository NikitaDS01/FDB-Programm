using FileDB.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core;
using WindowDatabase.Core.Data.Entity;
using FileDB.Core.Data.Tables;
using WindowDatabase.Windows.OtherWindow;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.Core.Dialog;
using FileDB.Core.Data;
using WindowDatabase.ViewModel.RootVM;
using WindowDatabase.Windows;
using WindowDatabase.Core.Data.TableValue;
using System.Linq;
using System.Text;
using System.IO;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelSquare : INotifyPropertyChanged, IViewModel, IEntityTable<Square>
    {
        private Table _tableSquare;
        private Square _item;

        public ViewModelSquare(Table tableSquareIn)
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
            _tableSquare = tableSquareIn;
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
        public Square SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Square> Items
        { get { return GetData(); } }


        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreateSquareWindow(),
                                     new ViewModelCreateSquare(_tableSquare));
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreateSquareWindow(),
                                     new ViewModelCreateSquare(_tableSquare, SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            if (_tableSquare.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                tbl.DirectoryTable.Delete(true);
                _tableSquare.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
                _tableSquare.RemoveChildTable(SelectedItem.Name);
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
            if (_tableSquare.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var vm = new ViewModelGraphic();
                foreach (var table in tbl.ChildTables)
                {
                    var recordProjects = table.Select(new FileDB.Core.Data.RecordSearch(0));
                    var picket = FileSerializer.DeserializeArray<Picket>(recordProjects);
                    var list = new Result[picket.Length];
                    for (int index = 0; index < picket.Length; index++)
                    {
                        list[index] = picket[index].Result;
                    }
                    vm.AddPoints(list, table.Name);
                }
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
            if (_tableSquare.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var vm = new ViewModelGraphic();
                foreach (var table in tbl.ChildTables)
                {
                    var recordProjects = table.Select(new FileDB.Core.Data.RecordSearch(0));
                    var picket = FileSerializer.DeserializeArray<Picket>(recordProjects);
                    var list = new Result[picket.Length];
                    for (int index = 0; index < picket.Length; index++)
                    {
                        list[index] = picket[index].Transformant_1;
                    }
                    vm.AddPoints(list, table.Name);
                }
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
            if (_tableSquare.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var vm = new ViewModelGraphic();
                foreach (var table in tbl.ChildTables)
                {
                    var record1Projects = table.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "рядовое", RecordSearch.ConditionType.Equals));
                    var record2Projects = table.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "контрольное", RecordSearch.ConditionType.Equals));

                    var picket1 = FileSerializer.DeserializeArray<Picket>(record1Projects);
                    var picket2 = FileSerializer.DeserializeArray<Picket>(record2Projects);
                    var list1 = new Result[picket1.Length];
                    var list2 = new Result[picket2.Length];
                    for (int index = 0; index < picket1.Length; index++)
                        list1[index] = picket1[index].Result;
                    for (int index = 0; index < picket2.Length; index++)
                        list2[index] = picket2[index].Result;

                    vm.AddPoints(list1, table.Name + " - Рядовые");
                    vm.AddPoints(list2, table.Name + " - Контрольные");
                }
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
            if (_tableSquare.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var vm = new ViewModelGraphic();
                foreach (var table in tbl.ChildTables)
                {
                    var record1Projects = table.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "рядовое", RecordSearch.ConditionType.Equals));
                    var record2Projects = table.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "опытное", RecordSearch.ConditionType.Equals));

                    var picket1 = FileSerializer.DeserializeArray<Picket>(record1Projects);
                    var picket2 = FileSerializer.DeserializeArray<Picket>(record2Projects);
                    var list1 = new Result[picket1.Length];
                    var list2 = new Result[picket2.Length];
                    for (int index = 0; index < picket1.Length; index++)
                        list1[index] = picket1[index].Result;
                    for (int index = 0; index < picket2.Length; index++)
                        list2[index] = picket2[index].Result;

                    vm.AddPoints(list1, table.Name + " - Рядовые");
                    vm.AddPoints(list2, table.Name + " - Опытные");
                }
                WindowManager.OpenDialog(new GraphicWindow(), vm);
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        private void CreateDiscrepancies1(object args)
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

            if (_tableSquare.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var builder = new StringBuilder($"Отчет разницы рядовых и контрольных точек\nПлощадь - {tbl.Name}\n");

                foreach (var table in tbl.ChildTables)
                {
                    builder.AppendLine($"\nПрофиль - {table.Name}");

                    var record1Projects = table.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "рядовое", RecordSearch.ConditionType.Equals));
                    var record2Projects = table.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "контрольное", RecordSearch.ConditionType.Equals));
                    var picketsCommon = FileSerializer.DeserializeArray<Picket>(record1Projects);
                    var picketsSkilled = FileSerializer.DeserializeArray<Picket>(record2Projects);

                    var picketsCommonUnion = picketsCommon.IntersectBy(picketsSkilled.Select(p => p.Result.Time), p => p.Result.Time).ToArray();
                    var picketsSkilledUnion = picketsSkilled.IntersectBy(picketsCommon.Select(p => p.Result.Time), p => p.Result.Time).ToArray();
                    
                    int count = picketsCommonUnion.Count();
                    if (count != picketsSkilledUnion.Count())
                    {
                        count = count < picketsSkilledUnion.Count() ? count : picketsSkilledUnion.Count();
                    }
                    if (count == 0)
                    {
                        builder.AppendLine("Нет точек для создания отчета");
                        continue;
                    }

                    for (int index = 0; index < count; index++)
                    {
                        var picketCommon = picketsCommonUnion[index];
                        var picketCheck = picketsSkilledUnion[index];

                        float munis = picketCommon.Result.Value - picketCheck.Result.Value;
                        float plus = picketCommon.Result.Value + picketCheck.Result.Value;
                        float result = MathF.Pow((munis / plus) / 2, 2);
                        builder.AppendLine(string.Format("Время: {0} Разница: {1}", picketCommon.Result.Time, result));
                    }
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

            if (_tableSquare.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                var builder = new StringBuilder($"Отчет разницы рядовых и контрольных точек\nПлощадь - {tbl.Name}\n");

                foreach (var table in tbl.ChildTables)
                {
                    builder.AppendLine($"\nПрофиль - {table.Name}");

                    var record1Projects = table.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "рядовое", RecordSearch.ConditionType.Equals));
                    var record2Projects = table.Select(new FileDB.Core.Data.RecordSearch(1).Add("Type", "опытное", RecordSearch.ConditionType.Equals));
                    var picketsCommon = FileSerializer.DeserializeArray<Picket>(record1Projects);
                    var picketsSkilled = FileSerializer.DeserializeArray<Picket>(record2Projects);

                    var picketsCommonUnion = picketsCommon.IntersectBy(picketsSkilled.Select(p => p.Result.Time), p => p.Result.Time).ToArray();
                    var picketsSkilledUnion = picketsSkilled.IntersectBy(picketsCommon.Select(p => p.Result.Time), p => p.Result.Time).ToArray();

                    int count = picketsCommonUnion.Count();
                    if (count != picketsSkilledUnion.Count())
                    {
                        count = count < picketsSkilledUnion.Count() ? count : picketsSkilledUnion.Count();
                    }
                    if (count == 0)
                    {
                        builder.AppendLine("Нет точек для создания отчета");
                        continue;
                    }

                    for (int index = 0; index < count; index++)
                    {
                        var picketCommon = picketsCommonUnion[index];
                        var picketCheck = picketsSkilledUnion[index];

                        float munis = picketCommon.Result.Value - picketCheck.Result.Value;
                        float plus = picketCommon.Result.Value + picketCheck.Result.Value;
                        float result = MathF.Pow((munis / plus) / 2, 2);
                        builder.AppendLine(string.Format("Время: {0} Разница: {1}", picketCommon.Result.Time, result));
                    }
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
            if (_tableSquare.TryGetTable(name, out Table? tbl))
            {
                var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
                if (rootVM == null)
                    throw new ArgumentNullException(nameof(rootVM));
                WindowManager.AddHistory(this);
                rootVM.ChangeVM(new ViewModelProfile(tbl));
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
        private void Write(string pathIn, string valueIn)
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

        public ObservableCollection<Square> GetData()
        {
            var recordProjects = _tableSquare.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Square>(recordProjects);
            return new ObservableCollection<Square>(projects);
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


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

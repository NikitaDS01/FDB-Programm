using ConsoleTest.Data;
using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
using FileDB.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.ViewModel.RootVM;
using WindowDatabase.Windows;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelProject : INotifyPropertyChanged, IViewModel, IEntityTable<Project>
    {
        private Table _tableProject;
        private Project _item;

        public ViewModelProject()
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");
            if (!Database.CurrentDatabase.ContainRootTable(Settings.TableProject))
                throw new ArgumentNullException(Settings.TableProject);

            AddCommand = new RelayCommand(AddContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            OpenCommand = new RelayCommand(OpenSelectItem);
            _tableProject = Database.CurrentDatabase.GetRootTable(Settings.TableProject);
            
        }
        public string Name => Database.CurrentDatabase.Name;
        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }

        public Project SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Project> Items
            { get { return GetData(); } }

        
        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreateProjectWindow(),
                                     new ViewModelCreateProject());
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreateProjectWindow(),
                                     new ViewModelCreateProject(SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            if (_tableProject.TryGetTable(SelectedItem.Name, out Table? tbl))
            {
                tbl.DirectoryTable.Delete(true);
                _tableProject.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
                _tableProject.RemoveChildTable(SelectedItem.Name);
                OnPropertyChanged(nameof(Items));
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }
        private void OpenSelectItem(object args)
        {
            string name = (string)args;
            if(_tableProject.TryGetTable(name, out Table? tbl))
            {
                var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
                if (rootVM == null)
                    throw new ArgumentNullException(nameof(rootVM));
                WindowManager.AddHistory(this);
                rootVM.ChangeVM(new ViewModelSquare(tbl));
            }
            else
            {
                throw new ArgumentNullException(nameof(args));
            }
        }

        public ObservableCollection<Project> GetData()
        {
            var recordProjects = _tableProject.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Project>(recordProjects);
            return new ObservableCollection<Project>(projects);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

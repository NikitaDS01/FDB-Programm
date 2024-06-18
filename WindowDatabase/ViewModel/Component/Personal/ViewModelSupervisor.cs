using ConsoleTest.Data;
using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
using FileDB.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelSupervisor : INotifyPropertyChanged, IViewModel, IEntityTable<Personal>
    {
        private Table _tableChief;
        private Personal _item;

        public ViewModelSupervisor()
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");
            if (!Database.CurrentDatabase.ContainRootTable(Settings.TableSupervisor))
                throw new ArgumentNullException(Settings.TableProject);

            AddCommand = new RelayCommand(AddContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            _tableChief = Database.CurrentDatabase.GetRootTable(Settings.TableSupervisor);
        }

        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Personal SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public string Name => Database.CurrentDatabase.Name;
        public ObservableCollection<Personal> Items
        { get { return GetData(); } }

        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreatePersonalWindow(),
                                     new ViewModelCreatePersonal(_tableChief));
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreatePersonalWindow(),
                                     new ViewModelCreatePersonal(_tableChief, SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            //var text = Database.CurrentDatabase.SelectRecord(SelectedItem.Name);
            // ShowDialog.Error(string.Format("count: {0}", text.Length));
            _tableChief.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
            OnPropertyChanged(nameof(Items));
        }
        private ObservableCollection<Personal> GetData()
        {
            var recordCutromer = _tableChief.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Personal>(recordCutromer);
            return new ObservableCollection<Personal>(projects);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

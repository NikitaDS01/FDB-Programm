using FileDB.Core.Data;
using FileDB.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Dialog;
using WindowDatabase.Core;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.Windows.OtherWindow;
using ConsoleTest.Data;
using FileDB.Core.Data.Tables;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelPersonalGroup : INotifyPropertyChanged, IViewModel, IEntityTable<PersonalGroup>
    {
        private Table _tableProject;
        private PersonalGroup _item;

        public ViewModelPersonalGroup()
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");
            if (!Database.CurrentDatabase.ContainRootTable(Settings.TableGroup))
                throw new ArgumentNullException(Settings.TableGroup);

            AddCommand = new RelayCommand(AddContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            _tableProject = Database.CurrentDatabase.GetRootTable(Settings.TableGroup);

        }
        public string Name => Database.CurrentDatabase.Name;
        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public PersonalGroup SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<PersonalGroup> Items
        { get { return GetData(); } }


        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreatePersonalGroupWindow(),
                                     new ViewModelCreatePersonalGroup());
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreatePersonalGroupWindow(),
                                     new ViewModelCreatePersonalGroup(SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            _tableProject.DeleteOne(new RecordSearch(1).Add("Index", SelectedItem.Index));
            OnPropertyChanged(nameof(Items));
        }

        public ObservableCollection<PersonalGroup> GetData()
        {
            var recordProjects = _tableProject.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<PersonalGroup>(recordProjects);
            return new ObservableCollection<PersonalGroup>(projects);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

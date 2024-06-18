using FileDB.Core.Data;
using FileDB.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Dialog;
using WindowDatabase.Core;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.Windows.OtherWindow;
using FileDB.Core.Data.Tables;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelEquipmentGroup : INotifyPropertyChanged, IViewModel, IEntityTable<EquipmentGroup>
    {
        private Table _tableProject;
        private EquipmentGroup _item;

        public ViewModelEquipmentGroup()
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");
            if (!Database.CurrentDatabase.ContainRootTable(Settings.TableEquipments))
                throw new ArgumentNullException(Settings.TableEquipments);

            AddCommand = new RelayCommand(AddContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            _tableProject = Database.CurrentDatabase.GetRootTable(Settings.TableEquipments);

        }
        public string Name => Database.CurrentDatabase.Name;
        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public EquipmentGroup SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<EquipmentGroup> Items
        { get { return GetData(); } }


        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreateEquipmentGroup(),
                                     new ViewModelCreateEquipmentGroup());
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreateEquipmentGroup(),
                                     new ViewModelCreateEquipmentGroup(SelectedItem));
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

        public ObservableCollection<EquipmentGroup> GetData()
        {
            var recordProjects = _tableProject.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<EquipmentGroup>(recordProjects);
            return new ObservableCollection<EquipmentGroup>(projects);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

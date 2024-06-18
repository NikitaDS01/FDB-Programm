using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
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
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.Component
{
    class ViewModelGeneratorEquipment : INotifyPropertyChanged, IViewModel, IEntityTable<Equipment>
    {
        private Table _tableEquipment;
        private Equipment _item;

        public ViewModelGeneratorEquipment()
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");
            if (!Database.CurrentDatabase.ContainRootTable(Settings.TableGenerator))
                throw new ArgumentNullException(Settings.TableProject);

            AddCommand = new RelayCommand(AddContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            _tableEquipment = Database.CurrentDatabase.GetRootTable(Settings.TableGenerator);
        }

        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Equipment SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreateEquipmentWindow(),
                                     new ViewModelCreateEquipment(_tableEquipment));
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreateEquipmentWindow(),
                                     new ViewModelCreateEquipment(_tableEquipment, SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            _tableEquipment.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
            OnPropertyChanged(nameof(Items));
        }

        public string Name => Database.CurrentDatabase.Name;
        public ObservableCollection<Equipment> Items
        { get { return GetData(); } }
        private ObservableCollection<Equipment> GetData()
        {
            var recordCutromer = _tableEquipment.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Equipment>(recordCutromer);
            return new ObservableCollection<Equipment>(projects);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

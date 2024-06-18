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
    public class ViewModelConctract : INotifyPropertyChanged, IViewModel, IEntityTable<Contract>
    {
        private Table _tableContract;
        private Contract _item;

        public ViewModelConctract()
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");
            if (!Database.CurrentDatabase.ContainRootTable(Settings.TableContract))
                throw new ArgumentNullException(Settings.TableProject);

            AddCommand = new RelayCommand(AddContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            _tableContract = Database.CurrentDatabase.GetRootTable(Settings.TableContract);
            
        }

        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Contract SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public string Name => Database.CurrentDatabase.Name;
        public ObservableCollection<Contract> Items
        { get { return GetData(); } }

        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreateContractWindow(),
                                     new ViewModelCreateContract());
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreateContractWindow(),
                                     new ViewModelCreateContract(SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            _tableContract.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
            OnPropertyChanged(nameof(Items));
        }
        private ObservableCollection<Contract> GetData()
        {
            var recordCutromer = _tableContract.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Contract>(recordCutromer);
            return new ObservableCollection<Contract>(projects);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

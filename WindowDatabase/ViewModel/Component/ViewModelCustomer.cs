using ConsoleTest.Data;
using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
using FileDB.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Projects;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelCustomer : INotifyPropertyChanged, IViewModel, IEntityTable<Customer>
    {
        private Table _tableCustomer;
        private Customer _item;
        
        public ViewModelCustomer()
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");
            if (!Database.CurrentDatabase.ContainRootTable(Settings.TableCustomer))
                throw new ArgumentNullException(Settings.TableProject);

            AddCommand = new RelayCommand(AddCustomer);
            ChangeCommand = new RelayCommand(ChangeCustomer);
            DeleteCommand = new RelayCommand(DeleteCustomer);
            _tableCustomer = Database.CurrentDatabase.GetRootTable(Settings.TableCustomer);
        }
        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Customer SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public string Name => Database.CurrentDatabase.Name;
        public ObservableCollection<Customer> Items
        { get { return GetData(); } }

        private ObservableCollection<Customer> GetData()
        {
            var recordCutromer = _tableCustomer.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Customer>(recordCutromer);
            return new ObservableCollection<Customer>(projects);
        }
        private void AddCustomer(object args)
        {
            WindowManager.OpenDialog(new CreateCustomerWindow(), 
                                     new ViewModelCreateCustomer());
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeCustomer(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreateCustomerWindow(), 
                                     new ViewModelCreateCustomer(SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteCustomer(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            _tableCustomer.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
            OnPropertyChanged(nameof(Items));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

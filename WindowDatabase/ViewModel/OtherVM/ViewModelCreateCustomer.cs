using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConsoleTest.Data;
using FileDB.Core.Data.Tables;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreateCustomer : INotifyPropertyChanged, IViewModel
    {
        private Customer _currentCustomer = null!;
        private Table _tableCustomer;
        private bool _isCreate;
        public ViewModelCreateCustomer()
        {
            _isCreate = true;
            _tableCustomer = Database.CurrentDatabase.GetRootTable(Settings.TableCustomer);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentCustomer = new Customer();
        }
        public ViewModelCreateCustomer(Customer customerIn)
        {
            _isCreate = false;
            _tableCustomer = Database.CurrentDatabase.GetRootTable(Settings.TableCustomer);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentCustomer = customerIn;
        }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;

        public string Name
        {
            get { return _currentCustomer.Name; }
            set
            {
                _currentCustomer.Name = value;
                OnPropertyChanged();
            }
        }
        public string JurAddress
        {
            get { return _currentCustomer.JurAddress; }
            set
            {
                _currentCustomer.JurAddress = value;
                OnPropertyChanged();
            }
        }
        public string Address
        {
            get { return _currentCustomer.Address; }
            set
            {
                _currentCustomer.Address = value;
                OnPropertyChanged();
            }
        }
        public string INN
        {
            get { return _currentCustomer.INN; }
            set
            {
                _currentCustomer.INN = value;
                OnPropertyChanged();
            }
        }
        public string KPK
        {
            get { return _currentCustomer.KPK; }
            set
            {
                _currentCustomer.KPK = value;
                OnPropertyChanged();
            }
        }
        public string PC
        {
            get { return _currentCustomer.PC; }
            set
            {
                _currentCustomer.PC = value;
                OnPropertyChanged();
            }
        }
        public string KOPP_C
        {
            get { return _currentCustomer.KOPP_C; }
            set
            {
                _currentCustomer.KOPP_C = value;
                OnPropertyChanged();
            }
        }
        public string Delegate
        {
            get { return _currentCustomer.Delegate; }
            set
            {
                _currentCustomer.Delegate = value;
                OnPropertyChanged();
            }
        }
        public string Phone
        {
            get { return _currentCustomer.Phone; }
            set
            {
                _currentCustomer.Phone = value;
                OnPropertyChanged();
            }
        }
        public string AddressSite
        {
            get { return _currentCustomer.AddressSite; }
            set
            {
                _currentCustomer.AddressSite = value;
                OnPropertyChanged();
            }
        }

        private void SaveCustomer(object args)
        {
            if(_isCreate)
                _currentCustomer.CreateRecord = DateTime.Now;

            _currentCustomer.UpdateRecord = DateTime.Now;
            _tableCustomer.WriteOne(_currentCustomer);
            WindowManager.Close<CreateCustomerWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreateCustomerWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FileDB.Core.Data.Tables;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreateEquipment : INotifyPropertyChanged, IViewModel
    {
        private Equipment _currentEquipment = null!;
        private Table _tableEquipment;
        private bool _isCreate;
        public ViewModelCreateEquipment(Table tableEquipmentIn)
        {
            _isCreate = true;
            _tableEquipment = tableEquipmentIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentEquipment = new Equipment();
        }
        public ViewModelCreateEquipment(Table tableEquipmentIn, Equipment personalIn)
        {
            _isCreate = false;
            _tableEquipment = tableEquipmentIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentEquipment = personalIn;
        }
        
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;

        public string Name
        {
            get { return _currentEquipment.Name; }
            set
            {
                _currentEquipment.Name = value;
                OnPropertyChanged();
            }
        }
        public int Index
        {
            get { return _currentEquipment.Index; }
            set
            {
                _currentEquipment.Index = value;
                OnPropertyChanged();
            }
        }
        public DateTime DatePay
        {
            get { return _currentEquipment.DatePay; }
            set
            {
                _currentEquipment.DatePay = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateCheck
        {
            get { return _currentEquipment.DateCheck; }
            set
            {
                _currentEquipment.DateCheck = value;
                OnPropertyChanged();
            }
        }
        public string Specifications
        {
            get { return _currentEquipment.Specifications; }
            set
            {
                _currentEquipment.Specifications = value;
                OnPropertyChanged();
            }
        }

        private void SaveCustomer(object args)
        {
            _tableEquipment.WriteOne(_currentEquipment);
            WindowManager.Close<CreateEquipmentWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreateEquipmentWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

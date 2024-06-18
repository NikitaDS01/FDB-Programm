using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ConsoleTest.Data;
using FileDB.Core.Data.Tables;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreateContract : INotifyPropertyChanged, IViewModel
    {
        private Contract _currentContract = null!;
        private Table _tableContract;
        private bool _isCreate;
        public ViewModelCreateContract()
        {
            _isCreate = true;
            _tableContract = Database.CurrentDatabase.GetRootTable(Settings.TableContract);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentContract = new Contract();
        }
        public ViewModelCreateContract(Contract customerIn)
        {
            _isCreate = false;
            _tableContract = Database.CurrentDatabase.GetRootTable(Settings.TableContract);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentContract = customerIn;
        }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;
        public string Name
        {
            get { return _currentContract.Name; }
            set
            {
                _currentContract.Name = value;
                OnPropertyChanged();
            }
        }
        public int Id
        {
            get { return _currentContract.Id; }
            set
            {
                _currentContract.Id = value;
                OnPropertyChanged();
            }
        }
        public DateTime Begin
        {
            get { return _currentContract.Begin; }
            set
            {
                _currentContract.Begin = value;
                OnPropertyChanged();
            }
        }
        public DateTime End
        {
            get { return _currentContract.End; }
            set
            {
                _currentContract.End = value;
                OnPropertyChanged();
            }
        }
        public float Cost
        {
            get { return _currentContract.Cost; }
            set
            {
                _currentContract.Cost = value;
                OnPropertyChanged();
            }
        }

        private void SaveCustomer(object args)
        {
            if (_isCreate)
                _currentContract.CreateRecord = DateTime.Now;

            _currentContract.UpdateRecord = DateTime.Now;
            _tableContract.WriteOne(_currentContract);
            WindowManager.Close<CreateContractWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreateContractWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

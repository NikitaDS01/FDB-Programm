using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ConsoleTest.Data;
using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreatePersonal : INotifyPropertyChanged, IViewModel
    {
        private Personal _currentPersonal = null!;
        private Table _tablePersonal;
        private bool _isCreate;
        public ViewModelCreatePersonal(Table tablePersonalIn)
        {
            _isCreate = true;
            _tablePersonal = tablePersonalIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentPersonal = new Personal();
        }
        public ViewModelCreatePersonal(Table tablePersonalIn, Personal personalIn)
        {
            _isCreate = true;
            _tablePersonal = tablePersonalIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentPersonal = personalIn;
        }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;

        public string Name
        {
            get { return _currentPersonal.Name; }
            set
            {
                _currentPersonal.Name = value;
                OnPropertyChanged();
            }
        }
        public string Skill
        {
            get { return _currentPersonal.Skill; }
            set
            {
                _currentPersonal.Skill = value;
                OnPropertyChanged();
            }
        }
        public int WorkExpCompany
        {
            get { return _currentPersonal.WorkExpCompany; }
            set
            {
                _currentPersonal.WorkExpCompany = value;
                OnPropertyChanged();
            }
        }
        public int WorkExpAll
        {
            get { return _currentPersonal.WorkExpAll; }
            set
            {
                _currentPersonal.WorkExpAll = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateMedic
        {
            get { return _currentPersonal.DateMedic; }
            set
            {
                _currentPersonal.DateMedic = value;
                OnPropertyChanged();
            }
        }

        private void SaveCustomer(object args)
        {
            _tablePersonal.WriteOne(_currentPersonal);
            WindowManager.Close<CreatePersonalWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreatePersonalWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

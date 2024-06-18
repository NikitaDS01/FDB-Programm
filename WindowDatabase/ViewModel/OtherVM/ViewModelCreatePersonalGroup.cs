using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Core;
using WindowDatabase.Windows.OtherWindow;
using FileDB.Core.Data.Tables;
using ConsoleTest.Data;
using System.IO;
using WindowDatabase.Core.Dialog;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreatePersonalGroup : INotifyPropertyChanged, IViewModel
    {
        private PersonalGroup _currentMethod = null!;
        private Table _tableMethod;
        private bool _isCreate;
        private string _linkChief;
        private string _linkWorker;
        private string _linkEngineer;
        private string _linkDriver;

        public ViewModelCreatePersonalGroup()
        {
            _isCreate = true;
            _linkChief = string.Empty;
            _linkWorker = string.Empty;
            _linkEngineer = string.Empty;
            _linkDriver = string.Empty;
            _tableMethod = Database.CurrentDatabase.GetRootTable(Settings.TableGroup);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentMethod = new PersonalGroup();
        }
        public ViewModelCreatePersonalGroup(PersonalGroup methodIn)
        {
            _isCreate = false;
            _tableMethod = Database.CurrentDatabase.GetRootTable(Settings.TableGroup);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentMethod = methodIn;
            _linkChief = methodIn.Chief.FullName;
            _linkWorker = methodIn.Worker.FullName;
            _linkEngineer = methodIn.Engineer.FullName;
            _linkDriver = methodIn.Driver.FullName;
        }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;

        public int Index
        {
            get { return _currentMethod.Index; }
            set
            {
                _currentMethod.Index = value;
                OnPropertyChanged();
            }
        }
        public string LinkChief
        {
            get { return _linkChief; }
            set
            {
                _linkChief = value;
                OnPropertyChanged();
            }
        }
        public string LinkDriver
        {
            get { return _linkDriver; }
            set
            {
                _linkDriver = value;
                OnPropertyChanged();
            }
        }
        public string LinkWorker
        {
            get { return _linkWorker; }
            set
            {
                _linkWorker = value;
                OnPropertyChanged();
            }
        }
        public string LinkEngineer
        {
            get { return _linkEngineer; }
            set
            {
                _linkEngineer = value;
                OnPropertyChanged();
            }
        }

        private void SaveCustomer(object args)
        {
            if (string.IsNullOrEmpty(LinkChief))
            {
                ShowDialog.Error("Ссылка не указывает на начальника отряда!");
                return;
            }
            if (string.IsNullOrEmpty(LinkDriver))
            {
                ShowDialog.Error("Ссылка не указывает на водителя!");
                return;
            }
            if (string.IsNullOrEmpty(LinkEngineer))
            {
                ShowDialog.Error("Ссылка не указывает на инженера!");
                return;
            }
            if (string.IsNullOrEmpty(LinkWorker))
            {
                ShowDialog.Error("Ссылка не указывает на рабочего!");
                return;
            }
            
            _currentMethod.Chief = new FileDB.Core.Data.RecordLink(LinkChief);
            _currentMethod.Driver = new FileDB.Core.Data.RecordLink(LinkDriver);
            _currentMethod.Engineer = new FileDB.Core.Data.RecordLink(LinkEngineer);
            _currentMethod.Worker = new FileDB.Core.Data.RecordLink(LinkWorker);
            _tableMethod.WriteOne(_currentMethod);
            WindowManager.Close<CreatePersonalGroupWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreatePersonalGroupWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

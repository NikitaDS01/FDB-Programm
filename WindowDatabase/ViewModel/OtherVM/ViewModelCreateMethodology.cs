using FileDB.Core.Data.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.OtherVM
{
    class ViewModelCreateMethodology : INotifyPropertyChanged, IViewModel
    {
        private Methodology _currentMethod = null!;
        private Table _tableMethod;
        private bool _isCreate;
        public ViewModelCreateMethodology()
        {
            _isCreate = true;
            _tableMethod = Database.CurrentDatabase.GetRootTable(Settings.TableMethodology);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentMethod = new Methodology();
        }
        public ViewModelCreateMethodology(Methodology methodIn)
        {
            _isCreate = false;
            _tableMethod = Database.CurrentDatabase.GetRootTable(Settings.TableMethodology);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentMethod = methodIn;
        }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;

        public string Name
        {
            get { return _currentMethod.Name; }
            set
            {
                _currentMethod.Name = value;
                OnPropertyChanged();
            }
        }
        public string DescriptionGenerator
        {
            get { return _currentMethod.DescriptionGenerator; }
            set
            {
                _currentMethod.DescriptionGenerator = value;
                OnPropertyChanged();
            }
        }
        public string DescriptionMeasuring
        {
            get { return _currentMethod.DescriptionMeasuring; }
            set
            {
                _currentMethod.DescriptionMeasuring = value;
                OnPropertyChanged();
            }
        }
        public string DescriptionTelemetry
        {
            get { return _currentMethod.DescriptionTelemetry; }
            set
            {
                _currentMethod.DescriptionTelemetry = value;
                OnPropertyChanged();
            }
        }
        public string DescriptionModes
        {
            get { return _currentMethod.DescriptionModes; }
            set
            {
                _currentMethod.DescriptionModes = value;
                OnPropertyChanged();
            }
        }

        private void SaveCustomer(object args)
        {
            _tableMethod.WriteOne(_currentMethod);
            WindowManager.Close<CreateMethodologyWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreateMethodologyWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

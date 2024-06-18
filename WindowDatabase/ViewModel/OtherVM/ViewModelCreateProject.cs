using ConsoleTest.Data;
using FileDB.Core.Data.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Dialog;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreateProject : INotifyPropertyChanged, IViewModel
    {
        private Project _currentProject = null!;
        private Table _tableProject;
        private bool _isCreate;
        private string _linkContract;
        private string _linkCustomer;
        public ViewModelCreateProject()
        {
            _isCreate = true;
            _linkCustomer = string.Empty;
            _linkContract = string.Empty;
            _tableProject = Database.CurrentDatabase.GetRootTable(Settings.TableProject);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentProject = new Project();
        }
        public ViewModelCreateProject(Project projectIn)
        {
            _isCreate = true;
            _linkCustomer = projectIn.Customer.FullName;
            _linkContract = projectIn.Contract.FullName;
            _tableProject = Database.CurrentDatabase.GetRootTable(Settings.TableProject);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentProject = projectIn;
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;

        public string Name
        {
            get { return _currentProject.Name; }
            set
            {
                _currentProject.Name = value;
                OnPropertyChanged();
            }
        }
        public string LinkCustomer
        {
            get { return _linkCustomer; }
            set
            {
                _linkCustomer = value;
                OnPropertyChanged();
            }
        }
        public string LinkContract
        {
            get { return _linkContract; }
            set
            {
                _linkContract = value;
                OnPropertyChanged();
            }
        }
        private void SaveCustomer(object args)
        {
            if(string.IsNullOrEmpty(LinkCustomer))
            {
                ShowDialog.Error("Ссылка не указывает на заказчика!");
                return;
            }
            if (string.IsNullOrEmpty(LinkContract))
            {
                ShowDialog.Error("Ссылка не указывает на договор!");
                return;
            }

            if (_isCreate)
                _currentProject.CreateRecord = DateTime.Now;

            _currentProject.UpdateRecord = DateTime.Now;
            _currentProject.Customer = new FileDB.Core.Data.RecordLink(LinkCustomer);
            _currentProject.Contract = new FileDB.Core.Data.RecordLink(LinkContract);
            _tableProject.WriteOne(_currentProject);
            if(_isCreate)
                _tableProject.CreateChildRecord(_currentProject);
            WindowManager.Close<CreateProjectWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreateProjectWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

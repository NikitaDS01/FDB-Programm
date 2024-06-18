using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Projects;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.RootVM;

namespace WindowDatabase.ViewModel
{
    public class ViewModelEntranceDatabase : INotifyPropertyChanged, IViewModel
    {
        private PathDatabase _selectedPath;
        private ListPathDatabase _paths;
        private ICommand _openDBCommand;
        private ICommand _addCommand;
        private ICommand _deleteCommand;
        private ICommand _createCommand;
        private IOpenDialogService _dialog;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ViewModelEntranceDatabase(ListPathDatabase listPathsIn)
        {
            _paths = listPathsIn;
            _dialog = new FileDatabaseDialog();
            _addCommand = new RelayCommand(AddNewPath);
            _openDBCommand = new RelayCommand(OpenDatabase);
            _deleteCommand = new RelayCommand(DeletePath);
            _createCommand = new RelayCommand(CreateNewDatabase);
        }

        #region Property
        public ObservableCollection<PathDatabase> ListPaths
        {
            get { return new ObservableCollection<PathDatabase>(_paths.Databases); }
        }

        public PathDatabase SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                _selectedPath = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddCommand => _addCommand;
        public ICommand OpenDBCommand => _openDBCommand;
        public ICommand DeleteCommand => _deleteCommand;
        public ICommand CreateCommand => _createCommand;
        
        #endregion
        
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenDatabase(object args)
        {
            if (SelectedPath == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            Database.Open(SelectedPath.Database);
            WindowManager.Close<EntranceWindow>();
        }
        private void CreateNewDatabase(object args)
        {
            var vm = WindowManager.GetViewModel<EntranceWindow>() as EntranceRootViewModel;
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));
            vm.ChangeCreateDBWindow();
        }
        private void AddNewPath(object args)
        {
            try
            {
                if(_dialog.OpenFileDialog())
                {
                    var path = new PathDatabase(System.IO.Path.GetDirectoryName(_dialog.FilePath)!);
                    Database.Init(path.Database);
                    _paths.Add(path);
                    OnPropertyChanged(nameof(ListPaths));
                }
            }
            catch (Exception ex) 
            {
                ShowDialog.Error(ex.Message);
            }
        }
        private void DeletePath(object args)
        {
            if (_selectedPath != null)
            {
                _paths.Remove(_selectedPath);
                OnPropertyChanged(nameof(ListPaths));
            }
            else
            {
                ShowDialog.Warning("Не выбран элемент");
            }
        }
        public void AddItem(PathDatabase pathIn)
        {
            _paths.Add(pathIn);
            OnPropertyChanged(nameof(ListPaths));
        }
    }
}

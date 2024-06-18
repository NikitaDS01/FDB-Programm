using FileDB;
using FileDB.Core.Data.Tables;
using FileDB.Core.File;
using FileDB.Function;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Projects;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.RootVM;

namespace WindowDatabase.ViewModel
{
    class ViewModelCreateDatabase : INotifyPropertyChanged, IViewModel
    {
        private string _name = "Новая база данных";
        private string _path = string.Empty;
        private ICommand _setPathCommand;
        private ICommand _cancelCommand;
        private ICommand _acceptCommand;
        private IOpenDialogService _dialog;

        public ViewModelCreateDatabase()
        {
            _dialog = new OpenDirectoryDialog();
            _setPathCommand = new RelayCommand(GetPathDatabase);
            _cancelCommand = new RelayCommand(CancelWindow);
            _acceptCommand = new RelayCommand(AcceptDatabase);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                OnPropertyChanged();
            }
        }
        public ICommand SetPathCommand => _setPathCommand;
        public ICommand CancelCommand => _cancelCommand;
        public ICommand AcceptCommand => _acceptCommand;

        public PathDatabase GetDatabase()
        {
            string path = FunctionFile.FullFileName(Path,
                Name, TypeFormat.FDB);
            return new PathDatabase(System.IO.Path.GetDirectoryName(path)!);
        }
        public void AcceptDatabase(object args)
        {
            if (string.IsNullOrEmpty(Name))
            {
                ShowDialog.Warning("Укажите название базы данных");
                return;
            }
            if (string.IsNullOrEmpty(Path))
            {
                ShowDialog.Warning("Укажите путь");
                return;
            }
            
            var vm = WindowManager.GetViewModel<EntranceWindow>() as EntranceRootViewModel;
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));
            vm.ChangeEntranceWindow();
            var fdb = this.GetDatabase();
            Database.Create(fdb.Database, Name);
            vm.AddPath(fdb);
            this.CreateTables(fdb.Database);
        }
        public void CancelWindow(object args)
        {
            var vm = WindowManager.GetViewModel<EntranceWindow>() as EntranceRootViewModel;
            if (vm == null)
                throw new ArgumentNullException(nameof(vm));
            vm.ChangeEntranceWindow();
        }
        public void GetPathDatabase(object args)
        {
            try
            {
                if (_dialog.OpenFileDialog())
                {
                    Path = _dialog.FilePath;
                }
            }
            catch (Exception ex)
            {
                ShowDialog.Error(ex.Message);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void CreateTables(FileDatabase databaseIn)
        {
            databaseIn.CreateRootTable(Settings.TableCustomer);
            databaseIn.CreateRootTable(Settings.TableContract);
            databaseIn.CreateRootTable(new PropertyTable { NameTable= Settings.TableProject, IsNameTable=false});

            databaseIn.CreateRootTable(Settings.TableGroup);
            databaseIn.CreateRootTable(Settings.TableChief);
            databaseIn.CreateRootTable(Settings.TableDriver);
            databaseIn.CreateRootTable(Settings.TableEngineer);
            databaseIn.CreateRootTable(Settings.TableWorker);
            databaseIn.CreateRootTable(Settings.TableSupervisor);

            databaseIn.CreateRootTable(Settings.TableGenerator);
            databaseIn.CreateRootTable(Settings.TableTelemetry);
            databaseIn.CreateRootTable(Settings.TableMeasuring);
            databaseIn.CreateRootTable(Settings.TableEquipments);
            databaseIn.CreateRootTable(Settings.TableMethodology);
        }
    }
}

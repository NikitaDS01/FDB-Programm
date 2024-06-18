using FileDB.Core.Data;
using FileDB.Serialization;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.Windows.OtherWindow;
using FileDB.Core.Data.Tables;
using WindowDatabase.ViewModel.RootVM;
using WindowDatabase.Windows;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelPicket : INotifyPropertyChanged, IViewModel, IEntityTable<Picket>
    {
        private Table _tablePicket;
        private Picket _item;

        public ViewModelPicket(Table tablePicketIn)
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");

            AddCommand = new RelayCommand(AddContract);
            ExportCommand = new RelayCommand(ExportContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            ReturnCommand = new RelayCommand(ReturnWindow);

            _tablePicket = tablePicketIn;
        }
        public string Name => Database.CurrentDatabase.Name;
        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand ReturnCommand { get; private set; }

        public Picket SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Picket> Items
        { get { return GetData(); } }


        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreatePicketWindow(),
                                     new ViewModelCreatePicket(_tablePicket));
            OnPropertyChanged(nameof(Items));
        }
        private void ExportContract(object args)
        {
            WindowManager.OpenDialog(new CreateArrayPicketWindow(),
                                     new ViewModelCreateArrayPicket(_tablePicket));
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreatePicketWindow(),
                                     new ViewModelCreatePicket(_tablePicket, SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            _tablePicket.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
            OnPropertyChanged(nameof(Items));            
        }
        private void ReturnWindow(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            IViewModel? vm;
            if(!WindowManager.PopHistory(out vm))
                throw new ArgumentNullException(nameof(vm));
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeVM(vm);
        }
        public ObservableCollection<Picket> GetData()
        {
            var recordProjects = _tablePicket.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Picket>(recordProjects);
            return new ObservableCollection<Picket>(projects);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

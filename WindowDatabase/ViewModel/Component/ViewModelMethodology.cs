using FileDB.Core.Data;
using FileDB.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel.OtherVM;
using WindowDatabase.Windows.OtherWindow;
using FileDB.Core.Data.Tables;

namespace WindowDatabase.ViewModel.Component
{
    public class ViewModelMethodology : INotifyPropertyChanged, IViewModel, IEntityTable<Methodology>
    {
        private Table _tableContract;
        private Methodology _item;

        public ViewModelMethodology()
        {
            if (!Database.IsInit)
                throw new Exception("База данных не была загружена");
            if (!Database.CurrentDatabase.ContainRootTable(Settings.TableMethodology))
                throw new ArgumentNullException(Settings.TableMethodology);

            AddCommand = new RelayCommand(AddContract);
            ChangeCommand = new RelayCommand(ChangeContract);
            DeleteCommand = new RelayCommand(DeleteContract);
            _tableContract = Database.CurrentDatabase.GetRootTable(Settings.TableMethodology);

        }

        public ICommand AddCommand { get; private set; }
        public ICommand ChangeCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Methodology SelectedItem
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }
        public string Name => Database.CurrentDatabase.Name;
        public ObservableCollection<Methodology> Items
        { get { return GetData(); } }

        private void AddContract(object args)
        {
            WindowManager.OpenDialog(new CreateMethodologyWindow(),
                                     new ViewModelCreateMethodology());
            OnPropertyChanged(nameof(Items));
        }
        private void ChangeContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }

            WindowManager.OpenDialog(new CreateMethodologyWindow(),
                                     new ViewModelCreateMethodology(SelectedItem));
            OnPropertyChanged(nameof(Items));
        }
        private void DeleteContract(object args)
        {
            if (SelectedItem == null)
            {
                ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            _tableContract.DeleteOne(new RecordSearch(1).Add("Name", SelectedItem.Name));
            OnPropertyChanged(nameof(Items));
        }
        private ObservableCollection<Methodology> GetData()
        {
            var recordCutromer = _tableContract.Select(new FileDB.Core.Data.RecordSearch(0));
            var projects = FileSerializer.DeserializeArray<Methodology>(recordCutromer);
            return new ObservableCollection<Methodology>(projects);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

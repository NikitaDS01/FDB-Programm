using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Dialog;
using WindowDatabase.Core;
using WindowDatabase.Windows.OtherWindow;
using FileDB.Core.Data.Tables;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreateEquipmentGroup : INotifyPropertyChanged, IViewModel
    {
        private EquipmentGroup _currentMethod = null!;
        private Table _tableMethod;
        private bool _isCreate;
        private string _equipmentGEN;
        private string _equipmentIN;
        private string _equipmentTEL;

        public ViewModelCreateEquipmentGroup()
        {
            _isCreate = true;
            _equipmentGEN = string.Empty;
            _equipmentIN = string.Empty;
            _equipmentTEL = string.Empty;
            _tableMethod = Database.CurrentDatabase.GetRootTable(Settings.TableEquipments);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentMethod = new EquipmentGroup();
        }
        public ViewModelCreateEquipmentGroup(EquipmentGroup methodIn)
        {
            _isCreate = false;
            _tableMethod = Database.CurrentDatabase.GetRootTable(Settings.TableEquipments);
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            _currentMethod = methodIn;
            _equipmentGEN = methodIn.EquipmentGEN.FullName;
            _equipmentIN = methodIn.EquipmentIN.FullName;
            _equipmentTEL = methodIn.EquipmentTEL.FullName;
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
        public string LinkEquipmentGEN
        {
            get { return _equipmentGEN; }
            set
            {
                _equipmentGEN = value;
                OnPropertyChanged();
            }
        }
        public string LinkEquipmentIN
        {
            get { return _equipmentIN; }
            set
            {
                _equipmentIN = value;
                OnPropertyChanged();
            }
        }
        public string LinkEquipmentTEL
        {
            get { return _equipmentTEL; }
            set
            {
                _equipmentTEL = value;
                OnPropertyChanged();
            }
        }

        private void SaveCustomer(object args)
        {
            if (string.IsNullOrEmpty(LinkEquipmentGEN))
            {
                ShowDialog.Error("Ссылка не указывает на ген.оборудование!");
                return;
            }
            if (string.IsNullOrEmpty(LinkEquipmentIN))
            {
                ShowDialog.Error("Ссылка не указывает на изм.оборудование!");
                return;
            }
            if (string.IsNullOrEmpty(LinkEquipmentTEL))
            {
                ShowDialog.Error("Ссылка не указывает на тел.оборудование!");
                return;
            }

            _currentMethod.EquipmentGEN = new FileDB.Core.Data.RecordLink(LinkEquipmentGEN);
            _currentMethod.EquipmentIN = new FileDB.Core.Data.RecordLink(LinkEquipmentIN);
            _currentMethod.EquipmentTEL = new FileDB.Core.Data.RecordLink(LinkEquipmentTEL);
            _tableMethod.WriteOne(_currentMethod);
            WindowManager.Close<CreateEquipmentGroup>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreateEquipmentGroup>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

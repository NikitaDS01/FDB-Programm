using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Core.Data.TableValue;
using WindowDatabase.Core.Dialog;
using WindowDatabase.Core;
using WindowDatabase.Windows.OtherWindow;
using FileDB.Core.Data.Tables;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreatePicket : INotifyPropertyChanged, IViewModel
    {
        private Picket _currentPicket = null!;
        private Table _tablePicket;
        private bool _isCreate;
        private string _linkM;
        private string _linkPG;
        private string _linkEG;
        public ViewModelCreatePicket(Table tableIn)
        {
            _tablePicket = tableIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            TransformCommand = new RelayCommand(СomputingTransform);
            _linkM = string.Empty;
            _linkPG = string.Empty;
            _linkEG = string.Empty;
            _isCreate = true;

            _currentPicket = new Picket();
        }
        public ViewModelCreatePicket(Table tableIn, Picket squareIn)
        {
            _tablePicket = tableIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            TransformCommand = new RelayCommand(СomputingTransform);
            _linkM = squareIn.Method.FullName;
            _linkPG = squareIn.PersonalGroup.FullName;
            _linkEG = squareIn.Equipments.FullName;
            _isCreate = false;

            _currentPicket = squareIn;
        }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand TransformCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;

         #region Property
        public string Name
        {
            get { return _currentPicket.Name; }
            set
            {
                _currentPicket.Name = value;
                OnPropertyChanged();
            }
        }
        public Point2D Coordinate
        {
            get { return _currentPicket.Coordinate; }
            set
            {
                _currentPicket.Coordinate = value;
                OnPropertyChanged();
            }
        }
        public string Type
        {
            get { return _currentPicket.Type; }
            set
            {
                _currentPicket.Type = value;
                OnPropertyChanged();
            }
        }
        public string LinkM
        {
            get { return _linkM; }
            set
            {
                _linkM = value;
                OnPropertyChanged();
            }
        }
        public string LinkPG
        {
            get { return _linkPG; }
            set
            {
                _linkPG = value;
                OnPropertyChanged();
            }
        }
        public string LinkEG
        {
            get { return _linkEG; }
            set
            {
                _linkEG = value;
                OnPropertyChanged();
            }
        }
        public Result Result
        {
            get { return _currentPicket.Result; }
            set
            {
                _currentPicket.Result = value;
                OnPropertyChanged();
            }
        }
        public Result IntermediateResult
        {
            get { return _currentPicket.IntermediateResult; }
            set
            {
                _currentPicket.IntermediateResult = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndResult
        {
            get { return _currentPicket.EndResult; }
            set
            {
                _currentPicket.EndResult = value;
                OnPropertyChanged();
            }
        }
        public Result Transformant_1
        {
            get { return _currentPicket.Transformant_1; }
            set
            {
                _currentPicket.Transformant_1 = value;
                OnPropertyChanged();
            }
        }
        public Result Transformant_2
        {
            get { return _currentPicket.Transformant_2; }
            set
            {
                _currentPicket.Transformant_2 = value;
                OnPropertyChanged();
            }
        }
        public Result Transformant_3
        {
            get { return _currentPicket.Transformant_3; }
            set
            {
                _currentPicket.Transformant_3 = value;
                OnPropertyChanged();
            }
        }
        public Result IntermediateTransformant_1
        {
            get { return _currentPicket.IntermediateTransformant_1; }
            set
            {
                _currentPicket.IntermediateTransformant_1 = value;
                OnPropertyChanged();
            }
        }
        public Result IntermediateTransformant_2
        {
            get { return _currentPicket.IntermediateTransformant_2; }
            set
            {
                _currentPicket.IntermediateTransformant_2 = value;
                OnPropertyChanged();
            }
        }
        public Result IntermediateTransformant_3
        {
            get { return _currentPicket.IntermediateTransformant_3; }
            set
            {
                _currentPicket.IntermediateTransformant_3 = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public void СomputingTransform(object args)
        {
            Transformant_1 = Calculation.Transformant(Result);
            IntermediateTransformant_1 = Calculation.Transformant(IntermediateResult);
        }
        private void SaveCustomer(object args)
        {
            if (string.IsNullOrEmpty(LinkM))
            {
                ShowDialog.Error("Ссылка не указывает на методы!");
                return;
            }
            if (string.IsNullOrEmpty(LinkPG))
            {
                ShowDialog.Error("Ссылка не указывает на методы!");
                return;
            }
            if (string.IsNullOrEmpty(LinkEG))
            {
                ShowDialog.Error("Ссылка не указывает на методы!");
                return;
            }
            _currentPicket.Method = new FileDB.Core.Data.RecordLink(LinkM);
            _currentPicket.PersonalGroup = new FileDB.Core.Data.RecordLink(LinkPG);
            _currentPicket.Equipments = new FileDB.Core.Data.RecordLink(LinkEG);

            _tablePicket.WriteOne(_currentPicket);
            WindowManager.Close<CreatePicketWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreatePicketWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

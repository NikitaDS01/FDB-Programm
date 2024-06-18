using System;
using System.Collections.Generic;
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
using WindowDatabase.Core.Export;
using System.Collections.ObjectModel;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreateArrayPicket : INotifyPropertyChanged, IViewModel
    {
        private Picket _currentPicket = null!;
        private List<(Result, Result, string)> _values;
        private Table _tablePicket;
        private bool _isCreate;
        private string _linkM;
        private string _linkPG;
        private string _linkEG;
        public ViewModelCreateArrayPicket(Table tableIn)
        {
            _tablePicket = tableIn;
            _values = new List<(Result, Result, string)>();
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            ExportCommand = new RelayCommand(ExportData);
            _linkM = string.Empty;
            _linkPG = string.Empty;
            _linkEG = string.Empty;
            _isCreate = true;

            _currentPicket = new Picket();
            Name = tableIn.Name;
        }
        public ViewModelCreateArrayPicket(Table tableIn, Picket squareIn)
        {
            _tablePicket = tableIn;
            _values = new List<(Result, Result, string)>();
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            ExportCommand = new RelayCommand(ExportData);
            _linkM = squareIn.Method.FullName;
            _linkPG = squareIn.PersonalGroup.FullName;
            _linkEG = squareIn.Equipments.FullName;
            _isCreate = false;

            _currentPicket = squareIn;
            Name = tableIn.Name;
        }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ObservableCollection<Result> Results => GetResults();
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

        private void ExportData(object args)
        {
            var dialog = new FileExportDialog();
            if(dialog.OpenFileDialog() == true)
            {
                IExportResult export;
                if (dialog.Extension == ".txt")
                {
                    export = new TxtExportResult();
                    _values = export.Run(dialog.FilePath);
                }
                if (dialog.Extension == ".xlsx")
                {
                    export = new ExcelExportResult();
                    _values = export.Run(dialog.FilePath);
                }
                OnPropertyChanged(nameof(Results));
                ShowDialog.Info(string.Format("Прочитано {0} записей", _values.Count));
            }
            else
            {
                ShowDialog.Error("Не выбран файл!");
            }
        }
        private void SaveCustomer(object args)
        {
            if(_values.Count == 0)
            {
                ShowDialog.Error("Экспортируйте файл!");
                return;
            }
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

            var count = _tablePicket.CountRecord;
            var array = new Picket[_values.Count];
            for(int index = 0;  index < array.Length; index++)
            {
                array[index] = this.GetCopy(count + index + 1,_values[index]);
                array[index].Transformant_1 = Calculation.Transformant(array[index].Result);
                array[index].IntermediateTransformant_1 = Calculation.Transformant(array[index].IntermediateResult);
            }
            _tablePicket.WriteArray(array);
            WindowManager.Close<CreateArrayPicketWindow>();
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreateArrayPicketWindow>();
        }

        private Picket GetCopy(int index, (Result, Result, string) inputData)
        {
            return new Picket(string.Format("{0}-{1}", Name, index), Coordinate, inputData.Item3, _currentPicket.Method,
                              _currentPicket.PersonalGroup, _currentPicket.Equipments,
                              inputData.Item1, inputData.Item2, EndResult, Transformant_1, Transformant_2
                              , Transformant_3, IntermediateTransformant_1, IntermediateTransformant_2,
                              IntermediateTransformant_3);
        }
        private ObservableCollection<Result> GetResults()
        {
            var results = new ObservableCollection<Result>();
            foreach(var item in _values)
            {
                results.Add(item.Item1);
            }
            return results;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

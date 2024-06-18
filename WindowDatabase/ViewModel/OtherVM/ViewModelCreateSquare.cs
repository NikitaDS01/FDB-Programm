using FileDB.Core.Data.Tables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Entity;
using WindowDatabase.Core.Data.TableValue;
using WindowDatabase.Core.Dialog;
using WindowDatabase.Core.Export;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.ViewModel.OtherVM
{
    public class ViewModelCreateSquare : INotifyPropertyChanged, IViewModel
    {
        private ObservableCollection<Point2D> _points;
        private Square _currentSquare = null!;
        private Table _tableSquare;
        private bool _isCreate;
        private string _linkWork;
        private string _linkData;
        private int _createPointX = 0;
        private int _createPointY = 0;
        public ViewModelCreateSquare(Table tableIn)
        {
            _points = new ObservableCollection<Point2D>();
            _tableSquare = tableIn; 
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            CreatePointCommand = new RelayCommand(CreatePoint);
            DeletePointCommand = new RelayCommand(DeletePoint);
            SumLengthCommand = new RelayCommand(SumLength);
            SumSquareCommand = new RelayCommand(SumSquare);
            ImportCommand = new RelayCommand(Import);
            _linkWork = string.Empty;
            _linkData = string.Empty;
            _isCreate = true;

            _currentSquare = new Square();
        }
        public ViewModelCreateSquare(Table tableIn, Square squareIn)
        {
            _points = new ObservableCollection<Point2D>(squareIn.Points);
            _tableSquare = tableIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            CreatePointCommand = new RelayCommand(CreatePoint);
            DeletePointCommand = new RelayCommand(DeletePoint);
            SumLengthCommand = new RelayCommand(SumLength);
            SumSquareCommand = new RelayCommand(SumSquare);
            ImportCommand = new RelayCommand(Import);
            _linkWork = squareIn.SupervisorWork.FullName;
            _linkData = squareIn.SupervisorData.FullName;
            _isCreate = false;

            _currentSquare = squareIn;
        }
        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand SumLengthCommand { get; private set; }
        public ICommand SumSquareCommand { get; private set; }
        public ICommand CreatePointCommand { get; private set; }
        public ICommand DeletePointCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;
        public ObservableCollection<Point2D> Points => _points;

        #region Property
        public int PointX
        {
            get { return _createPointX; }
            set
            {
                _createPointX = value;
                OnPropertyChanged();
            }
        }
        public int PointY
        {
            get { return _createPointY; }
            set
            {
                _createPointY = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _currentSquare.Name; }
            set
            {
                _currentSquare.Name = value;
                OnPropertyChanged();
            }
        }
        public float LengthPerimeter
        {
            get { return _currentSquare.LengthPerimeter; }
            set
            {
                _currentSquare.LengthPerimeter = value;
                OnPropertyChanged();
            }
        }
        public float SquareValue
        {
            get { return _currentSquare.SquareValue; }
            set
            {
                _currentSquare.SquareValue = value;
                OnPropertyChanged();
            }
        }
        public DateTime BeginWork
        {
            get { return _currentSquare.BeginWork; }
            set
            {
                _currentSquare.BeginWork = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndWork
        {
            get { return _currentSquare.EndWork; }
            set
            {
                _currentSquare.EndWork = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreateRecord
        {
            get { return _currentSquare.CreateRecord; }
            set
            {
                _currentSquare.CreateRecord = value;
                OnPropertyChanged();
            }
        }
        public DateTime UpdateRecord
        {
            get { return _currentSquare.UpdateRecord; }
            set
            {
                _currentSquare.UpdateRecord = value;
                OnPropertyChanged();
            }
        }
        public string LinkWork
        {
            get { return _linkWork; }
            set
            {
                _linkWork = value;
                OnPropertyChanged();
            }
        }
        public string LinkData
        {
            get { return _linkData; }
            set
            {
                _linkData = value;
                OnPropertyChanged();
            }
        }
        #endregion
        
        private void CreatePoint(object args)
        {
            _points.Add(new Point2D(PointX, PointY));
            PointX = 0;
            PointY = 0;
            OnPropertyChanged(nameof(Points));
        }
        private void DeletePoint(object args)
        {
            if(_points.Count == 0)
                return;
            _points.RemoveAt(_points.Count - 1);
            OnPropertyChanged(nameof(Points));
        }
        private void SaveCustomer(object args)
        {
            if (_points.Count <= 2)
            {
                ShowDialog.Error("Укажите больше 2-ух точек!");
                return;
            }
            if (string.IsNullOrEmpty(LinkWork))
            {
                ShowDialog.Error("Ссылка не указывает на супервайзера полевых работ!");
                return;
            }
            if (string.IsNullOrEmpty(LinkData))
            {
                ShowDialog.Error("Ссылка не указывает на супервайзера обработки данных!");
                return;
            }
            if (_isCreate)
                _currentSquare.CreateRecord = DateTime.Now;

            _currentSquare.UpdateRecord = DateTime.Now;
            _currentSquare.Points = _points.ToArray();
            _currentSquare.SupervisorWork = new FileDB.Core.Data.RecordLink(LinkWork);
            _currentSquare.SupervisorData = new FileDB.Core.Data.RecordLink(LinkData);

            _tableSquare.WriteOne(_currentSquare);
            if (_isCreate)
                _tableSquare.CreateChildRecord(_currentSquare);
            WindowManager.Close<CreateSquareWindow>();
        }
        private void SumLength(object args)
        {
            if(_points.Count <= 2)
            {
                ShowDialog.Error("Укажите больше 2-ух точек!");
                return;
            }
            float length = 0f;
            length += Point2D.Length(_points[0], _points[_points.Count - 1]);
            for(int i = 0; i < _points.Count-1; i++)
            {
                length += Point2D.Length(_points[i], _points[i + 1]);
            }
            LengthPerimeter = length;
        }
        private void SumSquare(object args)
        {
            if (_points.Count <= 2)
            {
                ShowDialog.Error("Укажите больше 2-ух точек!");
                return;
            }
            float tmp1 = 0f;
            float tmp2 = 0f;
            for(int i = 0; i < _points.Count-1; i++)
            {
                tmp1 += _points[i].X * _points[i + 1].Y;
            }
            for (int i = 0; i < _points.Count - 1; i++)
            {
                tmp2 += _points[i + 1].X * _points[i].Y;
            }
            tmp1 += _points[_points.Count - 1].X * _points[0].Y;
            tmp2 += _points[0].X * _points[_points.Count - 1].Y;
            SquareValue = 0.5f * MathF.Abs(tmp1 - tmp2);
        }
        private void Import(object args)
        {
            var dialog = new FileExportDialog();
            if (dialog.OpenFileDialog() == true)
            {
                List<Point2D> points = new List<Point2D>();
                IExportPoint export;
                if (dialog.Extension == ".txt")
                {
                    export = new TxtExportPoint();
                    points = export.Run(dialog.FilePath);
                }
                if (dialog.Extension == ".xlsx")
                {
                    export = new ExcelExpotyPoint();
                    points = export.Run(dialog.FilePath);
                }
                for(int index = 0; index < points.Count; index++)
                    Points.Add(points[index]);
                OnPropertyChanged(nameof(Points));
                ShowDialog.Info(string.Format("Прочитано {0} записей", points.Count));
            }
            else
            {
                ShowDialog.Error("Не выбран файл!");
            }
        }
        private void CloseWindow(object args)
        {
            WindowManager.Close<CreateSquareWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

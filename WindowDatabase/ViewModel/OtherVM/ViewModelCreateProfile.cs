using FileDB.Core.Data.Tables;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class ViewModelCreateProfile: INotifyPropertyChanged, IViewModel
    {
        private ObservableCollection<Point2D> _points;
        private Profile _currentProfile = null!;
        private Table _tableProfile;
        private bool _isCreate;
        private int _createPointX = 0;
        private int _createPointY = 0;
        private Point2D _beginPoint;
        private Point2D _endPoint;

        public ViewModelCreateProfile(Table tableIn)
        {
            _points = new ObservableCollection<Point2D>();
            _tableProfile = tableIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            SumLengthCommand = new RelayCommand(SumLength);
            CreatePointCommand = new RelayCommand(CreatePoint);
            DeletePointCommand = new RelayCommand(DeletePoint);
            ImportCommand = new RelayCommand(Import);
            _isCreate = true;
            _beginPoint = Point2D.Empty;
            _endPoint = Point2D.Empty;

            _currentProfile = new Profile();
        }
        public ViewModelCreateProfile(Table tableIn, Profile profileIn)
        {
            Point2D[] points = new Point2D[profileIn.Points.Length - 2];
            Array.Copy(profileIn.Points, 1, points, 0, profileIn.Points.Length - 2);

            _points = new ObservableCollection<Point2D>(points);
            _tableProfile = tableIn;
            SaveCommand = new RelayCommand(SaveCustomer);
            CloseCommand = new RelayCommand(CloseWindow);
            SumLengthCommand = new RelayCommand(SumLength);
            CreatePointCommand = new RelayCommand(CreatePoint);
            DeletePointCommand = new RelayCommand(DeletePoint);
            ImportCommand = new RelayCommand(Import);
            _isCreate = false;
            _beginPoint = profileIn.Points[0];
            _endPoint = profileIn.Points[profileIn.Points.Length - 1];

            _currentProfile = profileIn;
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand SumLengthCommand { get; private set; }
        public ICommand CreatePointCommand { get; private set; }
        public ICommand DeletePointCommand { get; private set; }
        public ICommand ImportCommand { get; private set; }
        public bool IsReadOnlyName => !_isCreate;
        public ObservableCollection<Point2D> Points => _points;

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
            get { return _currentProfile.Name; }
            set
            {
                _currentProfile.Name = value;
                OnPropertyChanged();
            }
        }
        public float Length
        {
            get { return _currentProfile.Length; }
            set
            {
                _currentProfile.Length = value;
                OnPropertyChanged();
            }
        }
        public Point2D BeginPoint
        {
            get { return _beginPoint; }
            set
            {
                _beginPoint = value;
                OnPropertyChanged();
            }
        }
        public Point2D EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                OnPropertyChanged();
            }
        }
        public DateTime BeginWork
        {
            get { return _currentProfile.BeginWork; }
            set
            {
                _currentProfile.BeginWork = value;
                OnPropertyChanged();
            }
        }
        public DateTime EndWork
        {
            get { return _currentProfile.EndWork; }
            set
            {
                _currentProfile.EndWork = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreateRecord
        {
            get { return _currentProfile.CreateRecord; }
            set
            {
                _currentProfile.CreateRecord = value;
                OnPropertyChanged();
            }
        }
        public DateTime UpdateRecord
        {
            get { return _currentProfile.UpdateRecord; }
            set
            {
                _currentProfile.UpdateRecord = value;
                OnPropertyChanged();
            }
        }

        private void CreatePoint(object args)
        {
            _points.Add(new Point2D(PointX, PointY));
            PointX = 0;
            PointY = 0;
            OnPropertyChanged(nameof(Points));
        }
        private void DeletePoint(object args)
        {
            if (_points.Count == 0)
                return;
            _points.RemoveAt(_points.Count - 1);
            OnPropertyChanged(nameof(Points));
        }
        private void SumLength(object args)
        {
            if(_points.Count == 0)
            {
                Length = Point2D.Length(BeginPoint, EndPoint);
                return;
            }

            float length = 0f;
            length += Point2D.Length(BeginPoint, _points[0]);
            length += Point2D.Length(_points[0], EndPoint);
            for (int i = 0; i < _points.Count - 1; i++)
            {
                length += Point2D.Length(_points[i], _points[i + 1]);
            }
            Length = length;
        }
        private void SaveCustomer(object args)
        { 
            if (_isCreate)
                _currentProfile.CreateRecord = DateTime.Now;

            _currentProfile.UpdateRecord = DateTime.Now;
            _points.Insert(0, BeginPoint);
            _points.Add(EndPoint);
            _currentProfile.Points = _points.ToArray();

            _tableProfile.WriteOne(_currentProfile);
            if (_isCreate)
                _tableProfile.CreateChildRecord(_currentProfile);
            WindowManager.Close<CreateProfileWindow>();
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
                for (int index = 0; index < points.Count; index++)
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
            WindowManager.Close<CreateProfileWindow>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

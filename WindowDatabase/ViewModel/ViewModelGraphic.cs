using OxyPlot;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.TableValue;
using WindowDatabase.Windows;

namespace WindowDatabase.ViewModel
{
    public class ViewModelGraphic : INotifyPropertyChanged, IViewModel
    {
        private PlotModel _graphic = null!;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ViewModelGraphic()
        {
            _graphic = new PlotModel()
            {
                Title = "График",
                Subtitle = "Визуализация графика ЭДС и кажущегося сопротивления",
               
            };
            _graphic.Legends.Add(new OxyPlot.Legends.Legend()
            {
                LegendPosition = OxyPlot.Legends.LegendPosition.TopLeft,
                LegendFontSize = 12
            });
            _graphic.Axes.Add(new OxyPlot.Axes.LogarithmicAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Base = 10,
                ExtraGridlines = new double[] { 0 },
                Title = "Ом * м"
            });
            _graphic.Axes.Add(new OxyPlot.Axes.LogarithmicAxis()
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Base = 10,
                Title = "Время"
            });
            CloseCommand = new RelayCommand(Close);
        }

        public ICommand CloseCommand { get; private set; }
        public PlotModel GraphModel
        {
            get { return _graphic; }
            set
            {
                _graphic = value;
                OnPropertyChanged();
            }
        }

        public void AddPoints(Result[] results, string name)
        {
            var points = results.OrderByDescending(r => r.Time);
            var line = new OxyPlot.Series.LineSeries()
            {
                Title = name,
                DataFieldX = "Time",
                DataFieldY = "Value",
                TrackerFormatString = "{0}\nTime:{2}\nValue:{4}",
            };
            foreach(var result in points)
            {
                line.Points.Add(new DataPoint(result.Time, result.Value));
            }
            GraphModel.Series.Add(line);
            OnPropertyChanged(nameof(GraphModel));
        }
        private void Close(object args)
        {
            WindowManager.Close<GraphicWindow>();
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

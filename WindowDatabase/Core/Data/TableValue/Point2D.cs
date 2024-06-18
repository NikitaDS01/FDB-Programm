using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowDatabase.Core.Data.TableValue
{
    public class Point2D : INotifyPropertyChanged
    {
        public static Point2D Empty => new Point2D(0, 0);
        private int _x = 0;
        private int _y = 0;
        public int X
        {
            get { return _x; }
            set 
            { 
                _x = value; 
                OnPropertyChanged();
            } 
        }
        public int Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return string.Format("({0};{1})", X, Y);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static float Length(Point2D point1, Point2D point2)
        {
            int x = (point2.X - point1.X) * (point2.X - point1.X);
            int y = (point2.Y - point1.Y) * (point2.Y - point1.Y);
            return MathF.Sqrt(x + y);
        }
    }
}

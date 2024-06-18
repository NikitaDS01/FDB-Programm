using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowDatabase.Core.Data.TableValue
{
    public class Result : INotifyPropertyChanged
    {
        public static Result Empty => new Result(0f, 0f);
        private float _value = 0;
        private float _time = 0;
        public float Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }
        public float Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }
        public Result(float timeIn, float valueIn)
        {
            _value = valueIn;
            _time = timeIn;
        }
        public override string ToString()
        {
            return string.Format("{0};{1}", Time, Value);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowDatabase.Core.UserControls
{
    /// <summary>
    /// Логика взаимодействия для NumberParameterControl.xaml
    /// </summary>
    public partial class NumberParameterControl : UserControl
    {
        public NumberParameterControl()
        {
            InitializeComponent();
        }
        public static DependencyProperty TitleParameter =
            DependencyProperty.Register("Title", typeof(string), typeof(NumberParameterControl));

        public static DependencyProperty MaximumParameter =
            DependencyProperty.Register("Maximum", typeof(int), 
                typeof(NumberParameterControl), new UIPropertyMetadata(int.MaxValue));
        public static DependencyProperty MinimumParameter =
           DependencyProperty.Register("Minimum", typeof(int), 
               typeof(NumberParameterControl), new UIPropertyMetadata(int.MinValue));
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumberParameterControl),
                new UIPropertyMetadata(100) { CoerceValueCallback = new CoerceValueCallback(CorrectValue)}, 
                new ValidateValueCallback(ValidateValue));

        public static DependencyProperty IsReadOnlyParameter =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(NumberParameterControl), new UIPropertyMetadata(false));
        
        private static bool ValidateValue(object value)
        {
            string tmp = value.ToString();
            if (int.TryParse(tmp, out int number))
                return true;
            else
                return false;
        }
        private static object CorrectValue(DependencyObject obj, object baseValue)
        {
            int currentValue = (int)baseValue;
            int min = (int)obj.GetValue(MinimumParameter);
            int max = (int)obj.GetValue(MaximumParameter);
            if(currentValue < min)
                return min;
            if(currentValue > max)
                return max;

            return currentValue;
        }

        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyParameter); }
            set { SetValue(IsReadOnlyParameter, value); }
        }
        public string Title
        {
            get { return (string)GetValue(TitleParameter); }
            set { SetValue(TitleParameter, value); }
        }
        public int Maximum
        {
            get { return (int)GetValue(MaximumParameter); }
            set { SetValue(MaximumParameter, value); }
        }
        public int Minimum
        {
            get { return (int)GetValue(MinimumParameter); }
            set { SetValue(MinimumParameter, value); }
        }
        public int Value
        {
            get { return (int)GetValue(ValueProperty);}
            set { SetValue(ValueProperty, value); }
        }
    }
}

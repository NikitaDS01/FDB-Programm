using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ParameterTablePointControl.xaml
    /// </summary>
    public partial class ParameterTablePointControl : UserControl
    {
        public ParameterTablePointControl()
        {
            InitializeComponent();
        }
        public static DependencyProperty TitleParameter =
           DependencyProperty.Register("Title", typeof(string), typeof(ParameterTablePointControl));

        public static readonly DependencyProperty ValueYProperty =
            DependencyProperty.Register("ValueY", typeof(int), typeof(ParameterTablePointControl));
        public static readonly DependencyProperty ValueXProperty =
            DependencyProperty.Register("ValueX", typeof(int), typeof(ParameterTablePointControl));
        public static DependencyProperty IsReadOnlyParameter =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ParameterTablePointControl), new UIPropertyMetadata(false));

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
        public int ValueX
        {
            get { return (int)GetValue(ValueXProperty); }
            set { SetValue(ValueXProperty, value); }
        }
        public int ValueY
        {
            get { return (int)GetValue(ValueYProperty); }
            set { SetValue(ValueYProperty, value); }
        }
    }
}

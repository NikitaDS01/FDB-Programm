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
    public partial class ParameterTableResultControl : UserControl
    {
        public ParameterTableResultControl()
        {
            InitializeComponent();
        }
        public static DependencyProperty TitleParameter =
           DependencyProperty.Register("Title", typeof(string), typeof(ParameterTableResultControl));

        public static readonly DependencyProperty ValueTimeProperty =
            DependencyProperty.Register("ValueTime", typeof(float), typeof(ParameterTableResultControl), new UIPropertyMetadata(0.0f));
        public static readonly DependencyProperty ValueResultProperty =
            DependencyProperty.Register("ValueResult", typeof(float), typeof(ParameterTableResultControl), new UIPropertyMetadata(0.0f));
        public static DependencyProperty IsReadOnlyParameter =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ParameterTableResultControl), new UIPropertyMetadata(false));

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
        public float ValueResult
        {
            get { return (float)GetValue(ValueResultProperty); }
            set { SetValue(ValueResultProperty, value); }
        }
        public float ValueTime
        {
            get { return (float)GetValue(ValueTimeProperty); }
            set { SetValue(ValueTimeProperty, value); }
        }
    }
}

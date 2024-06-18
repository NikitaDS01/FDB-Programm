using System.Windows;
using System.Windows.Controls;

namespace WindowDatabase.Core.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ParameterTableControl.xaml
    /// </summary>
    public partial class ParameterTableControl : UserControl
    {
        public ParameterTableControl()
        {
            InitializeComponent();
        }
        public static DependencyProperty TitleParameter =
            DependencyProperty.Register("Title", typeof(string), typeof(ParameterTableControl));

        public static DependencyProperty InsideTextParameter =
            DependencyProperty.Register("InsideText", typeof(string), typeof(ParameterTableControl));
        
        public static DependencyProperty IsReadOnlyParameter =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ParameterTableControl), new UIPropertyMetadata(false));

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
        public string InsideText
        {
            get { return (string)GetValue(InsideTextParameter); }
            set { SetValue(InsideTextParameter, value); }
        }
    }
}

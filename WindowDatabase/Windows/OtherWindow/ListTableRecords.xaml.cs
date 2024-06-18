using FileDB.Core.Data;
using FileDB.Core.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WindowDatabase.Core.UserControls;

namespace WindowDatabase.Windows.OtherWindow
{
    /// <summary>
    /// Логика взаимодействия для ListTableRecords.xaml
    /// </summary>
    public partial class ListTableRecords : Window
    {
        private Table _table;
        public ListTableRecords(Table table)
        {
            InitializeComponent();
            _table = table;
            ListRecords = _table.Select(new FileDB.Core.Data.RecordSearch(0));
        }
        public static DependencyProperty TitleWindowParameter =
            DependencyProperty.Register("TitleWindow", typeof(string), typeof(ListTableRecords));
        public static DependencyProperty ListRecordsParameter =
            DependencyProperty.Register("ListRecords", typeof(Record[]), typeof(ListTableRecords));
        public static DependencyProperty SelectedItemParameter =
            DependencyProperty.Register("SelectedItem", typeof(Record), typeof(ListTableRecords));

        public string TitleWindow
        {
            get { return (string)GetValue(TitleWindowParameter); }
            set { SetValue(TitleWindowParameter, value); }
        }
        public Record[] ListRecords
        {
            get { return (Record[])GetValue(ListRecordsParameter); }
            set { SetValue(ListRecordsParameter, value); }
        }
        public Record SelectedItem
        {
            get { return (Record)GetValue(SelectedItemParameter); }
            set { SetValue(SelectedItemParameter, value); }
        }

        private void ButtonClickExit(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void ButtonClickOpen(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null)
            {
                Core.Dialog.ShowDialog.Warning("Вы не выбрали элемент");
                return;
            }
            DialogResult = true;
            this.Close();
        }
    }
}

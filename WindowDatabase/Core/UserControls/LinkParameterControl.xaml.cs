 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Dialog;

namespace WindowDatabase.Core.UserControls
{
    /// <summary>
    /// Логика взаимодействия для LinkParameterControl.xaml
    /// </summary>
    public partial class LinkParameterControl : UserControl
    {
        public enum TableType
        {
            Contract = 0,
            Customer = 1,
            Project = 2,
            Group = 4,
            Chief = 8,
            Driver = 16,
            Engineer = 32,
            Worker = 64,
            Supervisor = 128,
            Measuring = 256,
            Generator = 512,
            Telemetry = 1024,
            Methodology = 2048,
            EquipmentGroup = 4096
        }
        private TxtFileDialog _fileDialog;

        public LinkParameterControl()
        {
            InitializeComponent();
        }
        public static DependencyProperty TypeTableParameter =
            DependencyProperty.Register("TypeTable", typeof(TableType), typeof(LinkParameterControl));
        public static DependencyProperty TitleParameter =
            DependencyProperty.Register("Title", typeof(string), typeof(LinkParameterControl));
        public static DependencyProperty PathParameter = 
            DependencyProperty.Register("Path", typeof(string), typeof(LinkParameterControl));
        public static DependencyProperty IsButtonEnableParameter =
            DependencyProperty.Register("IsButtonEnabl", typeof(bool), typeof(NumberParameterControl), new UIPropertyMetadata(true));


        public string Title
        {
            get { return (string)GetValue(TitleParameter); }
            set { SetValue(TitleParameter, value); }
        }
        public TableType TypeTable
        {
            get { return (TableType)GetValue(TypeTableParameter); }
            set { SetValue(TypeTableParameter, value); }
        }
        public string Path
        {
            get { return (string)GetValue(PathParameter); }
            set { SetValue(PathParameter, value); }
        }
        public bool IsButtonEnable
        {
            get { return (bool)GetValue(IsButtonEnableParameter); }
            set { SetValue(IsButtonEnableParameter, value); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _fileDialog = new TxtFileDialog(TypeTable);
                if (_fileDialog.OpenFileDialog())
                {
                    Path = _fileDialog.FilePath;

                }
            }
            catch (Exception ex)
            {
                ShowDialog.Error(ex.Message);
            }
        }
    }
}

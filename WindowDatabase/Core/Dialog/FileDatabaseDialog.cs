using Microsoft.Win32;
using System.Windows;

namespace WindowDatabase.Core.Dialog
{
    class FileDatabaseDialog : IOpenDialogService
    {
        public string FilePath { get; set; } = string.Empty;

        public bool OpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Файл базы данных(*.fdb)|*.fdb";
            if(dialog.ShowDialog() == true)
            {
                this.FilePath = dialog.FileName;
                return true;
            }
            return false;
        }
    }
}

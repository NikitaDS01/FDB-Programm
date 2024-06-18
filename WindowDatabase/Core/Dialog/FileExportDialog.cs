using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowDatabase.Core.Dialog
{
    public class FileExportDialog : IOpenDialogService
    {
        public string FilePath { get; set; } = string.Empty;
        public string Extension => Path.GetExtension(FilePath);

        public bool OpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Текст экспорта(*.txt)|*.txt|Таблица экспорта(*.xlsx)|*.xlsx";
            if (dialog.ShowDialog() == true)
            {
                this.FilePath = dialog.FileName;
                return true;
            }
            return false;
        }
    }
}

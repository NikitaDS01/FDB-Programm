using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WindowDatabase.Core.Dialog
{
    class OpenDirectoryDialog : IOpenDialogService
    {
        public string FilePath { get; set; } = string.Empty;

        public bool OpenFileDialog()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.FilePath = dialog.SelectedPath;
                return true;
            }
            return false;
        }
    }
}

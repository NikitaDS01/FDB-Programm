using Microsoft.Win32;

namespace WindowDatabase.Core.Dialog
{
    public class FileDiscrepanciesDialog : ISaveDialogService
    {
        public string FilePath { get; set; } = string.Empty;

        public bool SaveFileDialog()
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Текстовый файл(*.txt)|*.txt";
            if (dialog.ShowDialog() == true)
            {
                this.FilePath = dialog.FileName;
                return true;
            }
            return false;
        }
    }
}

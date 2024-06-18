namespace WindowDatabase.Core.Dialog
{
    interface ISaveDialogService
    {
        string FilePath { get; set; }
        bool SaveFileDialog();
    }
}

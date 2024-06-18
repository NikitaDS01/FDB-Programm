using WindowDatabase.Core.Data.Projects;

namespace WindowDatabase.ViewModel.RootVM
{
    public class EntranceRootViewModel : RootViewModel
    {
        public ListPathDatabase ListPathDatabase { get; private set; }
        public EntranceRootViewModel(ListPathDatabase listIn) : base(new ViewModelEntranceDatabase(listIn))
        {
            this.ListPathDatabase = listIn;
        }
        public void ChangeEntranceWindow()
        {
            this.ChangeVM(new ViewModelEntranceDatabase(ListPathDatabase));
        }
        public void ChangeCreateDBWindow()
        {
            this.ChangeVM(new ViewModelCreateDatabase());
        }
        public void AddPath(PathDatabase pathIn)
        {
            ListPathDatabase.Add(pathIn);
        }
    }
}

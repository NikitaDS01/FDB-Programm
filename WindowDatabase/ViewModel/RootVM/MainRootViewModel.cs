using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.Core.Data.Projects;
using WindowDatabase.ViewModel.Component;
using WindowDatabase.Windows;

namespace WindowDatabase.ViewModel.RootVM
{
    public class MainRootViewModel : RootViewModel
    {
        public MainRootViewModel() : base(new ViewModelVoid())
        {
            ReturnDBCommand = new RelayCommand(ReturnDataBase);
        }

        private void ReturnDataBase(object args)
        {
            Database.Close();
            this.ChangeVM(new ViewModelVoid());
            var listPathDatabase = new ListPathDatabase(Settings.LoadPath());
            var entranceView = new EntranceWindow();

            var entranceRootVM = new EntranceRootViewModel(listPathDatabase);
            WindowManager.OpenDialog(entranceView, entranceRootVM);

            var mainWindow = WindowManager.GetWindow<MainWindow>();
            if (mainWindow == null)
                throw new ArgumentNullException(nameof(mainWindow));

            if (!Database.IsInit)
            {
                WindowManager.Close(mainWindow);
                return;
            }
        }
        public ICommand ReturnDBCommand { get; private set; }

        public void ChangeCustomer()
        {
            this.ChangeVM(new ViewModelCustomer());
        }
        public void ChangeProject()
        {
            this.ChangeVM(new ViewModelProject());
        }
        public void ChangeContract()
        {
            this.ChangeVM(new ViewModelConctract());
        }

        public void ChangeChief()
        {
            this.ChangeVM(new ViewModelChief());
        }
        public void ChangeDriver()
        {
            this.ChangeVM(new ViewModelDriver());
        }
        public void ChangeWorker()
        {
            this.ChangeVM(new ViewModelWorker());
        }
        public void ChangeEngineer()
        {
            this.ChangeVM(new ViewModelEngineer());
        }
        public void ChangeSupervisor()
        {
            this.ChangeVM(new ViewModelSupervisor());
        }
        public void ChangeMeasuring()
        {
            this.ChangeVM(new ViewModelMeasuringEquipment());
        }
        public void ChangeTelemetry()
        {
            this.ChangeVM(new ViewModelTelemetryEquipment());
        }
        public void ChangeGenerator()
        {
            this.ChangeVM(new ViewModelGeneratorEquipment());
        }
        public void ChangeMethodology()
        {
            this.ChangeVM(new ViewModelMethodology());
        }
        public void ChangePersonalGroup()
        {
            this.ChangeVM(new ViewModelPersonalGroup());
        }
        public void ChangeEquipmentGroup()
        {
            this.ChangeVM(new ViewModelEquipmentGroup());
        }
    }
}

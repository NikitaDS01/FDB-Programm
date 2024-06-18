using System;
using System.Windows.Input;
using WindowDatabase.Core;
using WindowDatabase.Core.Command;
using WindowDatabase.ViewModel.Component;
using WindowDatabase.ViewModel.RootVM;
using WindowDatabase.Windows;

namespace WindowDatabase.ViewModel
{
    public class MenuViewModel : IViewModel
    {
        public MenuViewModel()
        {
            CursomerCommand = new RelayCommand(ChangeWindowCustomer);
            ProjectCommand = new RelayCommand(ChangeWindowProject);
            ContractCommand = new RelayCommand(ChangeWindowContract);

            ChiefCommand = new RelayCommand(ChangeWindowChief);
            DriverCommand = new RelayCommand(ChangeWindowDriver);
            WorkerCommand = new RelayCommand(ChangeWindowWorker);
            EngineerCommand = new RelayCommand(ChangeWindowEngineer);
            SupervisorCommand = new RelayCommand(ChangeWindowSupervisor);

            MeasuringCommand = new RelayCommand(ChangeWindowMeasuring);
            GeneratorCommand = new RelayCommand(ChangeWindowGenerator);
            TelemetryCommand = new RelayCommand(ChangeWindowTelemetry);
            EquipmentsCommand = new RelayCommand(ChangeWindowEquipmentGroup);

            MethodCommand = new RelayCommand(ChangeWindowMethodology);
            GroupCommand = new RelayCommand(ChangeWindowPersonalGroup);
        }
        public ICommand CursomerCommand { get; private set; }
        public ICommand ProjectCommand { get; private set; }
        public ICommand ContractCommand { get; private set; }
        public ICommand ChiefCommand { get; private set; }
        public ICommand DriverCommand { get; private set; }
        public ICommand WorkerCommand { get; private set; }
        public ICommand EngineerCommand { get; private set; }
        public ICommand SupervisorCommand { get; private set; }

        public ICommand MeasuringCommand { get; private set; }
        public ICommand GeneratorCommand { get; private set; }
        public ICommand TelemetryCommand { get; private set; }
        public ICommand EquipmentsCommand { get; private set; }

        public ICommand MethodCommand { get; private set; }
        public ICommand GroupCommand { get; private set; }



        private void ChangeWindowContract(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeContract();
        }
        private void ChangeWindowCustomer(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if(rootVM == null) 
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeCustomer();
        }
        private void ChangeWindowProject(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeProject();
        }
        private void ChangeWindowChief(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeChief();
        }
        private void ChangeWindowEngineer(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeEngineer();
        }
        private void ChangeWindowWorker(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeWorker();
        }
        private void ChangeWindowDriver(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeDriver();
        }
        private void ChangeWindowSupervisor(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeSupervisor();
        }
        private void ChangeWindowMeasuring(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeMeasuring();
        }
        private void ChangeWindowGenerator(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeGenerator();
        }
        private void ChangeWindowTelemetry(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeTelemetry();
        }

        private void ChangeWindowMethodology(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeMethodology();
        }
        private void ChangeWindowPersonalGroup(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangePersonalGroup();
        }
        private void ChangeWindowEquipmentGroup(object args)
        {
            var rootVM = WindowManager.GetViewModel<MainWindow>() as MainRootViewModel;
            if (rootVM == null)
                throw new ArgumentNullException(nameof(rootVM));
            rootVM.ChangeEquipmentGroup();
        }
    }
}

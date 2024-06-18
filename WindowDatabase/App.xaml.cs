using System;
using System.Windows;
using WindowDatabase.Core;
using WindowDatabase.Core.Data.Projects;
using WindowDatabase.Core.Data.TableValue;
using WindowDatabase.Core.Dialog;
using WindowDatabase.ViewModel;
using WindowDatabase.ViewModel.Component;
using WindowDatabase.ViewModel.RootVM;
using WindowDatabase.Windows;

namespace WindowDatabase
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ListPathDatabase _listPathDatabase = null!;
        private RootViewModel _entranceRootVM = null!;
        private RootViewModel _mainRootVM = null!;

        private Window _entranceView = null!; 
        private Window _mainView = null!; 
        protected override void OnStartup(StartupEventArgs e)
        {
            FileDB.Function.FunctionField.AddConstructor(new ConstructorPoint2D());
            FileDB.Function.FunctionField.AddConstructor(new ConstructorResult());
            base.OnStartup(e);
            ShowDialog.Info("Запуск программы");
            _listPathDatabase = new ListPathDatabase(Settings.LoadPath());

            _entranceRootVM = new EntranceRootViewModel(_listPathDatabase);
            _mainView = new MainWindow();

            _mainRootVM = new MainRootViewModel();
            _entranceView = new EntranceWindow();

            WindowManager.Open(_mainView, _mainRootVM);
            WindowManager.OpenDialog(_entranceView, _entranceRootVM);
            

            if (!Database.IsInit)
            {
                WindowManager.Close(_mainView);
                return;
            }

        }

        protected override void OnDeactivated(EventArgs e)
        {
            Settings.SavePath(_listPathDatabase.Databases);
            base.OnDeactivated(e);
        }
    }
}

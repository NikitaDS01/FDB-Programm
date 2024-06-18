using FileDB.Core.Data.Tables;
using Microsoft.Win32;
using WindowDatabase.Core.UserControls;
using WindowDatabase.Windows.OtherWindow;

namespace WindowDatabase.Core.Dialog
{
    public class TxtFileDialog : IOpenDialogService
    {
        private Table _table;
        public TxtFileDialog(LinkParameterControl.TableType tableTypeIn)
        {
            string type;
            switch(tableTypeIn)
            {
                case LinkParameterControl.TableType.Contract: type = Settings.TableContract; break;
                case LinkParameterControl.TableType.Customer: type = Settings.TableCustomer; break;
                case LinkParameterControl.TableType.Project: type = Settings.TableProject; break;
                case LinkParameterControl.TableType.Group: type = Settings.TableGroup; break;
                case LinkParameterControl.TableType.Chief: type = Settings.TableChief; break;
                case LinkParameterControl.TableType.Driver: type = Settings.TableDriver; break;
                case LinkParameterControl.TableType.Engineer: type = Settings.TableEngineer; break;
                case LinkParameterControl.TableType.Worker: type = Settings.TableWorker; break;
                case LinkParameterControl.TableType.Supervisor: type = Settings.TableSupervisor; break;
                case LinkParameterControl.TableType.Measuring: type = Settings.TableMeasuring; break;
                case LinkParameterControl.TableType.Generator: type = Settings.TableGenerator; break;
                case LinkParameterControl.TableType.Telemetry: type = Settings.TableTelemetry; break;
                case LinkParameterControl.TableType.Methodology: type = Settings.TableMethodology; break;
                case LinkParameterControl.TableType.EquipmentGroup: type = Settings.TableEquipments; break;
                default: type = string.Empty; break;
            }
            _table = Database.CurrentDatabase.GetRootTable(type);
        }
        public string FilePath { get; set; } = string.Empty;

        public bool OpenFileDialog()
        {
            var window = new ListTableRecords(_table);
            if(window.ShowDialog() == true)
            {
                FilePath = window.SelectedItem.Path;
                return true;
            }

            return false;
        }
    }
}

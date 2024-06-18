using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowDatabase.Core.Dialog
{
    interface IOpenDialogService
    {
        string FilePath { get; set; }
        bool OpenFileDialog();  
    }
}

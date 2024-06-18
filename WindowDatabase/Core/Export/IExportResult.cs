using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core.Export
{
    public interface IExportResult
    {
        List<(Result, Result, string)> Run(string fileIn);
    }
}

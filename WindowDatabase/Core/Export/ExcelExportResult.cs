using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core.Data.TableValue;
using WindowDatabase.Core.Dialog;

namespace WindowDatabase.Core.Export
{
    public class ExcelExportResult : IExportResult
    {
        public List<(Result, Result, string)> Run(string fileIn)
        {
            if (!File.Exists(fileIn))
                throw new FileNotFoundException();
            FileStream? file = null;
            try
            {
                file = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
                IWorkbook workbook = new XSSFWorkbook(file);
                ISheet sheet = workbook.GetSheetAt(0);
                var list = new List<(Result, Result, string)>();
                for(int index = 0; index <= sheet.LastRowNum; index++)
                {
                    if (sheet.GetRow(index) == null)
                        break;

                    var row = sheet.GetRow(index);
                    var time1 = row.GetCell(0).ToString();
                    var value1 = row.GetCell(1).ToString();
                    var time2 = row.GetCell(2).ToString();
                    var value2 = row.GetCell(3).ToString();
                    var type = row.GetCell(4).ToString();

                    var result1 = new Result(Convert.ToSingle(time1), Convert.ToSingle(value1));
                    var result2 = new Result(Convert.ToSingle(time2), Convert.ToSingle(value2));
                    list.Add((result1, result2, type));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception();
            }
            finally
            {
                file?.Close();
            }
        }
    }
}

using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core.Export
{
    public class ExcelExpotyPoint : IExportPoint
    {
        public List<Point2D> Run(string fileIn)
        {
            if (!File.Exists(fileIn))
                throw new FileNotFoundException();
            FileStream? file = null;
            try
            {
                file = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
                IWorkbook workbook = new XSSFWorkbook(file);
                ISheet sheet = workbook.GetSheetAt(0);
                var list = new List<Point2D>();
                for (int index = 0; index <= sheet.LastRowNum; index++)
                {
                    if (sheet.GetRow(index) == null)
                        break;

                    var row = sheet.GetRow(index);
                    var pointX = row.GetCell(0).ToString();
                    var pointY = row.GetCell(1).ToString();

                    list.Add(new Point2D(Convert.ToInt32(pointX),
                                         Convert.ToInt32(pointY)));
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

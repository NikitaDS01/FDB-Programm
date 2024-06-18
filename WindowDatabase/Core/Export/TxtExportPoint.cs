using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core.Export
{
    public class TxtExportPoint : IExportPoint
    {
        public List<Point2D> Run(string fileIn)
        {
            if (!File.Exists(fileIn))
                throw new FileNotFoundException();
            FileStream? file = null;
            try
            {
                file = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];

                file.Read(buffer, 0, buffer.Length);

                string fullText = Encoding.Default.GetString(buffer);
                fullText = fullText.Replace("\r", string.Empty);
                var list = new List<Point2D>();
                string[] lines = fullText.Split('\n');
                foreach (var line in lines)
                {
                    string[] results = line.Split(';');
                    if (results.Length != 2)
                        continue;
                    list.Add(new Point2D(Convert.ToInt32(results[0]),
                                         Convert.ToInt32(results[1])));
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

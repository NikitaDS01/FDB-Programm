using FileDB.Core.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core.Export
{
    public class TxtExportResult : IExportResult
    {
        public List<(Result, Result, string)> Run(string fileIn)
        {
            if(!File.Exists(fileIn)) 
                throw new FileNotFoundException();
            FileStream? file = null;
            try
            {
                file = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[file.Length];

                file.Read(buffer, 0, buffer.Length);

                string fullText = Encoding.Default.GetString(buffer);
                fullText = fullText.Replace("\r", string.Empty);
                var list = new List<(Result, Result, string)>();
                string[] lines = fullText.Split('\n');
                foreach(var line in lines)
                {
                    string[] values = line.Split(' ');
                    if (values.Length != 3)
                        continue;
                    string[] results1 = values[0].Split(';');
                    string[] results2 = values[1].Split(';');
                    if (results1.Length != 2)
                        continue;
                    if (results2.Length != 2)
                        continue;
                    list.Add((new Result(Convert.ToSingle(results1[0]), Convert.ToSingle(results1[1]) ),
                              new Result(Convert.ToSingle(results2[0]), Convert.ToSingle(results2[1]) ),
                              values[2]));
                }
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                file?.Close();
            }
            throw new Exception();
        }
    }
}

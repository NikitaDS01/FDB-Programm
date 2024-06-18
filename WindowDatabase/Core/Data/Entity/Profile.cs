using FileDB.Core.Attribute;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core.Data.Entity
{
    public class Profile : IDataDatabase
    {
        [SerializeIndex]
        public string Name { get; set; }

        public Point2D[] Points { get; set; }
        public float Length { get; set; }   

        public DateTime BeginWork { get; set; }
        public DateTime EndWork { get; set; }
        public DateTime CreateRecord { get; set; }
        public DateTime UpdateRecord { get; set; }

        public Profile(string name, Point2D[] points, float length, DateTime beginWork, DateTime endWork, DateTime createRecord, DateTime updateRecord)
        {
            Name = name;
            Points = points;
            Length = length;
            BeginWork = beginWork;
            EndWork = endWork;
            CreateRecord = createRecord;
            UpdateRecord = updateRecord;
        }
        public Profile()
        {
            Name = string.Empty;
            Points = new Point2D[0];
            Length = 0;
            BeginWork = DateTime.Now;
            EndWork = DateTime.Now;
            CreateRecord = DateTime.Now;
            UpdateRecord = DateTime.Now;
        }
    }
}

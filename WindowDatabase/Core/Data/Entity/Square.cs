using FileDB.Core.Attribute;
using FileDB.Core.Data;
using System;
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core.Data.Entity
{
    public class Square : IDataDatabase
    {

        [SerializeIndex]
        public string Name { get; set; }
        public Point2D[] Points { get; set; }
        public float LengthPerimeter { get; set; }
        public float SquareValue { get; set; }

        public DateTime BeginWork { get; set; }
        public DateTime EndWork { get; set; }
        public DateTime CreateRecord { get; set; }
        public DateTime UpdateRecord { get; set; }

        public RecordLink SupervisorWork { get; set; }
        public RecordLink SupervisorData { get; set; }
        public Square(string name, Point2D[] points, float lengthPerimeter, float squareValue, DateTime beginWork, DateTime endWork, DateTime createRecord, DateTime updateRecord, RecordLink supervisorWork, RecordLink supervisorData)
        {

            Name = name;
            Points = points;
            LengthPerimeter = lengthPerimeter;
            SquareValue = squareValue;
            BeginWork = beginWork;
            EndWork = endWork;
            CreateRecord = createRecord;
            UpdateRecord = updateRecord;
            SupervisorWork = supervisorWork;
            SupervisorData = supervisorData;
        }
        public Square()
        {
            Name = string.Empty;
            Points = new Point2D[0];
            LengthPerimeter = 0;
            SquareValue = 0;
            BeginWork = DateTime.Now;
            EndWork = DateTime.Now;
            CreateRecord = DateTime.Now;
            UpdateRecord = DateTime.Now;
            SupervisorWork = RecordLink.Empty;
            SupervisorData = RecordLink.Empty;
        }
        
    }
}

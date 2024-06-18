using ConsoleTest.Data;
using FileDB.Core.Attribute;
using FileDB.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core.Data.Entity
{
    public class Picket 
    {
        [SerializeIndex]
        public string Name { get; set; }
        public Point2D Coordinate { get; set; }
        public string Type { get; set; }
        public RecordLink Method { get; set; }
        public RecordLink PersonalGroup { get; set; }
        public RecordLink Equipments { get; set; }

        //Окончательный результат измерения (кривая измерения)
        public Result Result { get; set; }
        public Result IntermediateResult { get; set; }
        public DateTime EndResult { get; set; }
        public Result Transformant_1 { get; set; }
        public Result Transformant_2 { get; set; }
        public Result Transformant_3 { get; set; }
        public Result IntermediateTransformant_1 { get; set; }
        public Result IntermediateTransformant_2 { get; set; }
        public Result IntermediateTransformant_3 { get; set; }
        public Picket(string name, Point2D coordinate, string type, RecordLink method, RecordLink personalGroup, RecordLink equipments, 
            Result result, Result intermediateResult, DateTime endResult, Result transformant_1, Result transformant_2, Result transformant_3,
            Result intermediateTransformant_1, Result intermediateTransformant_2, Result intermediateTransformant_3)
        {
            Name = name;
            Coordinate = coordinate;
            Type = type;
            Method = method;
            PersonalGroup = personalGroup;
            Equipments = equipments;
            Result = result;
            IntermediateResult = intermediateResult;
            EndResult = endResult;
            Transformant_1 = transformant_1;
            Transformant_2 = transformant_2;
            Transformant_3 = transformant_3;
            IntermediateTransformant_1 = intermediateTransformant_1;
            IntermediateTransformant_2 = intermediateTransformant_2;
            IntermediateTransformant_3 = intermediateTransformant_3;
        }
        public Picket()
        {
            Name = string.Empty;
            Coordinate = Point2D.Empty;
            Type = string.Empty;
            Method = RecordLink.Empty;
            PersonalGroup = RecordLink.Empty;
            Equipments = RecordLink.Empty;
            Result = Result.Empty;
            IntermediateResult = Result.Empty;
            EndResult = DateTime.Now;
            Transformant_1 = Result.Empty;
            Transformant_2 = Result.Empty;
            Transformant_3 = Result.Empty;
            IntermediateTransformant_1 = Result.Empty;
            IntermediateTransformant_2 = Result.Empty;
            IntermediateTransformant_3 = Result.Empty;
        }
    }
}

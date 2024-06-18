using FileDB.Core.Data.TypeField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowDatabase.Core.Data.TableValue
{
    public class FieldPoint2D : AbstractRecordField
    {
        private readonly Point2D _currentPoint;
        public FieldPoint2D(Point2D pointIn, string nameIn, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _currentPoint = pointIn;
        }
        public FieldPoint2D(int xIn, int yIn, string nameIn, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _currentPoint = new Point2D(xIn, yIn);
        }

        public override object Value => _currentPoint;

        public override string Convert()
        {
            return $"[{Name}][Point]:[{Value}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if (fieldIn is not FieldPoint2D || fieldIn.Name != this.Name)
            {
                return false;
            }
            else
            {
                Point2D field = (Point2D)fieldIn.Value;
                return _currentPoint.X == field.X && 
                       _currentPoint.Y == field.Y;
            }
        }

        public override bool LargeField(AbstractRecordField fieldIn)
        {
            throw new NotImplementedException();
        }

        public override bool LessField(AbstractRecordField fieldIn)
        {
            throw new NotImplementedException();
        }
    }
}

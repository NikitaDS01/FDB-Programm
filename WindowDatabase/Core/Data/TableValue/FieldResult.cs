using FileDB.Core.Data.TypeField;
using System;

namespace WindowDatabase.Core.Data.TableValue
{
    public class FieldResult : AbstractRecordField
    {
        private readonly Result _currentResult;
        public FieldResult(Result pointIn, string nameIn, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _currentResult = pointIn;
        }
        public FieldResult(float valueIn, float timeIn, string nameIn, bool isIndexIn = false) : base(nameIn, isIndexIn)
        {
            _currentResult = new Result(timeIn, valueIn);
        }
        public override object Value => _currentResult;

        public override string Convert()
        {
            return $"[{Name}][Result]:[{Value}]";
        }

        public override bool EqualsField(AbstractRecordField fieldIn)
        {
            if (fieldIn is not FieldResult || fieldIn.Name != this.Name)
            {
                return false;
            }
            else
            {
                Result field = (Result)fieldIn.Value;
                return _currentResult.Value == field.Value &&
                       _currentResult.Time == field.Time;
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

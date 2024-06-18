using FileDB.Core.Data.TypeField;
using FileDB.Function.ConstructorField;
using System;
using System.Collections;

namespace WindowDatabase.Core.Data.TableValue
{
    public class ConstructorResult : IConstructorField
    {
        private const string TYPE = "Result";
        public IList GetArrayType(int countIn)
        {
            return new Result[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if (typeIn == typeof(Result))
                return true;
            return false;
        }

        public bool IsDefaultValue(string typeIn)
        {
            if (typeIn == TYPE)
                return true;
            return false;
        }

        public AbstractRecordField StringToField(string nameIn, string valueIn, bool isIndexIn)
        {
            string[] text = valueIn.Split(';');
            return new FieldResult(Convert.ToSingle(text[1]), Convert.ToSingle(text[0]), nameIn, isIndexIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldResult((Result)valueIn, nameIn, isIndex);
        }
    }
}

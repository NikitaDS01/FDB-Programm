using FileDB.Core.Data.TypeField;
using FileDB.Function.ConstructorField;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowDatabase.Core.Data.TableValue
{
    public class ConstructorPoint2D : IConstructorField
    {
        private const string TYPE = "Point";
        public IList GetArrayType(int countIn)
        {
            return new Point2D[countIn];
        }

        public bool IsDefaultValue(Type typeIn)
        {
            if (typeIn == typeof(Point2D))
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
            string[] text = valueIn.Trim(new char[] { '(', ')' }).Split(';');
            return new FieldPoint2D(Convert.ToInt32(text[0]), Convert.ToInt32(text[1]), nameIn, isIndexIn);
        }

        public AbstractRecordField ValueToField(string nameIn, object valueIn, bool isIndex)
        {
            return new FieldPoint2D((Point2D)valueIn, nameIn, isIndex);
        }
    }
}

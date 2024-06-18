using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowDatabase.Core.Data.TableValue;

namespace WindowDatabase.Core
{
    public static class Calculation
    {
        private const int Q = 10000;
        private const int q = 400;
        private static float U0 => 4 * MathF.PI * MathF.Pow(10f, -7f);
        public static Result Transformant(Result picketResult)
        {
            var tmp1 = U0 / (MathF.PI * picketResult.Time);
            var tmp2 = (Q * q * U0) / (20 * picketResult.Time * picketResult.Value);
            return new Result(
                picketResult.Time,
                tmp1 * MathF.Pow(MathF.Abs(tmp2), 2 / 3)
                );
        }
    }
}

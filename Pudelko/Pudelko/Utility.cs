using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P = Pudelko;

namespace Utility
{
    public static class Utility
    {
        public static P.Pudelko Compress(this P.Pudelko pudelko)
        {
            decimal size = (decimal)Math.Pow((double)pudelko.Volume, 1.0 / 3.0);

            return new P.Pudelko(size, size, size, pudelko.Unit);
        }
    }
}

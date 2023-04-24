using Pudelko.Enums;
using System.Text.Encodings;
using System.IO;
using System.Text;
using Pudelko.Utility;

namespace Pudelko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string s = "2 m × 9 m × 1 m";
            double b = 0.2011;
            decimal v = Convert.ToDecimal(Math.Round(b, 3));   
            Pudelko? x = Pudelko.Parse(s);
            Pudelko? y = new Pudelko(145.22m,14.79m, 60, UnitOfMeasure.centimeter);
            var z = x + y;
            ValueTuple<int, int, int> o = (3, 2, 2);

            Console.WriteLine(Pudelko.UnitConvertor(Math.Round(2*(decimal)(2.50) * (decimal)(2.50) + (decimal)(2.50)*(decimal)(2.50)*2+(decimal)(2.50)*(decimal)(2.50)*2, 9), UnitOfMeasure.meter, UnitOfMeasure.meterCube));

            Console.WriteLine(Pudelko.UnitConvertor(Math.Round(y.Length*y.Width*y.Height,9), y.Unit, UnitOfMeasure.meterCube));
            Console.WriteLine(y.Square);
            
        }
    }
}
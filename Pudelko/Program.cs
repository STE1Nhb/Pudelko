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


            Pudelko? x = new Pudelko(1000,100,90,UnitOfMeasure.milimeter);
            Pudelko? y = new Pudelko(1.2m,0.5m,6.1m, UnitOfMeasure.meter);
            var z = x + y;
            ValueTuple<int, int, int> o = (3, 2, 2);

            Pudelko v = o;
            double[] g = (double[])x;
            foreach(var i in v)
            {
                Console.WriteLine(i);
            }

     
            
        }
    }
}
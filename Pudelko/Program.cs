using Pudelko.Enums;
using System.Text.Encodings;
using System.IO;
using System.Text;

namespace Pudelko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            Pudelko? x = new Pudelko(100,100,100,UnitOfMeasure.milimeter);
            Pudelko? y = new Pudelko(0.1m,0.1m,0.1m, UnitOfMeasure.meter);
            Console.WriteLine(x.Equals(y));
        }
    }
}
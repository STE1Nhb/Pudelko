using Pudelko.Enums;
using System.Text.Encodings;
using System.IO;
using System.Text;
using U = Utility;
using System.Globalization;

namespace Pudelko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo current = CultureInfo.CurrentCulture;
            CultureInfo newCulture; 
            if (current.Name != "en-US")
                newCulture = new CultureInfo("en-US");
            else
                newCulture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = newCulture;

            ValueTuple<int, int, int> o = (3, 2, 2);
            string j = "4.0 m × 10.0 m × 5.5 m";
            Pudelko x = Pudelko.Parse(j);
            Console.WriteLine("{0:cM}", x);
            Console.WriteLine(x.Volume);

            Console.WriteLine(U.Utility.Compress(x));

        }
    }
}
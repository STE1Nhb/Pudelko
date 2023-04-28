using Pudelko.Enums;
using System.Text.Encodings;
using System.IO;
using System.Text;
using U = Utility;
using System.Globalization;
using Utility;

namespace Pudelko
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // OutPut Settings
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo current = CultureInfo.CurrentCulture;
            CultureInfo newCulture; 
            if (current.Name != "en-US")
                newCulture = new CultureInfo("en-US");
            else
                newCulture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = newCulture;

            // ToString functionality
            Pudelko pMeter = new Pudelko(5,5,5, UnitOfMeasure.meter);
            Pudelko pNoParams = new Pudelko();
            Console.WriteLine("Pudelko(meter):\n{0:M}\nPudelko(centimeter):\n{0:CM}\nPudelko(milimeter):\n{0:MM}", pMeter);
            Console.WriteLine("Pudelko without parameters(new Pudelko()):\n{0:M}", pNoParams);
            // Comparison
            Pudelko pCenti = new Pudelko(500, 31, 502, UnitOfMeasure.centimeter);
            Pudelko pMili = new Pudelko(5000, 5000, 5000, UnitOfMeasure.milimeter);
            Console.WriteLine("\nPudelko comparison:");
            Console.WriteLine($"(Equals)First comparison:\n{pMeter.Equals(pCenti)}\n(Equals)Second comparison:\n{pMeter.Equals(pMili)}");
            Console.WriteLine($"Equality operator(\"==\"):\n{pMeter == pMili}\nInequality operator(\"!=\"):\n{pCenti != pMili}");
            // Implicit/Explicit operators
            var doubleArray = (double[])pCenti;
            Console.WriteLine("\nImplicit/Explicit operators:");
            Console.WriteLine($"Explicit conversion(Pudelko -> double[]):");
            Console.Write(string.Join(", ", doubleArray));
            var intTuple = new ValueTuple<int, int, int>(1,2,3);
            Pudelko p2 = intTuple;
            Console.WriteLine("\nImplicit conversion(ValueTuple<int, int, int -> Pudelko>):\n{0:MM}", p2);
            // Pudelko addition
            var addResult = pMeter + pCenti;
            Console.WriteLine("\nPudelko addition:");
            Console.WriteLine("First Pudelko - {0:M}\nSecond Pudelko - {1:CM}", pMeter, pCenti);
            Console.WriteLine("Addition result:\n{0:M}", addResult);
            // Indexer
            Console.WriteLine("Indexer:\nLenght: {0}, Width: {1}, Height: {2}", pMeter['l'], pMeter['w'], pMeter['H']);
            // Foreach
            Console.WriteLine("Foreach cycle:");
            foreach(var item in pMeter)
            {
                Console.Write("{0} ", item);
            }
            // Parse and TryParse
            Console.WriteLine($"\nParse:\n{pMeter == Pudelko.Parse("5.000 m \u00D7 5.000 m \u00D7 5.000 m")}");
            Console.WriteLine($"TryParse:\n{Pudelko.TryParse("30.0 cm \u00D7 21.0 cm \u00D7 100.0 cm", out Pudelko pOut)}");
            // Extension Method - Compress
            var pToCompress = new Pudelko(250, 50m, 50m, UnitOfMeasure.centimeter);
            Console.WriteLine("\nCompress Pudelko:");
            Console.WriteLine("Not Compressed Pudelko - {0:CM} (Volume = {1} m\u00B3)", pToCompress, pToCompress.Volume);
            Console.WriteLine("Compressed Pudelko - {0:CM} (Volume = {1} m\u00B3)", pToCompress.Compress(), pToCompress.Compress().Volume);
        }
    }
}
using System;
using System.Reflection.Metadata.Ecma335;
using System.IO;
using System.Text;
using Pudelko.Enums;
using System.Globalization;
using System.ComponentModel;

namespace Pudelko 
{
    public sealed class Pudelko : IEquatable<Pudelko>
    {
        private decimal Length { get; }
        private decimal Width { get; }
        private decimal Height { get; }
        private decimal Volume { get; }
        private decimal Square { get; }
        private UnitOfMeasure Unit { get; }
        public Pudelko(decimal length, decimal width, decimal height, UnitOfMeasure unit)
        {
            if (length < 0 || width < 0 || height < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (UnitConvertor(length, unit, UnitOfMeasure.meter) > 10 || UnitConvertor(width, unit, UnitOfMeasure.meter) > 10 || UnitConvertor(height, unit, UnitOfMeasure.meter) > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Length = length;
                Width = width;
                Height = height;
                Unit = unit;
                Square = Math.Round(2*Height*Width + 2*Width*Length+2*Height*Length, 6);
                Volume = Math.Round(UnitConvertor(Length, unit, UnitOfMeasure.meter)*UnitConvertor(Width, unit, UnitOfMeasure.meter)
                    *UnitConvertor(Height, unit, UnitOfMeasure.meter), 9);
            }
        }

        public Pudelko()
        {
            Length = 10;
            Width = 10;
            Height = 10;
            Unit = UnitOfMeasure.centimeter;
            Square = Math.Round(2 * Height * Width + 2 * Width * Length + 2 * Height * Length, 6);
            Volume = Math.Round(Length * Width * Height, 9);
        }
        private static decimal UnitConvertor(decimal value, UnitOfMeasure unit, UnitOfMeasure toUnit)
        {
            if(toUnit == UnitOfMeasure.meter && toUnit != unit) 
            {
                value = unit == UnitOfMeasure.centimeter ? value * 0.01m : value * 0.001m; 
            }
            else if (toUnit == UnitOfMeasure.centimeter && toUnit != unit) 
            {
                value = unit == UnitOfMeasure.meter ? value * 100 : value * 0.1m;
            }
            else if(toUnit == UnitOfMeasure.milimeter && toUnit != unit)
            {
                value = unit == UnitOfMeasure.centimeter ? value * 10 : value * 1000;
            }
            else if(toUnit == UnitOfMeasure.meterCube && unit != UnitOfMeasure.meter)
            {
                value = unit == UnitOfMeasure.centimeter ? value / 1000000 : value / 1000000000;
            }
            return value;
        }
       public string ToString(string format)
        {
            if(format ==  "m")
            {
                return ($"{Math.Round(UnitConvertor(Length, Unit, UnitOfMeasure.meter), 3)} «meter» " +
                    $"\u00D7 {Math.Round(UnitConvertor(Width, Unit, UnitOfMeasure.meter), 3)} «meter» " +
                    $"\u00D7 {Math.Round(UnitConvertor(Height, Unit, UnitOfMeasure.meter), 3)} «meter»");
            }
            else if (format == "cm") 
            {
                return ($"{Math.Round(UnitConvertor(Length, Unit, UnitOfMeasure.centimeter), 1)} «centimeter» " +
                    $"\u00D7 {Math.Round(UnitConvertor(Width, Unit, UnitOfMeasure.centimeter), 1)} «centimeter» " +
                    $"\u00D7 {Math.Round(UnitConvertor(Height, Unit, UnitOfMeasure.centimeter), 1)} «centimeter»");
            }
            else
            {
                return ($"{Math.Round(UnitConvertor(Length, Unit, UnitOfMeasure.milimeter), 0)} «milimeter» " +
                    $"\u00D7 {Math.Round(UnitConvertor(Width, Unit, UnitOfMeasure.milimeter), 0)} «milimeter» " +
                    $"\u00D7 {Math.Round(UnitConvertor(Height, Unit, UnitOfMeasure.milimeter), 0)} «milimeter»");
            }
        }
        public bool Equals(Pudelko? other)
        {
                try
                {
                    return this == other;
                }
                catch (NullReferenceException)
                {
                    return false;
                }
        }
        public override bool Equals(object? obj)
        {
            if(obj is Pudelko)
                return Equals((Pudelko)obj);
            return false;
        }
        public static bool operator ==(Pudelko? left, Pudelko? right)
        {
            if (left is null && right is null)
                return true;
            else if (left is null || right is null)
                return false;
            else 
            {
                return left.Volume == right.Volume;
            }
            
            return false;
        }
        public static bool operator !=(Pudelko left, Pudelko right) => !(left == right);
        public override int GetHashCode()
        {
            return Length.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
        }

        public override string ToString()
        {
            if (Unit == UnitOfMeasure.meter)
            {
                return ($"{ Math.Round(Length,3)} «{Unit}» \u00D7 {Math.Round(Width,3)} «{Unit}» \u00D7 {Math.Round(Height,3)} «{Unit}»");
            }
            else if (Unit == UnitOfMeasure.centimeter) 
            {
                return ($"{Math.Round(Length, 1)} «{Unit}» \u00D7 {Math.Round(Width, 1)} «{Unit}» \u00D7 {Math.Round(Height, 1)} «{Unit}»");
            }
            else if(Unit == UnitOfMeasure.milimeter)
            {
                return ($"{Math.Round(Length, 0)} «{Unit}» \u00D7 {Math.Round(Width, 0)} «{Unit}» \u00D7 {Math.Round(Height, 0)} «{Unit}»");
            }
            else
            {
                throw new FormatException();
            }
        }
    }
    
}
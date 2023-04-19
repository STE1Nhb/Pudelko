using System;
using System.Reflection.Metadata.Ecma335;
using System.IO;
using System.Text;
using Pudelko.Enums;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Pudelko 
{
    public sealed class Pudelko : IEquatable<Pudelko>, IEnumerable<decimal>
    {
        // Properties
        public decimal Length { get; }
        public decimal Width { get; }
        public decimal Height { get; }
        public decimal Volume { get; }
        public decimal Square { get; }
        public UnitOfMeasure Unit { get; }
        public List<decimal> Parameters = new List<decimal>();
        // Constructors
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
                Parameters.Add(Length);
                Parameters.Add(Width);
                Parameters.Add(Height);
            }
        }
        public Pudelko()
        {
            Length = 10;
            Width = 10;
            Height = 10;
            Unit = UnitOfMeasure.centimeter;
            Square = Math.Round(2 * Height * Width + 2 * Width * Length + 2 * Height * Length, 6);
            Volume = Volume = Math.Round(UnitConvertor(Length, Unit, UnitOfMeasure.meter) * UnitConvertor(Width, Unit, UnitOfMeasure.meter)
                    * UnitConvertor(Height, Unit, UnitOfMeasure.meter), 9);
        }

        // UnitConvertor
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
            else if(toUnit == UnitOfMeasure.meterSquare && toUnit != UnitOfMeasure.meter)
            {
                value = unit == UnitOfMeasure.centimeter ? value / 10000 : value / 1000000;
            }
            else if(toUnit == UnitOfMeasure.meterCube && unit != UnitOfMeasure.meter)
            {
                value = unit == UnitOfMeasure.centimeter ? value / 1000000 : value / 1000000000;
            }
            return value;
        }
        
        // Equals and GetHashCode()
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
        public override int GetHashCode()
        {
            return Length.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
        }

        // Operators
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
        }
        public static bool operator !=(Pudelko left, Pudelko right) => !(left == right);
        public static Pudelko operator +(Pudelko left, Pudelko right)
        {
            decimal x = UnitConvertor(left.Length, left.Unit, UnitOfMeasure.meter) > UnitConvertor(right.Length, right.Unit, UnitOfMeasure.meter) ? 
                UnitConvertor(left.Length, left.Unit, UnitOfMeasure.meter) : UnitConvertor(right.Length, right.Unit, UnitOfMeasure.meter);
            decimal y = UnitConvertor(left.Width,left.Unit, UnitOfMeasure.meter) + UnitConvertor(right.Width, right.Unit, UnitOfMeasure.meter);
            decimal z = UnitConvertor(left.Height, left.Unit, UnitOfMeasure.meter) > UnitConvertor(right.Height, right.Unit, UnitOfMeasure.meter) ? 
                UnitConvertor(left.Height, left.Unit, UnitOfMeasure.meter) : UnitConvertor(right.Height, right.Unit, UnitOfMeasure.meter);
            return new Pudelko(x, y, z, UnitOfMeasure.meter);    
        }
        public static explicit operator double[](Pudelko pudelko) => new double[] { (double)pudelko.Length, (double)pudelko.Width, (double)pudelko.Height };

        public static implicit operator Pudelko(ValueTuple<int, int, int> size) => new Pudelko(size.Item1, size.Item2, size.Item3, UnitOfMeasure.milimeter);


        // ToString()
        public string ToString(string format)
        {
            if (format == "m")
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

        // Indexer declaration

        public decimal this[char index]
        {
            get
            {
                decimal parameter = 0;
                if (index == 'l' || index == 'L')
                    parameter = this.Length;
                else if (index == 'w' || index == 'W')
                    parameter = this.Width;
                else if (index == 'h' || index == 'H')
                    parameter = this.Height;
                return parameter;
            }
        }

        // GetEnumerator Implementation

        public IEnumerator<decimal> GetEnumerator() => Parameters.GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        
    }
    
}
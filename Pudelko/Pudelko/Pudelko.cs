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
        public sealed class Pudelko : IEquatable<Pudelko>, IEnumerable<decimal>, IFormattable
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
                    Square = UnitConvertor(Math.Round(2 * ((Height * Width) + (Width * Length) + (Height * Length)), 2), Unit, UnitOfMeasure.meterSquare);
                    Volume = UnitConvertor(Math.Round(Length * Width * Height, 9), Unit, UnitOfMeasure.meterCube);

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
                Square = UnitConvertor(Math.Round(2 * ((Height * Width) +   (Width * Length) +   (Height * Length)), 2), Unit, UnitOfMeasure.meterSquare);
                Volume = UnitConvertor(Math.Round(Length * Width * Length, 9), Unit, UnitOfMeasure.meterCube);
            }

            // UnitConvertor
            // Converts different units to each other
            public static decimal UnitConvertor(decimal value, UnitOfMeasure unit, UnitOfMeasure toUnit)
            {
                if (toUnit == UnitOfMeasure.meter && toUnit != unit)
                {
                    value = unit == UnitOfMeasure.centimeter ? value * 0.01m : value * 0.001m;
                }
                else if (toUnit == UnitOfMeasure.centimeter && toUnit != unit)
                {
                    value = unit == UnitOfMeasure.meter ? value * 100 : value * 0.1m;
                }
                else if (toUnit == UnitOfMeasure.milimeter && toUnit != unit)
                {
                    value = unit == UnitOfMeasure.centimeter ? value * 10 : value * 1000;
                }
                else if (toUnit == UnitOfMeasure.meterSquare && unit != UnitOfMeasure.meter)
                {
                    value = unit == UnitOfMeasure.centimeter ? value / 10000 : value / 1000000;
                }
                else if (toUnit == UnitOfMeasure.meterCube && unit != UnitOfMeasure.meter)
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
                if (obj is Pudelko)
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
                decimal y = UnitConvertor(left.Width, left.Unit, UnitOfMeasure.meter) + UnitConvertor(right.Width, right.Unit, UnitOfMeasure.meter);
                decimal z = UnitConvertor(left.Height, left.Unit, UnitOfMeasure.meter) > UnitConvertor(right.Height, right.Unit, UnitOfMeasure.meter) ?
                    UnitConvertor(left.Height, left.Unit, UnitOfMeasure.meter) : UnitConvertor(right.Height, right.Unit, UnitOfMeasure.meter);
                return new Pudelko(x, y, z, UnitOfMeasure.meter);
            }
            public static explicit operator double[](Pudelko pudelko) => new double[] { (double)pudelko.Length, (double)pudelko.Width, (double)pudelko.Height };

            public static implicit operator Pudelko(ValueTuple<int, int, int> size) => new Pudelko(size.Item1, size.Item2, size.Item3, UnitOfMeasure.milimeter);


            // ToString()
            public string ToString(string format)
            {
                return ToString(format, CultureInfo.CurrentCulture);
            }
            public string ToString(string? format, IFormatProvider? provider)
            {
                if (string.IsNullOrEmpty(format))
                    format = "G";
                if (provider is null)
                    provider = CultureInfo.CurrentCulture;

                switch (format.ToUpperInvariant())
                {
                    case "G":
                    case "M":
                        return UnitConvertor(Length, Unit, UnitOfMeasure.meter).ToString("F3", provider) + " «meter»" +
                        " \u00D7 " + UnitConvertor(Width, Unit, UnitOfMeasure.meter).ToString("F3", provider) + " «meter»" +
                        " \u00D7 " + UnitConvertor(Height, Unit, UnitOfMeasure.meter).ToString("F3", provider) + " «meter»";
                    case "CM":
                        return UnitConvertor(Length, Unit, UnitOfMeasure.centimeter).ToString("F1", provider) + " «centimeter»" +
                       " \u00D7 " + UnitConvertor(Width, Unit, UnitOfMeasure.centimeter).ToString("F1", provider) + " «centimeter»" +
                       " \u00D7 " + UnitConvertor(Height, Unit, UnitOfMeasure.centimeter).ToString("F1", provider) + " «centimeter»";
                    case "MM":
                        return UnitConvertor(Length, Unit, UnitOfMeasure.milimeter).ToString("F0", provider) + " «milimeter»" +
                       " \u00D7 " + UnitConvertor(Width, Unit, UnitOfMeasure.milimeter).ToString("F0", provider) + " «milimeter»" +
                       " \u00D7 " + UnitConvertor(Height, Unit, UnitOfMeasure.milimeter).ToString("F0", provider) + " «milimeter»";
                    default:
                        throw new FormatException();
                }
            }
            public override string ToString()
            {
                return ToString("G", CultureInfo.CurrentCulture);
            }

            // Indexer declaration

            public decimal this[char index]
            {
                get
                {
                    decimal parameter = 0;
                    if (index == 'l' || index == 'L')
                        parameter = Length;
                    else if (index == 'w' || index == 'W')
                        parameter = Width;
                    else if (index == 'h' || index == 'H')
                        parameter = Height;
                    return parameter;
                }
            }

            // GetEnumerator Implementation

            public IEnumerator<decimal> GetEnumerator() => Parameters.GetEnumerator();

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

            public static string[] RemoveExtraChars(string s, params char[] extraChars)
            {
                return s.Split(extraChars, StringSplitOptions.RemoveEmptyEntries);
            }
            public static Pudelko Parse(string s)
            {
                int j = 0;

                if (s is null || s.Length == 0)
                    throw new ArgumentNullException();
            else if (s.Where(x => "cm".IndexOf(x) != -1).Count() == 6 )
            {
                string[] parametersStr = RemoveExtraChars(s, 'c', 'm', '\u00D7', ' ');

                foreach (string element in parametersStr)
                {
                    if(!element.Contains('.'))
                        throw new FormatException("Pudelko parameters wasn't in a right format!");

                    j = element.IndexOf('.');

                    if (Convert.ToDecimal(element) > 1000)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                    else if (j == 1 && element.Length > 3)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                    else if (j == 2 && element.Length > 4)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                    else if (j == 3 && element.Length > 5)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                    else if (j == 4 && element.Length > 5)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                    else if (j == 5 && element.Length > 6)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                }

                decimal[] parameter = new decimal[parametersStr.Length];
                for(int i = 0; i < parameter.Length; i++)
                {
                    parameter[i] = Convert.ToDecimal(parametersStr[i]);
                }
                return new Pudelko(parameter[0], parameter[1], parameter[2], UnitOfMeasure.centimeter);
            }
            else if (s.Where(x => "mm".IndexOf(x) != -1).Count() == 6)
            {
                string[] parametersStr = RemoveExtraChars(s, 'm', '\u00D7', ' ');

                foreach (string element in parametersStr)
                {
                    if (element.Contains('.') || Convert.ToDecimal(element) > 10000)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                }

                decimal[] parameter = new decimal[parametersStr.Length];
                for(int i = 0; i < parameter.Length; i++)
                {
                    parameter[i] = Convert.ToDecimal(parametersStr[i]);
                }
                return new Pudelko(parameter[0], parameter[1], parameter[2], UnitOfMeasure.milimeter);
            }
            else if (s.Where(x => "m".IndexOf(x) != -1).Count() == 3)
            {

                string[] parametersStr = RemoveExtraChars(s, 'm', '\u00D7', ' ');

                foreach (string element in parametersStr)
                {
                    if (!element.Contains('.'))
                        throw new FormatException("Pudelko parameters wasn't in a right format!");

                    j = element.IndexOf('.');
                    
                    if (Convert.ToDecimal(element) > 10)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                    else if (j == 1 && element.Length > 5)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                    else if (j == 2 && element.Length > 6)
                        throw new FormatException("Pudelko parameters wasn't in a right format!");
                }

                decimal[] parameter = new decimal[parametersStr.Length];
                for(int i = 0; i < parameter.Length; i++)
                {
                    parameter[i] = Convert.ToDecimal(parametersStr[i]);
                }

                return new Pudelko(parameter[0], parameter[1], parameter[2], UnitOfMeasure.meter);
            }
            else
                throw new ArgumentException();
            }
            public static bool TryParse(string s, out Pudelko pudelko)
            {
                if (string.IsNullOrEmpty(s))
                {
                    pudelko = new Pudelko();
                    return false;
                }
                else
                {
                    pudelko = Parse(s);
                    return true;
                }
            }
        }

    }

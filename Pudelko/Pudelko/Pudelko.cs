using System;
using Pudelko.Enums;

namespace Pudelko.NewFolder
{
    public sealed class Pudelko
    {
        private decimal Length { get; }
        private decimal Width { get; }
        private decimal Height { get; }
        private decimal Volume { get; }
        private UnitOfMeasure Unit { get; }
        public Pudelko(decimal length, decimal width, decimal height, UnitOfMeasure unit, decimal volume)
        {
            if (length < 0 || width < 0 || height < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else if (unit == UnitOfMeasure.meter && length > 10 || width > 10 || height > 10)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Length = length;
                Width = width;
                Height = height;
                Unit = unit;
                Volume = Math.Round(volume, 9);
            }
        }

        public Pudelko()
        {
            Length = 10;
            Width = 10;
            Height = 10;
            Volume = Length * Width * Height;
            Unit = UnitOfMeasure.centimeter;
        }
    }
}
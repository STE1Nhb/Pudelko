using P = Pudelko;
using Pudelko.Enums;
using System;

namespace PudelkoUnitTests
{
    [TestClass]
    public class PudelkoTests
    {
        [DataTestMethod]
        [DataRow(2.500, 2.500, 2.500, UnitOfMeasure.meter, 15.625, 37.50)]
        [DataRow(145.22, 14.79, 60, UnitOfMeasure.centimeter, 0.128868228, 2.349681)]
        [DataRow(500, 234, 14, UnitOfMeasure.milimeter, 0.001638, 0.254552)]
        public void Volume_And_Square_Properties_Test(double length, double width, double height, UnitOfMeasure unit, double a, double b)
        {
            P.Pudelko x = new P.Pudelko((decimal)length, (decimal)width, (decimal)height, unit);

            Assert.AreEqual((decimal)a, x.Volume);
            Assert.AreEqual((decimal)b, x.Square);

        }
        [DataTestMethod]
        [DataRow(5,2.5,3,UnitOfMeasure.meter, 150, 80, 600, UnitOfMeasure.centimeter, 5, 3.3, 6, UnitOfMeasure.meter)]
        [DataRow(3750.895, 251.43, 1199.19, UnitOfMeasure.milimeter, 300, 9, 50.33, UnitOfMeasure.centimeter, 3.750895, 0.34143, 1.19919, UnitOfMeasure.meter)]
        [DataRow(50,5,23.56, UnitOfMeasure.milimeter, 0.0456, 0.003, 0.01, UnitOfMeasure.meter, 0.05, 0.008, 0.02356, UnitOfMeasure.meter)]
        public void Join_Operator_Test(
            double lengthFirst, double widthFirst, double heightFirst, UnitOfMeasure unitFirst, 
            double lengthSecond, double widthSecond, double heightSecond, UnitOfMeasure unitSecond, 
            double lengthResult, double widthResult, double heightResult, UnitOfMeasure unitResult)
        {
            P.Pudelko first = new P.Pudelko((decimal)lengthFirst, (decimal)widthFirst, (decimal)heightFirst, unitFirst);
            P.Pudelko second = new P.Pudelko((decimal)lengthSecond, (decimal)widthSecond, (decimal)heightSecond, unitSecond);
            P.Pudelko result = new P.Pudelko((decimal)lengthResult, (decimal)widthResult, (decimal)heightResult, unitResult);

            Assert.AreEqual(first+second, result);
        }

        [DataTestMethod]
        [DataRow(4.23, 0.4789, 2, UnitOfMeasure.meter, 423, 47.89, 200, UnitOfMeasure.centimeter, true)]
        [DataRow(4.23, 0.4789, 2, UnitOfMeasure.meter, 422, 47.89, 200, UnitOfMeasure.centimeter, false)]
        [DataRow(4.23, 0.4789, 2, UnitOfMeasure.centimeter, 42.3, 4.789, 20, UnitOfMeasure.milimeter, true)]
        [DataRow(6.256554, 2.58825, 7.8, UnitOfMeasure.meter, 6.256553, 2.58826, 7.79,UnitOfMeasure.meter, false)]
        public void Equality_Operator_Test(double lengthFirst, double widthFirst, double heightFirst, UnitOfMeasure unitFirst,
            double lengthSecond, double widthSecond, double heightSecond, UnitOfMeasure unitSecond, bool result)
        {
            P.Pudelko first = new P.Pudelko((decimal)lengthFirst, (decimal)widthFirst, (decimal)heightFirst, unitFirst);
            P.Pudelko second = new P.Pudelko((decimal)lengthSecond, (decimal)widthSecond, (decimal)heightSecond, unitSecond);

            Assert.AreEqual(first == second, result);
        }
    }
}
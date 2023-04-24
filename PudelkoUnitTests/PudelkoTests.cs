using Pudelko;
using Pudelko.Enums;

namespace PudelkoUnitTests
{
    [TestClass]
    public class PudelkoTests
    {
        [DataTestMethod]
        [DataRow(2.500, 2.500, 2.500, UnitOfMeasure.meter, 15.625, 37.50)]
        [DataRow(145.22, 14.79, 60, UnitOfMeasure.centimeter, 0.128868228, 2.349681)]
        [DataRow(500, 234, 14, UnitOfMeasure.milimeter, 1, 1)]
        public void Volume_And_Square_Properties_Test(double lenght, double width, double height, UnitOfMeasure unit, double a, double b)
        {
            Pudelko.Pudelko x = new Pudelko.Pudelko((decimal)lenght, (decimal)width, (decimal)height, unit);

            Assert.AreEqual((decimal)a, x.Volume);
            Assert.AreEqual((decimal)b, x.Square);

        }
    }
}
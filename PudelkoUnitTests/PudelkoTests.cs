using P = Pudelko;
using Pudelko.Enums;
using System;
using System.Globalization;

namespace PudelkoUnitTests
{
    [TestClass]
    public class PudelkoTests
    {
        #region PropertiesTest
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
        [TestMethod]
        #endregion

        #region PudelkoAdditionTest
        [DataTestMethod]
        [DataRow(5, 2.5, 3, UnitOfMeasure.meter, 150, 80, 600, UnitOfMeasure.centimeter, 5, 3.3, 6, UnitOfMeasure.meter)]
        [DataRow(3750.895, 251.43, 1199.19, UnitOfMeasure.milimeter, 300, 9, 50.33, UnitOfMeasure.centimeter, 3.750895, 0.34143, 1.19919, UnitOfMeasure.meter)]
        [DataRow(50, 5, 23.56, UnitOfMeasure.milimeter, 0.0456, 0.003, 0.01, UnitOfMeasure.meter, 0.05, 0.008, 0.02356, UnitOfMeasure.meter)]
        public void Pudelko_Addition_Test(
            double lengthFirst, double widthFirst, double heightFirst, UnitOfMeasure unitFirst,
            double lengthSecond, double widthSecond, double heightSecond, UnitOfMeasure unitSecond,
            double lengthResult, double widthResult, double heightResult, UnitOfMeasure unitResult)
        {
            P.Pudelko first = new P.Pudelko((decimal)lengthFirst, (decimal)widthFirst, (decimal)heightFirst, unitFirst);
            P.Pudelko second = new P.Pudelko((decimal)lengthSecond, (decimal)widthSecond, (decimal)heightSecond, unitSecond);
            P.Pudelko result = new P.Pudelko((decimal)lengthResult, (decimal)widthResult, (decimal)heightResult, unitResult);

            Assert.AreEqual(first + second, result);
        }
        #endregion

        #region PudelkoEqualityTest
        [DataTestMethod]
        [DataRow(4.23, 0.4789, 2, UnitOfMeasure.meter, 423, 47.89, 200, UnitOfMeasure.centimeter, true)]
        [DataRow(4.23, 0.4789, 2, UnitOfMeasure.meter, 422, 47.89, 200, UnitOfMeasure.centimeter, false)]
        [DataRow(4.23, 0.4789, 2, UnitOfMeasure.centimeter, 42.3, 4.789, 20, UnitOfMeasure.milimeter, true)]
        [DataRow(6.256554, 2.58825, 7.8, UnitOfMeasure.meter, 6.256553, 2.58826, 7.79, UnitOfMeasure.meter, false)]
        public void Equality_Operator_Test(double lengthFirst, double widthFirst, double heightFirst, UnitOfMeasure unitFirst,
            double lengthSecond, double widthSecond, double heightSecond, UnitOfMeasure unitSecond, bool result)
        {
            P.Pudelko first = new P.Pudelko((decimal)lengthFirst, (decimal)widthFirst, (decimal)heightFirst, unitFirst);
            P.Pudelko second = new P.Pudelko((decimal)lengthSecond, (decimal)widthSecond, (decimal)heightSecond, unitSecond);

            Assert.AreEqual(first == second, result);
        }
        #endregion

        [TestClass]
        public static class InitializeCulture
        {
            [AssemblyInitialize]
            public static void SetEnglishCultureOnAllUnitTest(TestContext context)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            }
        }

        // ========================================

        [TestClass]
        public class UnitTestsPudelkoConstructors
        {
            private static double defaultSize = 10; // w centymetrach
            private static double accuracy = 0.001; //dokładność 3 miejsca po przecinku

            private void AssertPudelko(P.Pudelko p, double expectedA, double expectedB, double expectedC)
            {
                Assert.AreEqual(expectedA, (float)p.Length, delta: accuracy);
                Assert.AreEqual(expectedB, (float)p.Width, delta: accuracy);
                Assert.AreEqual(expectedC, (float)p.Height, delta: accuracy);
            }

            #region Constructor tests ================================

            [TestMethod, TestCategory("Constructors")]
            public void Constructor_Default()
            {
                P.Pudelko p = new P.Pudelko();

                Assert.AreEqual(defaultSize, (float)p.Length, delta: accuracy);
                Assert.AreEqual(defaultSize, (float)p.Width, delta: accuracy);
                Assert.AreEqual(defaultSize, (float)p.Height, delta: accuracy);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(1.0, 2.543, 3.1,
                     1.0, 2.543, 3.1)]
            [DataRow(1.0001, 2.54387, 3.1005,
                     1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
            public void Constructor_3params_DefaultMeters(double a, double b, double c,
                                                          double expectedA, double expectedB, double expectedC)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, (decimal)c);

                AssertPudelko(p, expectedA, expectedB, expectedC);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(1.0, 2.543, 3.1,
                     1.0, 2.543, 3.1)]
            [DataRow(1.0001, 2.54387, 3.1005,
                     1.0, 2.543, 3.1)] // dla metrów liczą się 3 miejsca po przecinku
            public void Constructor_3params_InMeters(double a, double b, double c,
                                                          double expectedA, double expectedB, double expectedC)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, (decimal)c, unit: UnitOfMeasure.meter);

                AssertPudelko(p, expectedA, expectedB, expectedC);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(100.0, 25.5, 3.1,
                     100.0, 25.5, 3.1)]
            [DataRow(100.0, 25.58, 3.13,
                     100.0, 25.58, 3.13)] // dla centymertów liczy się tylko 1 miejsce po przecinku
            public void Constructor_3params_InCentimeters(double a, double b, double c,
                                                          double expectedA, double expectedB, double expectedC)
            {
                P.Pudelko p = new P.Pudelko(length: (decimal)a, width: (decimal)b, height: (decimal)c, unit: UnitOfMeasure.centimeter);

                AssertPudelko(p, expectedA, expectedB, expectedC);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(100, 255, 3,
                     100, 255, 3)]
            [DataRow(100.0, 25.58, 3.13,
                     100.0, 25.58, 3.13)] // dla milimetrów nie liczą się miejsca po przecinku
            public void Constructor_3params_InMilimeters(double a, double b, double c,
                                                         double expectedA, double expectedB, double expectedC)
            {
                P.Pudelko p = new P.Pudelko(unit: UnitOfMeasure.milimeter, length: (decimal)a, width: (decimal)b, height: (decimal)c);

                AssertPudelko(p, expectedA, expectedB, expectedC);
            }


            // ----

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(1.0, 2.5, 1.0, 2.5)]
            [DataRow(1.001, 2.599, 1.001, 2.599)]
            [DataRow(1.0019, 2.5999, 1.001, 2.599)]
            public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b);

                AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(1.0, 2.5, 1.0, 2.5)]
            [DataRow(1.001, 2.599, 1.001, 2.599)]
            [DataRow(1.0019, 2.5999, 1.001, 2.599)]
            public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
            {
                P.Pudelko p = new P.Pudelko(length: (decimal)a, width: (decimal)b, unit: UnitOfMeasure.meter);

                AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(11.0, 2.5, 11.0, 2.5)]
            [DataRow(100.1, 2.599, 100.1, 2.599)]
            [DataRow(2.0019, 0.25999, 2.0019, 0.25999)]
            public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
            {
                P.Pudelko p = new P.Pudelko(unit: UnitOfMeasure.centimeter, length: (decimal)a, width: (decimal)b);

                AssertPudelko(p, expectedA, expectedB, expectedC: 10);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(11, 2.0, 11, 2.0)]
            [DataRow(100.1, 2599, 100.1, 2599)]
            [DataRow(200.19, 2.5999, 200.19, 2.5999)]
            public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
            {
                P.Pudelko p = new P.Pudelko(unit: UnitOfMeasure.milimeter, length: (decimal)a, width: (decimal)b);

                AssertPudelko(p, expectedA, expectedB, expectedC: 100);
            }

            // -------

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(2.5)]
            public void Constructor_1param_DefaultMeters(double a)
            {
                P.Pudelko p = new P.Pudelko((decimal)a);

                Assert.AreEqual((decimal)a, p.Length);
                Assert.AreEqual(0.1m, p.Width);
                Assert.AreEqual(0.1m, p.Height);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(2.5)]
            public void Constructor_1param_InMeters(double a)
            {
                P.Pudelko p = new P.Pudelko((decimal)a);

                Assert.AreEqual((decimal)a, p.Length);
                Assert.AreEqual(0.1m, p.Width);
                Assert.AreEqual(0.1m, p.Height);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(11, 11)]
            [DataRow(100.1, 100.1)]
            [DataRow(2.0019, 2.0019)]
            public void Constructor_1param_InCentimeters(double a, double expectedA)
            {
                P.Pudelko p = new P.Pudelko(unit: UnitOfMeasure.centimeter, length: (decimal)a);

                AssertPudelko(p, expectedA, expectedB: 10, expectedC: 10);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(11, 11)]
            [DataRow(100.1, 100.1)]
            [DataRow(200.19, 200.19)]
            public void Constructor_1param_InMilimeters(double a, double expectedA)
            {
                P.Pudelko p = new P.Pudelko(unit: UnitOfMeasure.milimeter, length: (decimal)a);

                AssertPudelko(p, expectedA, expectedB: 100, expectedC: 100);
            }

            // ---

            public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {1000.1, 2.5, 3.1},
            new object[] {10, 1000.1, 3.1},
            new object[] {10, 10, 1000.1},
            new object[] {1000.1, 1000.1, 3.1},
            new object[] {1000.1, 10, 1000.1},
            new object[] {10, 1000.1, 1000.1},
            new object[] {1000.1, 1000.1, 1000.1}
        };

            [DataTestMethod, TestCategory("Constructors")]
            [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, (decimal)c);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, (decimal)c, unit: UnitOfMeasure.meter);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-1, 1, 1)]
            [DataRow(1, -1, 1)]
            [DataRow(1, 1, -1)]
            [DataRow(-1, -1, 1)]
            [DataRow(-1, 1, -1)]
            [DataRow(1, -1, -1)]
            [DataRow(-1, -1, -1)]
            [DataRow(0, 1, 1)]
            [DataRow(1, 0, 1)]
            [DataRow(1, 1, 0)]
            [DataRow(0, 0, 1)]
            [DataRow(0, 1, 0)]
            [DataRow(1, 0, 0)]
            [DataRow(0, 0, 0)]
            [DataRow(0.01, 0.1, 1)]
            [DataRow(0.1, 0.01, 1)]
            [DataRow(0.1, 0.1, 0.01)]
            [DataRow(1001, 1, 1)]
            [DataRow(1, 1001, 1)]
            [DataRow(1, 1, 1001)]
            [DataRow(1001, 1, 1001)]
            [DataRow(1, 1001, 1001)]
            [DataRow(1001, 1001, 1)]
            [DataRow(1001, 1001, 1001)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, (decimal)c, unit: UnitOfMeasure.centimeter);
            }


            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-1, 1, 1)]
            [DataRow(1, -1, 1)]
            [DataRow(1, 1, -1)]
            [DataRow(-1, -1, 1)]
            [DataRow(-1, 1, -1)]
            [DataRow(1, -1, -1)]
            [DataRow(-1, -1, -1)]
            [DataRow(0, 1, 1)]
            [DataRow(1, 0, 1)]
            [DataRow(1, 1, 0)]
            [DataRow(0, 0, 1)]
            [DataRow(0, 1, 0)]
            [DataRow(1, 0, 0)]
            [DataRow(0, 0, 0)]
            [DataRow(0.1, 1, 1)]
            [DataRow(1, 0.1, 1)]
            [DataRow(1, 1, 0.1)]
            [DataRow(10001, 1, 1)]
            [DataRow(1, 10001, 1)]
            [DataRow(1, 1, 10001)]
            [DataRow(10001, 10001, 1)]
            [DataRow(10001, 1, 10001)]
            [DataRow(1, 10001, 10001)]
            [DataRow(10001, 10001, 10001)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, (decimal)c, unit: UnitOfMeasure.milimeter);
            }


            public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {1000.1, 1000},
            new object[] {1000, 1000.1},
            new object[] {1000.1, 1000.1}
        };

            [DataTestMethod, TestCategory("Constructors")]
            [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, unit: UnitOfMeasure.meter);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-1, 1)]
            [DataRow(1, -1)]
            [DataRow(-1, -1)]
            [DataRow(0, 1)]
            [DataRow(1, 0)]
            [DataRow(0, 0)]
            [DataRow(1001, 1)]
            [DataRow(1, 1001)]
            [DataRow(1001, 1001)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, unit: UnitOfMeasure.centimeter);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-1, 1)]
            [DataRow(1, -1)]
            [DataRow(-1, -1)]
            [DataRow(0, 1)]
            [DataRow(1, 0)]
            [DataRow(0, 0)]
            [DataRow(10001, 1)]
            [DataRow(1, 10001)]
            [DataRow(10001, 10001)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, (decimal)b, unit: UnitOfMeasure.milimeter);
            }




            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-1.0)]
            [DataRow(0)]
            [DataRow(10.1)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, unit: UnitOfMeasure.meter);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-1.0)]
            [DataRow(0)]
            [DataRow(10.1)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, unit: UnitOfMeasure.meter);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-1.0)]
            [DataRow(0)]
            [DataRow(1001)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, unit: UnitOfMeasure.centimeter);
            }

            [DataTestMethod, TestCategory("Constructors")]
            [DataRow(-1)]
            [DataRow(0)]
            [DataRow(10001)]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
            {
                P.Pudelko p = new P.Pudelko((decimal)a, unit: UnitOfMeasure.milimeter);
            }

            #endregion


            #region ToString tests ===================================

            [TestMethod, TestCategory("String representation")]
            public void ToString_Default_Culture_EN()
            {
                var p = new P.Pudelko(2.5m, 9.321m);
                string expectedStringEN = "2.500 «meter» × 9.321 «meter» × 0.100 «meter»";

                Assert.AreEqual(expectedStringEN, p.ToString());
            }

            [DataTestMethod, TestCategory("String representation")]
            [DataRow(null, 2.5, 9.321, 0.1, "2.500 «meter» × 9.321 «meter» × 0.100 «meter»")]
            [DataRow("m", 2.5, 9.321, 0.1, "2.500 «meter» × 9.321 «meter» × 0.100 «meter»")]
            [DataRow("cm", 2.5, 9.321, 0.1, "250.0 «centimeter» × 932.1 «centimeter» × 10.0 «centimeter»")]
            [DataRow("mm", 2.5, 9.321, 0.1, "2500 «milimeter» × 9321 «milimeter» × 100 «milimeter»")]
            public void ToString_Formattable_Culture_EN(string format, double a, double b, double c, string expectedStringRepresentation)
            {
                var p = new P.Pudelko((decimal)a, (decimal)b, (decimal)c, unit: UnitOfMeasure.meter);
                Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
            }

            [TestMethod, TestCategory("String representation")]
            [ExpectedException(typeof(FormatException))]
            public void ToString_Formattable_WrongFormat_FormatException()
            {
                var p = new P.Pudelko(1);
                var stringformatedrepreentation = p.ToString("wrong code");
            }

            #endregion


            #region Pole, Objętość ===================================
            // ToDo

            #endregion

            #region Equals ===========================================
            // ToDo
            #endregion

            #region Operators overloading ===========================
            // ToDo
            #endregion

            #region Conversions =====================================
            [TestMethod]
            public void ExplicitConversion_ToDoubleArray_AsMeters()
            {
                var p = new P.Pudelko(1, 2.1m, 3.231m, UnitOfMeasure.meter);
                double[] tab = (double[])p;
                Assert.AreEqual(3, tab.Length);
                Assert.AreEqual((double)p.Length, tab[0]);
                Assert.AreEqual((double)p.Width, tab[1]);
                Assert.AreEqual((double)p.Height, tab[2]);
            }

            [TestMethod]
            public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters()
            {
                var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
                P.Pudelko p = (a, b, c);
                Assert.AreEqual((int)(p.Length), a);
                Assert.AreEqual((int)(p.Width), b);
                Assert.AreEqual((int)(p.Height), c);
            }

            #endregion

            #region Indexer, enumeration ============================
            [TestMethod]
            public void Indexer_ReadFrom()
            {
                var p = new P.Pudelko(1, 2.1m, 3.231m, UnitOfMeasure.meter);
                Assert.AreEqual(p.Length, p[0]);
                Assert.AreEqual(p.Width, p[1]);
                Assert.AreEqual(p.Height, p[2]);
            }

            [TestMethod]
            public void ForEach_Test()
            {
                var p = new P.Pudelko(1, 2.1m, 3.231m, UnitOfMeasure.meter);
                var tab = new[] { p.Length, p.Width, p.Height };
                int i = 0;
                foreach (var x in p)
                {
                    Assert.AreEqual((decimal)x, tab[i]);
                    i++;
                }
            }

            #endregion

            #region Parsing =========================================

            #endregion


        }
    }
}
using System;
using GeographicLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeographicLibraryTest
{
    [TestClass]
    public class UnitTestPoint
    {
        [TestMethod]
        [DataRow(-1000, 1000)]
        [DataRow(1000, 1000)]
        [DataRow(91, 179)]
        [DataRow(-91, 179)]
        [DataRow(89, 181)]
        [DataRow(-89, -181)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestMethodCtor_ThrowsArgumentOutOfRangeException(double a, double b)
        {
            Point p;

            p = new Point(a, b);
        }

        [TestMethod]
        [DataRow(89, 179)]
        [DataRow(-89.999999999999999999999999999999, 179)]
        [DataRow(89, 179.9999999999999999999999999)]
        [DataRow(-89, -179.9999999999999999999999999)]
        [DataRow(0.0, 179.999999)]
        [DataRow(-89.53465489347548957894, 179.48902378947238472389479823)]
        [DataRow(89, 178)]  
        [DataRow(-89.6048923842389948923753452347, 0)]
        public void TestMethodCtor_CteatePoint(double a, double b)
        {
            Point p;

            p = new Point(a, b);

            Assert.IsNotNull(p);
        }
    }
}

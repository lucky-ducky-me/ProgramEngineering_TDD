using System;
using GeographicLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GeographicLibraryTest
{
    [TestClass]
    public class UnitTestPoint
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestMethodCtor_ThrowsArgumentOutOfRangeException()
        {
            Point p;

            double a = -1000.0;
            double b = -1000.0;

            p = new Point(a, b);
        }

        [TestMethod]
        public void TestMethodCtor_CteatePoin()
        {
            Point p;

            double a = 0.0;
            double b = 0.0;

            p = new Point(a, b);

            Assert.IsNotNull(p);
        }
    }
}

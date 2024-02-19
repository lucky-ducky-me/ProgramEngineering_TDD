using GeographicLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GeographicLibraryTest
{
    [TestClass]
    public class AzimutTests
    {
        [TestMethod]
        [DynamicData(nameof(GetPoints), DynamicDataSourceType.Method)]
        public void Test_CreateArc_CreatedArcNotNull(Point a, Point b)
        {
            Arc arc = new Arc(a, b);

            Assert.IsNotNull(arc);
        }

        public static IEnumerable<object[]> GetPoints()
        {
            yield return new object[] { new Point(0.0, 180.0), new Point(0.0, 90.0) };
            yield return new object[] { new Point(0.0, -90.0), new Point(0.0, 90.0) };
        }
    }
}

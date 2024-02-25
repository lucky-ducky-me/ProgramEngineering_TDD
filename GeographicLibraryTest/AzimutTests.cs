using GeographicLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GeographicLibraryTest
{
    [TestClass]
    public class AzimutTests
    {
	    private const double Epsilon = 0.0001d;

        [TestMethod]
        [DynamicData(nameof(GetPoints), DynamicDataSourceType.Method)]
        public void Test_CreateArc_CreatedArcNotNull(Point a, Point b)
        {
            Arc arc = new Arc(a, b);

            Assert.IsNotNull(arc);
        }

        public static IEnumerable<object[]> GetPoints()
        {
            yield return new object[] { new Point(0.0, 90.0), new Point(0.0, 180.0)};
        }

        public static IEnumerable<object[]> GetPointsForCalculating()
        {
	        yield return new object[] { new Point(77.1539, -139.398), new Point(-77.1804, -139.55), 180.077867811 };
	        yield return new object[] { new Point(0, 90), new Point(0, 180), 90.0 };
	        yield return new object[] { new Point(77.1539, 120.398), new Point(77.1804, 129.55), 84.7925159033 };
	        yield return new object[] { new Point(77.1539, -120.398), new Point(77.1804, 129.55), 324.384112704 };
	        yield return new object[] { new Point(90, 0), new Point(-90, 0), 180.0 };
		}


		[TestMethod]
        [DynamicData(nameof(GetPointsForCalculating), DynamicDataSourceType.Method)]
        public void Test_ValidData_AzimutCalculated(Point a, Point b, double ans)
        {
            Arc arc = new Arc(a, b);
            GeoInfo az = arc.GetAzimut();
            Assert.IsTrue(Math.Abs(ans - az.AzimutValue) < Epsilon);
        }

        public static IEnumerable<object[]> GetSamePoints()
        {
	        yield return new object[] { new Point(77.1539, -139.398), new Point(77.1539, -139.398)};
	        yield return new object[] { new Point(0, 90), new Point(0, 90) };
	        yield return new object[] { new Point(77.1539, 120.398), new Point(77.1539, 120.398) };
        }

        [TestMethod]
		[DynamicData(nameof(GetSamePoints), DynamicDataSourceType.Method)]
		public void Test_SamePoints_AzimutNone(Point a, Point b)
        {
	        Arc arc = new Arc(a, b);
	        GeoInfo az = arc.GetAzimut();
	        Assert.IsTrue(az.AzimutStatus == AzimutStatus.None);
        }

		public static IEnumerable<object[]> GetPointsTwoOnPolar()
		{
			yield return new object[] { new Point(90, 90), new Point(90, 180)};
			yield return new object[] { new Point(-90, 0), new Point(-90, 10) };
		}

		[TestMethod]
		[DynamicData(nameof(GetPointsTwoOnPolar), DynamicDataSourceType.Method)]
		public void Test_PointsTwoOnPolar_AzimutNone(Point a, Point b)
		{
			Arc arc = new Arc(a, b);
			GeoInfo az = arc.GetAzimut();
			Assert.IsTrue(az.AzimutStatus == AzimutStatus.None);
		}
	}
}

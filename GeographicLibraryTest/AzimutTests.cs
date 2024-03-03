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
			yield return new object[] { new Point(0, 90), new Point(10, 90), 0.0 };
			yield return new object[] { new Point(10, 90), new Point(0, 90), 180.0 };
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

		public static IEnumerable<object[]> GetPointsOneOnPolar()
		{
			yield return new object[] { new Point(90, 90), new Point(70, 180) };
			yield return new object[] { new Point(-90, 0), new Point(-70, 10) };
			yield return new object[] { new Point(70, 180), new Point(90, 90) };
			yield return new object[] { new Point(-70, 10), new Point(-90, 0) };
		}

		[TestMethod]
		[DynamicData(nameof(GetPointsOneOnPolar), DynamicDataSourceType.Method)]
		public void Test_PointsOneOnPolar_AzimutNone(Point a, Point b)
		{
			var ans = 180;
			Arc arc = new Arc(a, b);
			GeoInfo az = arc.GetAzimut();
			Assert.IsTrue(Math.Abs(ans - az.AzimutValue) < Epsilon);
		}

		public static IEnumerable<object[]> GetPointsOppositeOnPolar()
		{
			yield return new object[] { new Point(90, 90), new Point(-90, 180) };
			yield return new object[] { new Point(-90, 0), new Point(90, 10) };
		}

		[TestMethod]
		[DynamicData(nameof(GetPointsOppositeOnPolar), DynamicDataSourceType.Method)]
		public void Test_PointsOppositeOnPolar_AzimutNone(Point a, Point b)
		{
			Arc arc = new Arc(a, b);
			GeoInfo az = arc.GetAzimut();
			Assert.IsTrue(az.AzimutStatus == AzimutStatus.Any);
		}

		public static IEnumerable<object[]> GetPointsForCalculatingDistanse()
		{
			yield return new object[] { new Point(77.1539, -139.398), new Point(-77.1804, -139.55), 17166029 };
			yield return new object[] { new Point(77.1539, 120.398), new Point(77.1804, 129.55), 225883 };
			yield return new object[] { new Point(77.1539, -120.398), new Point(77.1804, 129.55), 2332669 };
		}

		[TestMethod]
		[DynamicData(nameof(GetPointsForCalculatingDistanse), DynamicDataSourceType.Method)]
		public void Test_ValidPoints_DistanceCalculated(Point a, Point b, int dist)
		{
			Arc arc = new Arc(a, b);
			GeoInfo az = arc.GetAzimut();
			Assert.IsTrue(Math.Abs(dist - az.Distance) < Epsilon);
		}

		public static IEnumerable<object[]> GetPointsOnEquator()
		{
			yield return new object[] { new Point(0, 0), new Point(0, 30), 90 };
			yield return new object[] { new Point(0, 0), new Point(0, -30), 270.0 };
		}


		[TestMethod]
		[DynamicData(nameof(GetPointsOnEquator), DynamicDataSourceType.Method)]
		public void Test_PointsOnEquator_AzimutCalculated(Point a, Point b, double ans)
		{
			Arc arc = new Arc(a, b);
			GeoInfo az = arc.GetAzimut();
			Assert.IsTrue(Math.Abs(ans - az.AzimutValue) < Epsilon);
		}
	}
}

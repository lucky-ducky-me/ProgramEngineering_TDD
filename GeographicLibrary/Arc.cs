using System;

namespace GeographicLibrary
{
    public class Arc
    {
        Point p1;
        Point p2;

        const double ConvToRadiansCoef = Math.PI / 180.0;

        private const double Radius = 6372795;

        private const double Epsilon = 0.0001d;


		public Arc(Point p1, Point p2)
        {
            this.p1 = new Point(p1.Latitude, p1.Longitude);
            this.p2 = new Point(p2.Latitude, p2.Longitude);

			this.p1.Latitude *= ConvToRadiansCoef;
            this.p2.Latitude *= ConvToRadiansCoef;
            this.p1.Longitude *= ConvToRadiansCoef;
            this.p2.Longitude *= ConvToRadiansCoef;
        }

        public GeoInfo GetAzimut()
        {
            if (IsEqual(p1.Latitude, p2.Latitude)
                && IsEqual(p2.Longitude, p1.Longitude))
            {
	            return new GeoInfo(AzimutStatus.None, -1, -1);
            }

            var polarDegree = 90 * ConvToRadiansCoef;

            if (IsEqual(p1.Latitude, p2.Latitude) &&
                IsEqual(Math.Abs(p1.Latitude), polarDegree)
                && IsEqual(Math.Abs(p2.Latitude), polarDegree))
            {
				return new GeoInfo(AzimutStatus.None, -1, -1);
			} 

			var cl1 = Math.Cos(this.p1.Latitude);
            var cl2 = Math.Cos(this.p2.Latitude);
            var sl1 = Math.Sin(this.p1.Latitude);
            var sl2 = Math.Sin(this.p2.Latitude);

            var delta = this.p2.Longitude - this.p1.Longitude;
            var cdelta = Math.Cos(delta);
            var sdelta = Math.Sin(delta);

            var numerator = Math.Sqrt(
	            Math.Pow(cl2 * sdelta, 2) +
	            Math.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));

	        // Вычисление длины большого круга и расстояния
            var denumerator = sl1 * sl2 + cl1 * cl2 * cdelta;

            var ad = Math.Atan2(numerator, denumerator);

            var dist = ad * Radius;

            // Вычисление начального азимута

            var x = cl1 * sl2 - sl1 * cl2 * cdelta;
            var y = sdelta * cl2;
            var z = Math.Atan(-y / x) * (1 / ConvToRadiansCoef);

            if (x < 0)
            {
	            z += 180;
            }

            var z2 = (z + 180) % 360 - 180;
            z2 = -z2 * ConvToRadiansCoef;

            var anglerad = z2 - 2 * Math.PI * Math.Floor(z2 / (2 * Math.PI));
            var angledeg = anglerad * (1 / ConvToRadiansCoef);

			if (!IsEqual(Math.Abs(p1.Latitude), Math.Abs(p2.Latitude)) && (
				 IsEqual(Math.Abs(p1.Latitude), polarDegree)
				|| IsEqual(Math.Abs(p2.Latitude), polarDegree)
				))
			{
				return new GeoInfo(AzimutStatus.Defined, 180, (int)Math.Round(dist));
			}

			if (!IsEqual(p1.Longitude, p2.Longitude) && (
				 IsEqual(Math.Abs(p1.Latitude), polarDegree)
				&& IsEqual(Math.Abs(p2.Latitude), polarDegree)
				))
			{
				return new GeoInfo(AzimutStatus.Any, -1, (int)Math.Round(dist));
			}

			var geoInfo = new GeoInfo(AzimutStatus.Defined, angledeg, (int) Math.Round(dist));

            return geoInfo;
        }

        private bool IsEqual(double a, double b)
        {
	        return Math.Abs(a - b) < Epsilon;
        }
    }
}

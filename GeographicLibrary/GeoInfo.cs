namespace GeographicLibrary
{
	public class GeoInfo
	{
		public AzimutStatus AzimutStatus { get; private set; }

		public double AzimutValue { get; private set; }

		public int Distance { get; private set; }

		public GeoInfo(AzimutStatus azimutStatus, double azimutValue, int distance)
		{
			AzimutStatus = azimutStatus;
			AzimutValue = azimutValue;
			Distance = distance;
		}
	}
}
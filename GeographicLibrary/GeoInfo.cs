namespace GeographicLibrary
{
	public class GeoInfo
	{
		public AzimutStatus AzimutStatus { get; private set; }

		public double AzimutValue { get; private set; }

		public GeoInfo(AzimutStatus azimutStatus, double azimutValue)
		{
			AzimutStatus = azimutStatus;
			AzimutValue = azimutValue;
		}
	}
}
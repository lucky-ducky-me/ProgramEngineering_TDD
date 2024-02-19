using System;

namespace GeographicLibrary
{
    public class Point
    {
        double latitude;
        double longitude;

        public double Longitude 
        { 
            get => longitude; 
            set
            {
                if (Math.Abs(value) > 180)
                {
                    throw new ArgumentOutOfRangeException();
                }

                longitude = value;
            }
        }
        public double Latitude 
        {   
            get => latitude; 
            set
            {
                if (Math.Abs(value) > 90)
                {
                    throw new ArgumentOutOfRangeException();
                }

                latitude = value;
            } 
        }

        public Point(double latitude, double longitude)
        {
            if (Math.Abs(latitude) > 90 || Math.Abs(longitude) > 180)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}

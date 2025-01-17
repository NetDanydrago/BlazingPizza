﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BlazingPizza.Shared
{
    public class LatLong
    {
        public LatLong() { }
        public LatLong(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public static LatLong Interpolate(LatLong start, LatLong end, double proportion)
        {
            return new LatLong(start.Latitude + 
                (end.Latitude - start.Latitude) * proportion,
                start.Longitude +
                (end.Longitude - start.Longitude) * proportion);

        }
    }
}

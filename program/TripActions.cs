using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sharedEntities;
using dataPersistence;

namespace program
{
    public class TripActions
    {
        public static void AddTrip(Trip pTrip)
        {
            TripPersistence.AddTrip(pTrip);
        }
    }
}

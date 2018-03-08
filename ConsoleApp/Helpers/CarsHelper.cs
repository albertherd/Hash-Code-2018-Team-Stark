using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers
{
    public static class CarsHelper
    {
        public static List<Ride> GetEarliestStart(List<Ride> unsortedrides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedrides.OrderBy(r => r.EarliestStart).ThenByDescending(r => r.LatestFinish).ToList();

            return sortedRides;

        }

        public static List<Ride> GetEarliestStart2(List<Ride> unsortedrides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedrides.OrderBy(r => r.EarliestStart).ThenBy(r => r.LatestFinish).ToList();

            return sortedRides;

        }

        public static List<Ride> GetEarliestStart3(List<Ride> unsortedrides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedrides.OrderBy(r => r.RoundedEarliestStart).ThenBy(r => r.LatestFinish).ToList();

            return sortedRides;

        }

        public static List<Ride> GetLatestFinish(List<Ride> unsortedRides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedRides.OrderBy(r => r.LatestFinish).ThenBy(r => r.EarliestStart).ToList();

            return sortedRides;
        }

        public static List<Ride> GetByDistanceClosest(List<Ride> unsortedrides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedrides.OrderBy(r => r.GetDistance(new Location() { Columm = 0, Row = 0})).ThenByDescending(r => r.LatestFinish).ToList();

            return sortedRides;

        }

        public static List<Ride> EarliestDistance(List<Ride> unsortedrides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedrides.OrderBy(r => r.EarliestStart).ThenBy(r=>r.LatestFinish).ThenBy(r => r.GetRoughDistance(new Location() { Columm = 0, Row = 0 })).ToList();

            return sortedRides;

        }

        public static List<Ride> Earliest2(List<Ride> unsortedrides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedrides.OrderBy(r => r.LatestFinish).ThenBy(r => r.GetRoughDistance(new Location() { Columm = 0, Row = 0 })).ThenBy(r => r.EarliestStart).ToList();

            return sortedRides;

        }

        public static List<Ride> DistanceFromStart(List<Ride> unsortedrides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedrides.OrderBy(r => r.EarliestStart).ThenBy(r => r.DistanceFromStart).ToList();

            return sortedRides;
        }

        public static List<Ride> GetByLatestStart(List<Ride> unsortedrides)
        {
            List<Ride> sortedRides = new List<Ride>();

            sortedRides = unsortedrides.OrderBy(r => r.LatestStart).ThenBy(r => r.LatestFinish).ToList();

            return sortedRides;
        }

        

    }
}

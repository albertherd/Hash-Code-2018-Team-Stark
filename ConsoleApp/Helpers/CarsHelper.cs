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
    }
}

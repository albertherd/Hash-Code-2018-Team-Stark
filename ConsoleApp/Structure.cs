using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Helpers;

namespace ConsoleApp
{
    public class Structure
    {
        public City City { get; set; }
        public List<Ride> Rides { get; set; }
        public int Vehicles { get; set; }
        public int Bonus { get; set; }
        public int Steps { get; set; }

        public Ride GetNextRide(int curStep, Cart cart)
        {
            List<Ride> rides = new List<Ride>();
            List<RidesByDistance> toSort = new List<RidesByDistance>();
            foreach (Ride r in Rides)
            {
                if (r.IsCurrentlyPossible(curStep) && !r.IsDone && !r.IsInUse)
                {
                    rides.Add(r);
                    if (rides.Count > 50)
                        break;
                }
            }

            if (!rides.Any())
                return null;

            foreach (Ride ride in rides)
            {
                toSort.Add(new RidesByDistance()
                {
                    Ride = ride,
                    DistanceFromStart = DistanceHelper.GetDistance(cart.Location, ride.Start),
                    DistanceToEnd = DistanceHelper.GetDistance(cart.Location, ride.End)
                });
            }

            return toSort.OrderBy(cur => cur.DistanceFromStart).ThenBy(cur => cur.DistanceToEnd).ToList().FirstOrDefault().Ride;
        }

    }
}

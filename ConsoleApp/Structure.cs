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

        public void RemoveImpossibleRides(int curStep)
        {
            Rides = Rides.Where(r => r.IsCurrentlyPossible(curStep)).ToList();
        }

        public void RemoveImpossibleRidesStart(int curStep)
        {
            Rides = Rides.Where(r => r.IsCurrentlyPossibleFromLocation(curStep, new Location() { Columm = 0, Row = 0 })).ToList();
        }

        public void RemoveLongRides(int maxSteps)
        {
            Rides = Rides.Where(r => r.StepsRequired < maxSteps).ToList();
        }

        public void RemoveRidesBetween(int steps1, int steps2)
        {
            Rides = Rides.Where(r => r.StepsRequired < steps1 || r.StepsRequired > steps2).ToList();
        }

        public void RemoveFarRides(int maxDistance)
        {
            Rides = Rides.Where(r => r.GetDistance(new Location() { Columm = 0, Row = 0 }) < maxDistance).ToList();
        }

        public void RemoveFarRidesAndEarlyLong(int maxSteps,int timeFromEnd)
        {
            Rides = Rides.Where(r => r.StepsRequired < maxSteps || Steps - r.LatestFinish < timeFromEnd).ToList();
        }

        public Ride GetClosestRide(Location location, int curStep)
        {
           
            return Rides.Where(r => !r.IsInUse && r.IsCurrentlyPossibleFromLocation(curStep, location)).OrderByDescending(r => r.GetDistance(location)).FirstOrDefault();
        }

        public Ride ChooseFirstRide(Cart cart)
        {
            return Rides.Where(r => !r.IsInUse).OrderBy(r => r.RoundedEarliestStart).ThenBy(r => r.StepsRequired).FirstOrDefault();
        }

        public Ride GetNextRideForC(int curStep, Cart cart)
        {
            return Rides.Where(r => !r.IsInUse).OrderBy(r => r.GetDistance(cart.Location)).FirstOrDefault();
        }

        public Ride GetNextRide(int curStep, Cart cart)
        {

            List<Ride> rides = new List<Ride>();
            List<RidesByDistance> toSort = new List<RidesByDistance>();
            foreach (Ride r in Rides)
            {
                if (r.IsCurrentlyPossibleFromLocation(curStep, cart.Location) && !r.IsDone && !r.IsInUse)
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
                var distStart = DistanceHelper.GetDistance(cart.Location, ride.Start);
                //if (distStart >= (curStep - ride.EarliestStart))
                //{
                toSort.Add(new RidesByDistance()
                {
                    Ride = ride,
                    DistanceFromStart = distStart,
                    DistanceToEnd = DistanceHelper.GetDistance(cart.Location, ride.End),
                    TimeToWait = ride.TimeToWaitIfILeaveNow(curStep, cart.Location)
                    //DistanceToEnd = distStart + ride.StepsRequired
                });
                //}
            }

            if (toSort.Count == 0)
                return null;

            return toSort.OrderBy(r => r.DistanceFromStart).ThenBy(cur => cur.TimeToWait).FirstOrDefault().Ride;
            return toSort.OrderBy(cur => cur.DistanceFromStart).ThenBy(cur => cur.DistanceToEnd).FirstOrDefault().Ride;
            return rides.OrderBy(r => r.DistanceFromStart).ThenBy(r => r.LatestFinish).FirstOrDefault();
            return rides.OrderBy(r => r.LatestStart).FirstOrDefault();

         }

        public Ride GetOriginalRide(int curStep, Cart cart)
        {
            List<Ride> rides = new List<Ride>();
            List<RidesByDistance> toSort = new List<RidesByDistance>();
            //Winning
            foreach (Ride r in Rides)
            {
                if (r.IsCurrentlyPossibleFromLocation(curStep, cart.Location) && !r.IsDone && !r.IsInUse)
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
                var distStart = DistanceHelper.GetDistance(cart.Location, ride.Start);

                toSort.Add(new RidesByDistance()
                {
                    Ride = ride,
                    DistanceFromStart = distStart,
                    DistanceToEnd = DistanceHelper.GetDistance(cart.Location, ride.End)
                });

            }

            if (toSort.Count == 0)
                return null;

            return toSort.OrderBy(cur => cur.DistanceFromStart).ThenBy(cur => cur.DistanceToEnd).FirstOrDefault().Ride;

        }


    }
}

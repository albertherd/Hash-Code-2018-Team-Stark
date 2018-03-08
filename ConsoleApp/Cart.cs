using ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [Serializable]
    public class Cart
    {
        public Location Location { get; set; }
        public int NextEndTime { get; set; }

        public Ride Ride { get; set; }
        public bool IsIdle
        {
            get { return Ride == null; }
        }

        public List<Ride> RidesDone { get; set; }

        public void AssignRide(Ride r, int curStep)
        {
            Ride = r;       
            int distanceToStart = DistanceHelper.GetDistance(Location, r.Start);
            int stepsAtStart = Math.Max(curStep + distanceToStart, r.EarliestStart);
            int stepAtEnd = stepsAtStart + r.StepsRequired;
            NextEndTime = stepAtEnd;
        }

        public Cart()
        {
            RidesDone = new List<Ride>();
        }
    }
}

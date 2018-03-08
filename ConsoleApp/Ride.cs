using ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [Serializable]
    public class Ride
    {
        public Ride()
        {
            IsDone = false;
            IsInUse = false;
        }

        public int Id { get; set; }

        public Location Start { get; set; }
        public Location End { get; set; }
        public int EarliestStart { get; set; }
        public int RoundedEarliestStart { get { return Convert.ToInt32(Math.Ceiling(EarliestStart / 100.0) * 100);} }
        public int LatestFinish { get; set; }


        public bool IsDone { get; set; }

        public bool IsInUse { get; set; }
        public int StepsRequired
        {
            get
            {
                return DistanceHelper.GetDistance(Start, End);

            }
        }

        public int LatestStart { get { return LatestFinish - StepsRequired; } }


        public int DistanceFromStart
        {
            get { return DistanceHelper.GetDistance(new Location() { Columm = 0, Row = 0 }, Start); }
        }

        public bool IsPossible
        {
            get { return (LatestFinish - EarliestStart) > StepsRequired; }
        }

        public bool IsCurrentlyPossible(int currentStep)
        {
            return IsPossible && ((currentStep + StepsRequired) < LatestFinish);
        }

        public int GetDistance(Location curLocation)
        {
            return DistanceHelper.GetDistance(curLocation, Start);
        }

        public int GetRoughDistance(Location curLocation)
        {
            return Convert.ToInt32(DistanceHelper.GetDistance(curLocation, Start) / 400);
        }

        public bool IsCurrentlyPossibleFromLocation(int currentStep, Location location)
        {
            int distanceStart = DistanceHelper.GetDistance(location, Start);
            return IsCurrentlyPossible(currentStep) && ((distanceStart + currentStep + StepsRequired) <= LatestFinish);
        }

        public int GetScoreFromLocation(Location l)
        {
            return 0;
        }

        public bool DoIHaveToWaitIfILeaveNow(int curStep, Location currentLocation)
        {
            var distStart = DistanceHelper.GetDistance(currentLocation, Start);
            if ((curStep + distStart) >=EarliestStart)
            {
                return true;
            }
            return false;
        }

        public int TimeToWaitIfILeaveNow(int curStep, Location currentLocation)
        {
            var distStart = DistanceHelper.GetDistance(currentLocation, Start);
            return EarliestStart - curStep - distStart;

        }

    }
}

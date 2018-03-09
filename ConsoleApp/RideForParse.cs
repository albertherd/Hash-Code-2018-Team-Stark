using ConsoleApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class RideForParse
    {
        public int Id { get; set; }

        public Location Start { get; set; }
        public Location End { get; set; }
        public int EarliestStart { get; set; }
        public int LatestFinish { get; set; }


        public bool IsInUse { get; set; }
        public int StepsRequired { get; set; }

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
    }
}

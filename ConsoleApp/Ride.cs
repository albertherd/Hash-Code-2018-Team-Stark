using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
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
        public int LatestFinish { get; set; }
        public bool IsDone { get; set; }

        public bool IsInUse { get; set; }
        public int StepsRequired
        {
            get
            {
                return Math.Abs(End.Row - Start.Row) + Math.Abs((End.Columm - Start.Columm));
            }
        }

        public bool IsPossible
        {
            get { return (LatestFinish - EarliestStart) > StepsRequired; }
        }

        public bool IsCurrentlyPossible(int currentStep)
        {
            return IsPossible && ((currentStep + StepsRequired) < LatestFinish);
        }
    }
}

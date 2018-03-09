using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class RideScore
    {
        public int RideId { get; set; }
        public int Score { get; set; }
        public List<Ride> RidesToRemove { get; set; }
    }
}

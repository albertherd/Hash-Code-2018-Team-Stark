using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers
{
    public static class DistanceHelper
    {
        public static int GetDistance(Location start, Location end)
        {
            return Math.Abs(end.Row - start.Row) + Math.Abs((end.Columm - start.Columm));
        }
        
    }
}

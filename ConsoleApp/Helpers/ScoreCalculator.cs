using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers
{
    public static class ScoreCalculator
    {
        public static long GetScoreForC(List<Cart> carts)
        {

            var score = 0;
            foreach (Cart c in carts)
            {
                foreach(Ride r in c.RidesDone)
                {
                    score += r.StepsRequired;
                }
            }

            return score;
        }
    }
}

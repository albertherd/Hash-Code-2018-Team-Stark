using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers
{
    public static class Parser
    {
        public static Structure ParseAll(string firstLine, List<string> otherLines)
        {
            Structure structure = new Structure();
            List<int> seps = firstLine.Split(' ').Select(x => Int32.Parse(x)).ToList();

            structure.Bonus = seps[4];
            structure.Vehicles = seps[2];
            structure.Steps = seps[5];
            structure.Rides = new List<Ride>();

            for (int i=0; i< otherLines.Count; i++)
            {
                Ride ride = ParseLine(otherLines[i]);
                ride.Id = i;
                structure.Rides.Add(ride);
            }

            return structure;
        }

        public static Ride ParseLine(string line)
        {
            List<int> seps = line.Split(' ').Select(x => Int32.Parse(x)).ToList();

            Ride ride = new Ride();
            int starty = seps[0];
            int startx = seps[1];
            int endy = seps[2];
            int endx = seps[3];
            int earlyStart = seps[4];
            int latestFinish = seps[5];

            ride.Start = new Location() { Columm = startx, Row = starty };
            ride.End = new Location() { Columm = endx, Row = endy };
            ride.EarliestStart = earlyStart;
            ride.LatestFinish = latestFinish;
            
            return ride;
        }
    }
}

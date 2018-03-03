using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers
{
    /* Optimizations that never got used due to time constraints */
    public static class ZoneHelper
    {
        public static List<Zone> GenerateZones(int totalRows, int totalColumns, int zoneRows, int zoneCols)
        {
            List<Zone> zones = new List<Zone>();

            int numzonesinrow = Convert.ToInt32(Math.Ceiling((decimal)totalRows / (decimal)zoneRows));
            int numzonesincol = Convert.ToInt32(Math.Ceiling((decimal)totalColumns / (decimal)zoneCols));

            int totalzones = numzonesincol * numzonesinrow;

            for(int i=0; i< numzonesinrow; i++)
            {

                for (int j = 0; j < numzonesincol; i++)
                {
                    Zone zone = new Zone();
                    zone.ColumncCount = zoneCols;
                    zone.RowCount = zoneRows;
                    zone.StartLocation = new Location() { Columm = numzonesincol * j, Row = numzonesinrow * i };

                    zones.Add(zone);
                }
                    
            }

            return zones;
        }
    }
}

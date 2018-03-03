using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Helpers
{
    /* Optimizations that never got used due to time constraints */
    public class Zone
    {
        public int Id { get; set; }
        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public Location StartLocation { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }

        public int RowCount { get; set; }
        public int ColumncCount { get; set; }

        public Location EndLocation
        {
            get
            {
                return new Location
                {
                    Row = StartLocation.Row + RowCount,
                    Columm = StartLocation.Columm + ColumncCount
                };
            }
        }

        public bool IsInZone(Location location)
        {
            return (location.Row <= EndLocation.Row && location.Row >= StartLocation.Row)
                   && (location.Columm <= EndLocation.Columm && location.Columm >= StartLocation.Columm);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [Serializable]
    public class City
    {
       public List<Location> Locations { get; set; }
    }
}

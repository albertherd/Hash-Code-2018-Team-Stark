using ConsoleApp.Helpers;
using ConsoleApp.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = "b_should_be_easy.in";
            //string path = "c_no_hurry.in";
            string path = "d_metropolis.in";
            //string path = "e_high_bonus.in";

            string allFile = FileReader.ReadFile(path);

            var firstLine = FileReader.GetFirstLine(allFile);
            List<string> otherLines = FileReader.GetOtherLines(allFile);
            Structure structure = Parser.ParseAll(firstLine, otherLines);

            int initialRides = structure.Rides.Count;

            List<Cart> carts = new List<Cart>();
            for (int i = 0; i < structure.Vehicles; i++)
            {
                Cart c = new Cart() { Location = new Location() { Row = 0, Columm = 0 } };
                carts.Add(c);
            }

            structure.Rides = CarsHelper.GetEarliestStart(structure.Rides);

            for (int step = 0; step < structure.Steps; step++)
            {
                for (int cartid = 0; cartid < structure.Vehicles; cartid++)
                {
                    Cart cart = carts[cartid];
                    if (!cart.IsIdle && cart.NextEndTime == step)
                    {
                        cart.Location = cart.Ride.End;
                        structure.Rides.Remove(cart.Ride);
                        cart.RidesDone.Add(cart.Ride);
                        cart.Ride = null;
                    }

                    if (cart.IsIdle)
                    {
                        Ride r = structure.GetNextRide(step, cart);
                        if (r != null)
                        {
                            r.IsInUse = true;
                            cart.AssignRide(r, step);
                        }
                    }
                }
            }

            StringBuilder builder = new StringBuilder();
            int totalRides = 0;

            for (int id = 0; id < carts.Count; id++)
            {
                totalRides += carts[id].RidesDone.Count;

                builder.Append(carts[id].RidesDone.Count);
                builder.Append(" ");

                var ids = carts[id].RidesDone.Select(x => x.Id).ToList();
                builder.Append(string.Join(" ", ids.ToArray()));
                builder.Append("\n");
            }


            Console.WriteLine("Rides done: " + totalRides.ToString());
            Console.WriteLine("Total rides " + initialRides.ToString());
            Console.WriteLine("Finished calculation for " + path);

            File.WriteAllText("result-" + path + ".txt", builder.ToString());

            Console.ReadKey();
        }
    }
}

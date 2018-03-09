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
        public const string pathB = "b_should_be_easy.in";
        public const string pathC = "c_no_hurry.in";
        public const string pathD = "d_metropolis.in";
        public const string pathE = "e_high_bonus.in";

        private static string currentFile = pathC;

        static void Main(string[] args)
        {
            string allFile = FileReader.ReadFile(currentFile);

            var firstLine = FileReader.GetFirstLine(allFile);
            List<string> otherLines = FileReader.GetOtherLines(allFile);
            Structure initialstructure = Parser.ParseAll(firstLine, otherLines);

            int initialRides = initialstructure.Rides.Count;

            

            long highestScore = 0;

            //For C
            // structure.Rides = CarsHelper.GetEarliestStart(structure.Rides);

            //For D
            initialstructure.RemoveImpossibleRidesStart(0);

            //structure.RemoveFarRidesAndEarlyLong(3500,20000);
            //structure.RemoveRidesBetween(3000, 6000);
            //structure.RemoveFarRides(5000);
            //initialstructure.RemoveShortRides(800);

            //structure.Rides = CarsHelper.GetByLatestStart(structure.Rides);

            //Looper(initialstructure, CartStepper);

            
            List<Cart> carts = new List<Cart>();
            for (int i = 0; i < initialstructure.Vehicles; i++)
            {
                Cart c = new Cart() { Location = new Location() { Row = 0, Columm = 0 } };
                carts.Add(c);
            }
            Structure structure = initialstructure.Clone();

            //CartStepper(structure, carts);
            //StepCarter(structure, carts);
            BestCartForC(structure);

            StringBuilder builder = new StringBuilder();
            var totalRides = 0;

            for (int id = 0; id < carts.Count; id++)
            {
                totalRides += carts[id].RidesDone.Count;

                builder.Append(carts[id].RidesDone.Count);
                builder.Append(" ");

                var ids = carts[id].RidesDone.Select(x => x.Id).ToList();
                builder.Append(string.Join(" ", ids.ToArray()));
                builder.Append("\n");
            }

            Console.WriteLine("\r");

            if (currentFile.Equals(pathC, StringComparison.InvariantCultureIgnoreCase))
            {
                long curScore = ScoreCalculator.GetScoreForC(carts);
                highestScore = Math.Max(curScore, highestScore);

                Console.WriteLine("Cur Score: " + curScore);
                Console.WriteLine("Best Score: " + highestScore);
                File.WriteAllText("result-" + currentFile + "-" + curScore + ".txt", builder.ToString());

            }
            else
            {
                Console.WriteLine("Rides done: " + totalRides.ToString());
                Console.WriteLine("Total rides " + initialRides.ToString());
                Console.WriteLine("Finished calculation for " + currentFile);
                File.WriteAllText("result2-" + currentFile + ".txt", builder.ToString());
            }

            
            

            Console.ReadKey();
        }

        private static void StepCarter(Structure structure, List<Cart> carts)
        {
            structure.Rides = CarsHelper.GetEarliestStart(structure.Rides);
            var ridesDone = 0;
            var totalRides = structure.Rides.Count;

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
                        ridesDone++;
                        cart.Ride = null;
                    }

                    if (step % 200 == 0)
                    {
                       //structure.RemoveImpossibleRides(step);
                        Console.Write($"\rStep {step} / {structure.Steps}; Rides {ridesDone}/{totalRides}");

                    }

                    if (cart.IsIdle)
                    {

                        Ride r = null;

                        //if (step == 0)
                        //{
                        //    r = structure.ChooseFirstRide(cart);
                        //}
                        //else
                        {
                            if (currentFile.Equals(pathC, StringComparison.InvariantCultureIgnoreCase))
                                r = structure.GetNextRideForC(step, cart);
                            else if (currentFile.Equals(pathD, StringComparison.InvariantCultureIgnoreCase))
                                r = structure.GetNextRide(step, cart);
                            else if (currentFile.Equals(pathE, StringComparison.InvariantCultureIgnoreCase))
                                r = structure.GetNextRide(step, cart);
                            else if (currentFile.Equals(pathB, StringComparison.InvariantCultureIgnoreCase))
                                r = structure.GetNextRideForB(step, cart);
                        }

                        if (r != null)
                        {
                            r.IsInUse = true;
                            cart.AssignRide(r, step);
                        }
                    }
                }
            }
        }

        private static void Looper(Structure initialStructure, Func<Structure, List<Cart>, bool> func)
        {
            long highestScore = 0;
            int initialRides = initialStructure.Rides.Count;
            
            while (true)
            {
                List<Cart> carts = new List<Cart>();
                for (int i = 0; i < initialStructure.Vehicles; i++)
                {
                    Cart c = new Cart() { Location = new Location() { Row = 0, Columm = 0 } };
                    carts.Add(c);
                }
                Structure structure = initialStructure.Clone();

                func(structure, carts);

                StringBuilder builder = new StringBuilder();
                var totalRides = 0;

                for (int id = 0; id < carts.Count; id++)
                {
                    totalRides += carts[id].RidesDone.Count;

                    builder.Append(carts[id].RidesDone.Count);
                    builder.Append(" ");

                    var ids = carts[id].RidesDone.Select(x => x.Id).ToList();
                    builder.Append(string.Join(" ", ids.ToArray()));
                    builder.Append("\n");
                }

                Console.WriteLine("\r");

                if (currentFile.Equals(pathC, StringComparison.InvariantCultureIgnoreCase))
                {
                    long curScore = ScoreCalculator.GetScoreForC(carts);
                    highestScore = Math.Max(curScore, highestScore);

                    Console.WriteLine("Cur Score: " + curScore);
                    Console.WriteLine("Best Score: " + highestScore);
                    File.WriteAllText("result-" + currentFile + "-" + curScore + ".txt", builder.ToString());

                }
                else
                {
                    Console.WriteLine("Rides done: " + totalRides.ToString());
                    Console.WriteLine("Total rides " + initialRides.ToString());
                    Console.WriteLine("Finished calculation for " + currentFile);
                    File.WriteAllText("result2-" + currentFile + ".txt", builder.ToString());
                }

            }
        }

        private static bool CartStepper(Structure structure, List<Cart> carts)
        {
            structure.Rides = CarsHelper.GetByDistanceClosest(structure.Rides);

            var shuffledTopRides = structure.Rides.Take(structure.Vehicles * 2).OrderBy(r => Guid.NewGuid()).Take(structure.Vehicles).ToList();

            //structure.Rides = CarsHelper.ShuffleRides(structure.Rides);
            var ridesDone = 0;
            var totalRides = structure.Rides.Count;

            //Initialise Carts at step 0
            for (int cartid = 0; cartid < structure.Vehicles; cartid++)
            {
                Cart cart = carts[cartid];

                var rideIndex = shuffledTopRides[cartid].Id;

                Ride r = structure.Rides.Single(ride => ride.Id == rideIndex);
                r.IsInUse = true;
                cart.AssignRide(r, 0);
            }

            //Other steps
            for (int cartid = 0; cartid < structure.Vehicles; cartid++)
            {
                Cart cart = carts[cartid];

                var curStep = 1;
                
                while (curStep <= structure.Steps)
                {
                    if (cart.IsIdle) {
                        Ride nextRide = structure.GetNextRideForC(curStep, cart);
                        if (nextRide == null) curStep = structure.Steps+1;
                        else
                        {
                            nextRide.IsInUse = true;
                            cart.AssignRide(nextRide,curStep);
                        }

                    }

                    if (!cart.IsIdle)
                    {
                        curStep = cart.NextEndTime;
                        cart.Location = cart.Ride.End;
                        structure.Rides.Remove(cart.Ride);
                        cart.RidesDone.Add(cart.Ride);
                        ridesDone++;
                        cart.Ride = null;
                    }
                    
                }

                Console.Write($"\rCart {cartid+1} / {structure.Vehicles}; Rides {ridesDone}/{totalRides}");

            }

            return true;
        }

        private static bool BestCartForC(Structure initialStructure)
        {
            var cartscompleted = 0;
            

            while (cartscompleted < initialStructure.Vehicles)
            {
                var numRides = initialStructure.Rides.Count;
                List<RideScore> scorePerRide = new List<RideScore>();
               

                //First ride
                for (int i = 0; i < numRides; i++) {

                    Structure structure = initialStructure.Clone();
                    List<Ride> ridesToRemove = new List<Ride>();
                    Cart cart = new Cart();
                    cart.Location = new Location() { Row = 0, Columm = 0 };

                    var curStep = 1;
                    var rideScore = 0;

                    Ride r = structure.Rides[i];
                    r.IsInUse = true;
                    cart.AssignRide(r, i);

                    while (curStep <= structure.Steps)
                    {
                        if (cart.IsIdle)
                        {
                            Ride nextRide = structure.GetNextRideForC(curStep, cart);
                            if (nextRide == null) curStep = structure.Steps + 1;
                            else
                            {
                                nextRide.IsInUse = true;
                                cart.AssignRide(nextRide, curStep);
                            }

                        }

                        if (!cart.IsIdle)
                        {
                            curStep = cart.NextEndTime;
                            cart.Location = cart.Ride.End;
                            ridesToRemove.Add(cart.Ride);
                            structure.Rides.Remove(cart.Ride);
                            rideScore += cart.Ride.StepsRequired;
                            cart.Ride = null;
                        }

                    }

                    scorePerRide.Add(new RideScore() { RideId = i, Score = rideScore, RidesToRemove = ridesToRemove });

                    if (i%10==0) Console.Write($"\rCart {cartscompleted} / {structure.Vehicles}; Rides {i}/{numRides}");

                }

                RideScore bestRideToStart = scorePerRide.OrderByDescending(r => r.Score).FirstOrDefault();
                Console.WriteLine($"Best Ride Cart {cartscompleted}: Ride {bestRideToStart.RideId}");
                using (StreamWriter sw = File.AppendText("bestrides-" + currentFile + ".txt"))
                {
                    sw.WriteLine(bestRideToStart.RideId);
                }


                foreach(Ride ride in bestRideToStart.RidesToRemove)
                {
                    Ride rideFromStructure = initialStructure.Rides.Single(r => r.Id == ride.Id);
                    initialStructure.Rides.Remove(rideFromStructure);
                }

                cartscompleted++;
            }
            return true;
        }
    }
}

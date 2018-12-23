using System;
using System.Collections.Generic;
using System.Threading;

namespace ElevatorManager
{
    public class Program
    {
        private const string QUIT = "q";

        public static void Main(string[] args)
        {

        Start:

            Console.WriteLine("What is the Top Floor in the the building?");
            int floor;
            string floorInput;
            int availableElevators;
            string availableElevatorsInput;
            Building building = null;

            floorInput = Console.ReadLine();
            Console.WriteLine("How many elevators available in the building?");
            availableElevatorsInput = Console.ReadLine();

            if (Int32.TryParse(floorInput, out floor) && Int32.TryParse(availableElevatorsInput, out availableElevators))
            {
                building = new Building(floor, availableElevators);
            }
            else
            {
                Console.WriteLine("That' doesn't make sense...");
                Console.Beep();
                Thread.Sleep(2000);
                Console.Clear();
                goto Start;
            }
            string input = string.Empty;
            Console.WriteLine("All " + availableElevators + " elevators are in ground floor right now...");
            while (input != QUIT)
            {
                Console.WriteLine("Request coming from which floor?");

                input = Console.ReadLine();

                if (Int32.TryParse(input, out floor))
                {

                    #region FIND THE CLOSEST ELEVATOR

                    List<int> stuckElevatorsIndex = new List<int>();
                    bool atleastOneWorkingElevatorAvailable = false;
                Begin:
                    int closestElevatorIndex = 0;
                    int currentBestDifference = int.MaxValue;

                    for (int i = 0; i < building.Elevators.Count; i++)
                    {
                        if (!stuckElevatorsIndex.Contains(i))
                        {
                            atleastOneWorkingElevatorAvailable = true;

                            if (currentBestDifference == int.MaxValue)
                            {
                                closestElevatorIndex = i;
                                currentBestDifference = Math.Abs(building.Elevators[i].currentFloor - floor);
                            }
                            else if (Math.Abs(building.Elevators[i].currentFloor - floor) < currentBestDifference)
                            {
                                currentBestDifference = Math.Abs(building.Elevators[i].currentFloor - floor);
                                closestElevatorIndex = i;
                            }
                        }
                    }
                    if (stuckElevatorsIndex.Count == building.Elevators.Count)
                    {
                        Console.WriteLine("All elevators are stuck..!!!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Is closest elevator (Elevator" + (closestElevatorIndex + 1).ToString() + ") taking more time? Y/N");
                    }
                    if (Console.ReadLine() == "Y")
                    {
                        if (!stuckElevatorsIndex.Contains(closestElevatorIndex))
                        {
                            stuckElevatorsIndex.Add(closestElevatorIndex);
                        }

                        goto Begin;
                    }
                    else
                    {
                        if (atleastOneWorkingElevatorAvailable)
                            building.Elevators[closestElevatorIndex].FloorPress(floor);
                    }

                    #endregion
                }
                else
                    Console.WriteLine("You have pressed an incorrect floor, Please try again");
            }
        }
    }
}
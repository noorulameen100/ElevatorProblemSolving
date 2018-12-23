using System;
using System.Collections.Generic;

namespace ElevatorManager
{
    public class Building
    {
        private Lazy<List<Elevator>> elevators = null;

        public List<Elevator> Elevators
        {
            get
            {
                return elevators.Value;
            }
        }

        int TopFloor { get; set; }

        int ElevatorsCount { get; set; }

        public Building(int topFloor = 10, int elevatorsCount = 1)
        {
            TopFloor = topFloor;
            ElevatorsCount = elevatorsCount;
            elevators = new Lazy<List<Elevator>>(() => AddElevators());
        }

        private List<Elevator> AddElevators()
        {
            List<Elevator> elev = new List<Elevator>();

            for (int i = 1; i <= ElevatorsCount; i++)
            {
                elev.Add(new Elevator(TopFloor, i));
            }

            return elev;
        }
    }

}
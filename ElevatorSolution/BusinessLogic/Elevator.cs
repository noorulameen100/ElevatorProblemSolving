
using System;

namespace ElevatorManager
{
    public class Elevator
    {
        private bool[] floorReady;//including basements and ground floor
        private int numberOfBasements = 2;
        public int currentFloor = 0;
        public int elevatorNumber;
        private int topfloor;
        public ElevatorStatus Status = ElevatorStatus.STOPPED;

        /// <summary>
        /// Elevator Constructor
        /// </summary>
        /// <param name="numberOfFloors">Floors count excluding basement and ground floor</param>
        /// <param name="elevatorNumber"></param>
        public Elevator(int numberOfFloors = 10, int elevatorNumber = 1)
        {
            floorReady = new bool[numberOfFloors + 3];
            topfloor = numberOfFloors;
            this.elevatorNumber = elevatorNumber;
        }

        /// <summary>
        /// Elevator stops at this floor
        /// </summary>
        /// <param name="floor"></param>
        private void Stop(int floor)
        {
            Status = ElevatorStatus.STOPPED;
            currentFloor = floor;
            floorReady[floor + numberOfBasements] = false;
            Console.WriteLine("Elevator" + elevatorNumber + " opening the door at floor{0}", floor);
        }

        /// <summary>
        /// Elevator goes down from currentFloor to requested floor
        /// </summary>
        /// <param name="floor"></param>
        private void Descend(int floor)
        {
            for (int i = currentFloor - 1; i >= -2; i--)
            {
                if (floorReady[i + numberOfBasements])
                {
                    Stop(floor);
                    break;
                }
                else
                {
                    Console.WriteLine("Elevator" + elevatorNumber + " crossing floor" + i.ToString() + "...");
                    continue;
                }
            }

            Status = ElevatorStatus.STOPPED;
            Console.WriteLine("Waiting..");
        }

        /// <summary>
        /// Elevator goes up from currentFloor to requested floor
        /// </summary>
        /// <param name="floor"></param>
        private void Ascend(int floor)
        {
            for (int i = currentFloor + 1; i <= topfloor; i++)
            {
                if (floorReady[i + numberOfBasements])
                {
                    Stop(floor);
                    break;
                }
                else
                {
                    Console.WriteLine("Elevator" + elevatorNumber + " crossing floor" + i.ToString() + "...");
                    continue;
                }
            }

            Status = ElevatorStatus.STOPPED;
            Console.WriteLine("Waiting..");
        }

        /// <summary>
        /// If an elevator already available in requested floor
        /// </summary>
        void StayPut()
        {
            Console.WriteLine("Elevator" + elevatorNumber + " opening the door.");
        }

        /// <summary>
        /// Requested floor
        /// </summary>
        /// <param name="requestCameFromFloor"></param>
        public void FloorPress(int requestCameFromFloor)
        {
            if (requestCameFromFloor > topfloor)
            {
                Console.WriteLine("We only have {0} floors", topfloor);
                return;
            }

            floorReady[requestCameFromFloor + numberOfBasements] = true;

            switch (Status)
            {

                case ElevatorStatus.DOWN:
                    Descend(requestCameFromFloor);
                    break;

                case ElevatorStatus.STOPPED:
                    if (currentFloor < requestCameFromFloor)
                        Ascend(requestCameFromFloor);
                    else if (currentFloor == requestCameFromFloor)
                        StayPut();
                    else
                        Descend(requestCameFromFloor);
                    break;

                case ElevatorStatus.UP:
                    Ascend(requestCameFromFloor);
                    break;

                default:
                    break;
            }


        }

        public enum ElevatorStatus
        {
            UP,
            STOPPED,
            DOWN
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Robot
{
    public class Robot
    {
        private int _canMoveX;
        private int _canMoveY;
        private int _x;
        private int _y;
        private int _commandCount;
        public List<int[]> _cleanedAreas = new List<int[]>();
        private List<string[]> _commandList = new List<string[]>();

        private Coordinate Cleaner { get; set; }


        public Robot()
        {
            _canMoveX = 100000;//Width
            _canMoveY = 100000;//Height
            SetCommand();
            SetRobotLocation();
            SetCommandLocations();
        }

        public Robot(int commandCount, int x, int y, List<string[]> commandList)
        {
            _canMoveX = 100000;//Width
            _canMoveY = 100000;//Height
            _commandCount = commandCount;
            _x = x;
            _y = y;
            _commandList = commandList;
        }

        /// <summary>
        /// Initialze The Environment
        /// </summary>
        public void Initialize()
        {
            InitRobot(_x, _y);

            StartCleaning();

            PrintCleanedAreas();

            //Exit();
        }

        /// <summary>
        /// Helper to Coordinate Movement count
        /// </summary>
        void MoveRobot(string coord, int count)
        {
            for (int i = 0; i < count; i++)
            {
                CoordinateMovement(coord);
            }
        }

        /// <summary>
        /// Coordinate Movement
        /// </summary>
        void CoordinateMovement(string coordinate)
        {
            switch (coordinate)
            {
                case "N":
                    MoveRobotCoordinate(0, -1);
                    break;

                case "E":
                    MoveRobotCoordinate(1, 0);
                    break;

                case "S":
                    MoveRobotCoordinate(0, 1);
                    break;

                case "W":
                    MoveRobotCoordinate(-1, 0);
                    break;
            }
        }

        /// <summary>
        /// Change Robot Location
        /// </summary>
        void MoveRobotCoordinate(int x, int y)
        {
            var newRobot = new Coordinate()
            {
                X = Cleaner.X + x,
                Y = Cleaner.Y + y
            };

            if (!CanMove(newRobot)) return;

            //Save Cleaned Areas
            _cleanedAreas.Add(new int[] { newRobot.X, newRobot.Y });

            Cleaner = newRobot;
        }

        /// <summary>
        /// Make sure that the new coordinate is not placed outside the bound of the plane
        /// </summary>
        bool CanMove(Coordinate c)
        {
            if (c.X < 0 || c.X >= _canMoveX)
                return false;

            if (c.Y < 0 || c.Y >= _canMoveY)
                return false;

            return true;
        }

        /// <summary>
        /// Initialize the Robot
        /// </summary>
        void InitRobot(int x, int y)
        {
            Cleaner = new Coordinate()
            {
                X = x,
                Y = y
            };
            MoveRobotCoordinate(0, 0);
        }

        /// <summary>
        /// Set The Number of Commands
        /// </summary>
        void SetCommand()
        {
            _commandCount = Convert.ToInt32(Console.ReadLine());
        }

        /// <summary>
        /// Set Command Locations
        /// </summary>
        void SetCommandLocations()
        {
            for (int i = 0; i < _commandCount; i++)
            {
                var input = Console.ReadLine();
                if (input != null)
                {
                    string[] arr = input.Split(' ');
                    if (arr.Length < 2) return;
                    _commandList.Add(new string[] { arr[0], arr[1] });
                }
            }
        }

        /// <summary>
        /// Set Robot Location
        /// </summary>
        void SetRobotLocation()
        {
            var input = Console.ReadLine();
            if (input != null)
            {
                string[] arr = input.Split(' ');
                if (arr.Length < 2) return;
                _x = (_canMoveX / 2) + Convert.ToInt32(arr[0]);
                _y = (_canMoveY / 2) + Convert.ToInt32(arr[1]);
            }
        }

        /// <summary>
        /// Cleaning Process
        /// </summary>
        void StartCleaning()
        {
            for (int i = 0; i < _commandCount; i++)
            {
                if (i < _commandList.Count)
                {
                    MoveRobot(_commandList.ElementAt(i)[0], Convert.ToInt32(_commandList.ElementAt(i)[1]));
                }

            }
        }

        /// <summary>
        /// # of Cleaned Areas
        /// </summary>
        void PrintCleanedAreas()
        {
            Console.WriteLine("Cleaned: " + _cleanedAreas.Count);
        }

        /// <summary>
        /// Exit Method From Console
        /// </summary>
        void Exit()
        {
            while (Console.ReadLine() != "q") { }
        }

    }

    class Coordinate
    {
        public int X { get; set; } //Left
        public int Y { get; set; } //Top
    }
}

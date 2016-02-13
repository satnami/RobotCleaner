using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Robot
{
    public class RobotCleaner
    {
        const ConsoleColor HeroColor = ConsoleColor.DarkBlue;
        const ConsoleColor BackgroundColor = ConsoleColor.Black;
        private int _canMoveX;
        private int _canMoveY;
        private int _x;
        private int _y;
        private List<int[]> _cleanedAreas = new List<int[]>();

        private Coordinate Cleaner { get; set; } //Will represent our here that's moving around :P/>

        public void Initialize()
        {
            Console.WriteLine("Welcome To Robot Cleaner");
            Console.WriteLine("Enter Width");
            _canMoveX = 50;
            Console.WriteLine("Enter Height");
            _canMoveY = 50;
            Console.WriteLine("Enter X then Y");
            _x = (_canMoveX / 2) + IntValidation();
            _y = (_canMoveY / 2) + IntValidation();
            InitGame(_x, _y);

            MoveHero(2, "E");
            MoveHero(1, "N");

            PrintCleanedAreas();

            Exit();
        }

        /// <summary>
        /// Hero Multipe Movements
        /// </summary>
        void MoveHero(int count, string coord)
        {
            for (int i = 0; i < count; i++)
            {
                CoordinateMovement(coord);
            }
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Curser Movement
        /// </summary>
        void CurserMovement()
        {
            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        MoveHero(0, -1);
                        break;

                    case ConsoleKey.RightArrow:
                        MoveHero(1, 0);
                        break;

                    case ConsoleKey.DownArrow:
                        MoveHero(0, 1);
                        break;

                    case ConsoleKey.LeftArrow:
                        MoveHero(-1, 0);
                        break;
                }
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
                    MoveHero(0, -1);
                    break;

                case "E":
                    MoveHero(1, 0);
                    break;

                case "S":
                    MoveHero(0, 1);
                    break;

                case "W":
                    MoveHero(-1, 0);
                    break;
            }
        }

        /// <summary>
        /// Paint the new hero
        /// </summary>
        void MoveHero(int x, int y)
        {
            var newHero = new Coordinate()
            {
                X = Cleaner.X + x,
                Y = Cleaner.Y + y
            };

            if (!CanMove(newHero)) return;
            RemoveHero();

            MoveAndCleanHero(newHero);

            //Save Cleaned Areas
            _cleanedAreas.Add(new int[] { newHero.X, newHero.Y });

            Cleaner = newHero;
        }

        /// <summary>
        /// Move the Hero And Clean
        /// </summary>
        void MoveAndCleanHero(Coordinate newHero)
        {
            Console.BackgroundColor = HeroColor;
            Console.SetCursorPosition(newHero.X, newHero.Y);
            Console.Write(" ");
        }

        /// <summary>
        /// Overpaint the old hero
        /// </summary>
        void RemoveHero()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.SetCursorPosition(Cleaner.X, Cleaner.Y);
            Console.Write(" ");
        }

        /// <summary>
        /// Make sure that the new coordinate is not placed outside the
        /// console window (since that will cause a runtime crash
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
        /// Paint a background color
        /// </summary>
        /// <remarks>
        /// It is very important that you run the Clear() method after
        /// changing the background color since this causes a repaint of the background
        /// </remarks>
        void SetBackgroundColor()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.Clear(); //Important!
        }

        /// <summary>
        /// Initiates the game by painting the background
        /// and initiating the hero
        /// </summary>
        void InitGame(int x, int y)
        {
            SetBackgroundColor();
            MakeItDirty();
            Cleaner = new Coordinate()
            {
                X = x,
                Y = y
            };

            MoveHero(0, 0);
        }

        /// <summary>
        /// Initiates the board by printing the background with random dust
        /// </summary>
        void MakeItDirty()
        {
            Random random = new Random();
            //int randomNumber = random.Next(0, 100);
            for (int i = 0; i < _canMoveX; i++)
            {
                for (int j = 0; j < _canMoveY; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Input Number Validataion
        /// </summary>
        int IntValidation()
        {
            string input = Console.ReadLine();
            int result;
            while (!Int32.TryParse(input, out result))
            {
                Console.WriteLine("Not a valid number, try again.");
                input = Console.ReadLine();
            }

            return result;
        }


        /// <summary>
        /// Initiates the board by printing the background with random dust
        /// </summary>
        void PrintCleanedAreas()
        {
            Console.SetCursorPosition(0, _canMoveY + 1);
            Console.WriteLine("Cleaned Areas");
            foreach (var item in _cleanedAreas)
            {
                Console.WriteLine(item[0] + "," + item[1]);
            }

            Console.WriteLine("Cleaned: " + _cleanedAreas.Count);
        }


        void Exit()
        {
            while (Console.ReadLine() != "quit") { }
        }
    }
}

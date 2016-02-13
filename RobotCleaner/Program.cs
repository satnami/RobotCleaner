using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robot;

namespace RobotCleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot.Robot();
            robot.Initialize();

            //var robotCleaner = new Robot.RobotCleaner();
            //robot.Initialize();
        }
    }
}

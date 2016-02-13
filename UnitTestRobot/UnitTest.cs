using System;
using System.Collections.Generic;
using System.ComponentModel;
using Robot;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Robots = Robot.Robot;

namespace UnitTestRobot
{
    [TestClass]
    public class UnitTest
    {
        Robots robot;
        public void TestInitialize()
        {
            List<string[]> commmaList= new List<string[]>();
            commmaList.Add(new []{"E", "2"});
            commmaList.Add(new []{"N", "1"});
            robot = new Robots(2, 10, 22, commmaList);
            robot.Initialize();
        }

        [TestMethod]
        public void Test_Properties()
        {
            int num = 10;
            TestInitialize();
            Assert.AreEqual(4, robot._cleanedAreas.Count, "Correct");
        }
    }
}

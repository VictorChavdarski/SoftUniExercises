namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        private Robot robot;
        private RobotManager robotManager;

        [SetUp]
        public void Setup()
        {
            robot = new Robot("Test", 10);
            robotManager = new RobotManager(10);
        }

        [Test]
        public void WhenRobotIsCreated_PropertiesShouldBeSet()
        {
            Assert.AreEqual("Test", robot.Name);
            Assert.AreEqual(10, robot.Battery);
            Assert.AreEqual(10, robot.MaximumBattery);
        }

        [Test]
        public void WhenRobotManagerIsCreated_CapacityShouldBeSet()
        {
            Assert.AreEqual(10, robotManager.Capacity);
        }

        [Test]
        public void WhenRobotManagerCapacityIsNegative_ExceptionShouldBeThrown()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RobotManager manager = new RobotManager(-5);
            });
        }

        [Test]
        public void WhenRobotManagerIsCreated_CountShouldBe0()
        {
            Assert.AreEqual(0, robotManager.Count);
        }

        [Test]
        public void WhenAddSameRobots_ExceptionShouldBeThrown()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Add(robot);
                robotManager.Add(robot);
            });
        }

        [Test]
        public void WhenAddWithoutCapacity_ExceptionShouldBeThrown()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                RobotManager manager = new RobotManager(1);
                manager.Add(robot);
                manager.Add(new Robot("Test2", 10));
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                RobotManager manager = new RobotManager(0);
                manager.Add(robot);
            });
        }

        [Test]
        public void WhenAddCorrectData_ShouldWork()
        {
            robotManager.Add(robot);
            Assert.AreEqual(1, robotManager.Count);
            robotManager.Add(new Robot("Test2", 2));
            Assert.AreEqual(2, robotManager.Count);
        }

        [Test]
        public void WhenRemoveNotExistingRobot_ExceptionShouldBeThrown()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Remove("Error");
            });
        }

        [Test]
        public void WhenRemoveExistingRobot_ShouldWork()
        {
            robotManager.Add(new Robot("TestExist", 20));
            robotManager.Remove("TestExist");

            Assert.AreEqual(0, robotManager.Count);

        }


        [Test]
        public void WhenWorkNotExistingRobot_ExceptionShouldBeThrown()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work("Error", "", 10);
            });
        }

        [Test]
        public void WhenWorkNotChargeRobot_ExceptionShouldBeThrown()
        {
            robotManager.Add(robot);
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Work(robot.Name, "Test", robot.Battery + 10);
            });
        }

        [Test]
        public void WhenWorkChargeRobot_ShouldDecreaseBattery()
        {
            robotManager.Add(robot);
            robotManager.Work(robot.Name,"Job", 5);
            Assert.AreEqual(robot.Battery, 5);
        }

        [Test]
        public void WhenChargeNotExisitng_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                robotManager.Charge("NotExisitng");
            });
        }

        [Test]
        public void WhenChargeRobot_ShouldGetBatteryToMax()
        {
            robotManager.Add(robot);
            robotManager.Work(robot.Name, "Job", 5);
            robotManager.Charge(robot.Name);
            Assert.AreEqual(robot.Battery, 10);
        }



    }
}

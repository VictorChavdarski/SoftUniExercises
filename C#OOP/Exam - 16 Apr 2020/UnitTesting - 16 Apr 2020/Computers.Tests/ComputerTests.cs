namespace Computers.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    public class ComputerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_SetCorrectNameProperty()
        {
            Computer computer = new Computer("a");
            Assert.AreEqual("a", computer.Name);
        }

        [Test]
        public void Constructor_PartsCollectionIsEmpty()
        {
            Computer computer = new Computer("a");
            Assert.IsEmpty(computer.Parts);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void NamePropert_EmptyValue_ShouldThrowArgumentNullException(string name)
        {
            Assert.Throws<ArgumentNullException>(() => new Computer(name));
        }

        [Test]
        public void PartsProperty_AddTwoParts_ShouldAddTwoParts()
        {
            Computer computer = new Computer("a");
            computer.AddPart(new Part("a", 1));
            computer.AddPart(new Part("b", 2));
            Assert.AreEqual(2, computer.Parts.Count);
        }

        [Test]
        public void TotalPriceProperty_ShouldReturnCorrectResult()
        {
            Computer computer = new Computer("a");
            computer.AddPart(new Part("a", 1));
            computer.AddPart(new Part("b", 2));
            computer.AddPart(new Part("c", 3));
            Assert.AreEqual(6, computer.TotalPrice);
        }

        [Test]
        public void AddPartMethod_NullPart_ShouldThrowInvalidOperationException()
        {
            Computer computer = new Computer("a");

            Assert.Throws<InvalidOperationException>(() => computer.AddPart(null));
        }

        [Test]
        public void AddPartMethod_AddPart_ShouldAddPart()
        {
            Computer computer = new Computer("a");
            computer.AddPart(new Part("a", 1));

            Assert.AreEqual(1, computer.Parts.Count);
        }

        [Test]
        public void AddPartMethod_AddPart_ShouldAddCorrectPart()
        {
            Computer computer = new Computer("a");
            computer.AddPart(new Part("a", 1));

            Part part = computer.Parts.FirstOrDefault(p => p.Name == "a");

            Assert.IsNotNull(part);
        }

        [Test]
        public void RemovePartMethod_RemovePart_ShouldRemoveCorrectly()
        {
            Computer computer = new Computer("a");
            Part part = new Part("a", 1);

            computer.AddPart(part);
            computer.RemovePart(part);

            Assert.AreEqual(0, computer.Parts.Count);
        }

        [Test]
        public void RemovePartMethod_ValidPart_ShouldReturnTrue()
        {
            Computer computer = new Computer("a");
            Part part = new Part("a", 1);

            computer.AddPart(part);

            bool actualResult = computer.RemovePart(part);

            Assert.IsTrue(actualResult);
        }

        [Test]
        public void RemovePartMethod_InValidPart_ShouldReturnFalse()
        {
            Computer computer = new Computer("a");
            Part part = new Part("a", 1);
            Part partTwo = new Part("b", 1);

            computer.AddPart(part);


            bool actualResult = computer.RemovePart(partTwo);

            Assert.IsFalse(actualResult);
        }

        [Test]
        public void RemovePartMethod_RemovePart_ShouldRemoveCorrect()
        {
            Computer computer = new Computer("a");
            Part part = new Part("a", 1);

            computer.AddPart(part);
            computer.RemovePart(part);

            Part actualPart = computer.Parts.FirstOrDefault(p => p.Name == "a");

            Assert.IsNull(actualPart);
        }

        [Test]
        public void GetPartMethod_ValidPart_ShouldReturnCorrectPart()
        {
            Computer computer = new Computer("a");
            Part part = new Part("a", 1);

            computer.AddPart(part);

            Part actualPart = computer.GetPart("a");

            Assert.AreEqual("a", actualPart.Name);
            Assert.AreEqual(1, actualPart.Price);
        }

        [Test]
        public void GetPartMethod_InValidPart_ShouldReturnNull()
        {
            Computer computer = new Computer("a");
            Part part = new Part("a", 1);

            computer.AddPart(part);

            Part actualPart = computer.GetPart("q");

            Assert.IsNull(actualPart);
        }
    }
}
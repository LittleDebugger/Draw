using System;
using Draw.Console.CoreImplementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Draw.Console.Tests
{
    [TestClass]
    public class ConsolePixelTests
    {
        [TestMethod]
        public void ConsolePixelDataIsSet()
        {
            var consolePixelData = new ConsolePixelData();

            // act
            var consolePixel = new ConsolePixel(consolePixelData);

            Assert.AreEqual(consolePixelData, consolePixel.Data);
        }

        [TestMethod]
        public void Constructor_WhenNullData_Throws()
        {
            // act
            Assert.ThrowsException<ArgumentNullException>(() => new ConsolePixel(null));
        }

        [TestMethod]
        public void SetColour_SetsColour()
        {
            var consolePixelData = new ConsolePixelData { Colour = 'A' };
            var consolePixel = new ConsolePixel(consolePixelData);

            var consolePixelDataReference = new ConsolePixelData { Colour = 'B' };
            var consolePixelReference = new ConsolePixel(consolePixelDataReference);

            // act
            consolePixel.SetColour(consolePixelReference);

            Assert.AreEqual(consolePixelDataReference.Colour, consolePixelData.Colour);
        }

        [TestMethod]
        public void IsColour_NoMatch_ReturnsFalse()
        {
            var consolePixelData = new ConsolePixelData { Colour = 'A' };
            var consolePixel = new ConsolePixel(consolePixelData);

            var consolePixelDataReference = new ConsolePixelData { Colour = 'B' };
            var consolePixelReference = new ConsolePixel(consolePixelDataReference);

            // act
            var result = consolePixel.IsColourMatch(consolePixelReference);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsColour_Match_ReturnsTrue()
        {
            var consolePixelData = new ConsolePixelData { Colour = 'A' };
            var consolePixel = new ConsolePixel(consolePixelData);

            var consolePixelDataReference = new ConsolePixelData { Colour = 'A' };
            var consolePixelReference = new ConsolePixel(consolePixelDataReference);

            // act
            var result = consolePixel.IsColourMatch(consolePixelReference);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Clone_Clones()
        {
            var consolePixelData = new ConsolePixelData { Colour = 'A' };
            var consolePixel = new ConsolePixel(consolePixelData);

            // act
            var result = consolePixel.Clone();

            Assert.AreNotEqual(result, consolePixel);
            Assert.AreNotEqual(result.Data, consolePixel.Data);
            Assert.AreEqual(result.Data.Colour, consolePixel.Data.Colour);
        }
    }
}
using Draw.Core.Entities;
using Draw.Core.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Draw.Core.Tests
{
    [TestClass]
    public class CanvasTests
    {
        [TestMethod]
        public void WidthAndHeightSet()
        {
            var width = 10;
            var height = 20;

            var subject = new Canvas<PixelDataStub>(width, height, new PixelStub());

            Assert.AreEqual(subject.Width, width);
            Assert.AreEqual(subject.Height, height);
        }

        [TestMethod]
        public void Indexer_PixelsWithinBoundary_ReturnsPixel()
        {
            var width = 10;
            var height = 20;

            var subject = new Canvas<PixelDataStub>(width, height, new PixelStub());

            // act
            var result = subject[1, 1];
            result = subject[1, height];
            result = subject[width, 1];
            result = subject[width, height];
        }

        [TestMethod]
        public void PixelsAreAllDifferentInstances()
        {
            var width = 2;
            var height = 2;

            var subject = new Canvas<PixelDataStub>(width, height, new PixelStub());

            Assert.AreNotEqual(subject[1, 1], subject[1, 2]);
            Assert.AreNotEqual(subject[1, 1], subject[2, 1]);
            Assert.AreNotEqual(subject[1, 1], subject[2, 2]);

            Assert.AreNotEqual(subject[1, 2], subject[2, 1]);
            Assert.AreNotEqual(subject[1, 2], subject[2, 2]);

            Assert.AreNotEqual(subject[2, 1], subject[2, 2]);

            Assert.AreNotEqual(subject[1, 1].Data, subject[1, 2].Data);
            Assert.AreNotEqual(subject[1, 1].Data, subject[2, 1].Data);
            Assert.AreNotEqual(subject[1, 1].Data, subject[2, 2].Data);

            Assert.AreNotEqual(subject[1, 2].Data, subject[2, 1].Data);
            Assert.AreNotEqual(subject[1, 2].Data, subject[2, 2].Data);

            Assert.AreNotEqual(subject[2, 1].Data, subject[2, 2].Data);
        }
    }
}
using System;
using System.Collections.Generic;
using Draw.Core.Commands;
using Draw.Core.Commands.Interfaces;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;
using Draw.Core.Exceptions;
using Draw.Core.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Draw.Core.Tests.Commands
{
    [TestClass]
    public class DrawStraightLineTests
    {
        private readonly Mock<ICanvas<PixelDataStub>> _canvasMock = new Mock<ICanvas<PixelDataStub>>();
        private readonly Mock<IPixel<PixelDataStub>> _pixelMock = new Mock<IPixel<PixelDataStub>>();
        private readonly IDrawStraightLine<PixelDataStub> _subject;

        public DrawStraightLineTests()
        {
            _subject = new DrawStraightLine<PixelDataStub>(new Mock<ILogger>().Object);
            _canvasMock.SetupGet(c => c[It.IsAny<int>(), It.IsAny<int>()])
                .Returns(_pixelMock.Object);
        }

        [TestMethod]
        public void Draw_NullCanvas_ThrowsValidationException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                _subject.Draw(null, new Point(1, 1), new Point(1, 1), new PixelStub()));
        }

        [TestMethod]
        public void Draw_NullReferencePixel_ThrowsValidationException()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
                _subject.Draw(_canvasMock.Object, new Point(1, 1), new Point(1, 1), null));
        }

        [TestMethod]
        public void Draw_LineNotHorizontalOrVertical_ThrowsValidationException()
        {
            Assert.ThrowsException<ValidationException>(() =>
                _subject.Draw(_canvasMock.Object, new Point(1, 1), new Point(2, 2), _pixelMock.Object));
        }

        [TestMethod]
        [DataRow(5, 10)]
        [DataRow(10, 5)]
        public void Draw_LineHorizontalLine_CallsCanvas(int x1, int x2)
        {
            var pixelMocks = new List<Mock<IPixel<PixelDataStub>>>();

            IPixel<PixelDataStub> GetPixelMock()
            {
                var mock = new Mock<IPixel<PixelDataStub>>();
                pixelMocks.Add(mock);
                return mock.Object;
            }

            _canvasMock.SetupGet(c => c[It.IsAny<int>(), It.IsAny<int>()])
                .Returns(GetPixelMock);

            var point1 = new Point(x1, 5);
            var point2 = new Point(x2, 5);
            // act
            _subject.Draw(_canvasMock.Object, point1, point2, _pixelMock.Object);

            _canvasMock.Verify(c => c.ValidatePointWithinCanvas(point1), Times.Once);
            _canvasMock.Verify(c => c.ValidatePointWithinCanvas(point2), Times.Once);

            _canvasMock.VerifyGet(c => c[5, 5], Times.Once);
            _canvasMock.VerifyGet(c => c[6, 5], Times.Once);
            _canvasMock.VerifyGet(c => c[7, 5], Times.Once);
            _canvasMock.VerifyGet(c => c[8, 5], Times.Once);
            _canvasMock.VerifyGet(c => c[9, 5], Times.Once);
            _canvasMock.VerifyGet(c => c[10, 5], Times.Once);

            Assert.AreEqual(6, pixelMocks.Count);
            foreach (var pixelMock in pixelMocks)
            {
                pixelMock.Verify(p => p.SetColour(_pixelMock.Object), Times.Exactly(1));
            }

            _canvasMock.VerifyNoOtherCalls();
        }

        [TestMethod]
        [DataRow(5, 10)]
        [DataRow(10, 5)]
        public void Draw_LineVerticalLine_CallsCanvas(int y1, int y2)
        {
            var pixelMocks = new List<Mock<IPixel<PixelDataStub>>>();

            IPixel<PixelDataStub> GetPixelMock()
            {
                var mock = new Mock<IPixel<PixelDataStub>>();
                pixelMocks.Add(mock);
                return mock.Object;
            }

            _canvasMock.SetupGet(c => c[It.IsAny<int>(), It.IsAny<int>()])
                .Returns(GetPixelMock);

            var point1 = new Point(5, y1);
            var point2 = new Point(5, y2);
            // act
            _subject.Draw(_canvasMock.Object, point1, point2, _pixelMock.Object);

            _canvasMock.Verify(c => c.ValidatePointWithinCanvas(point1), Times.Once);
            _canvasMock.Verify(c => c.ValidatePointWithinCanvas(point2), Times.Once);

            _canvasMock.VerifyGet(c => c[5, 5], Times.Once);
            _canvasMock.VerifyGet(c => c[5, 6], Times.Once);
            _canvasMock.VerifyGet(c => c[5, 7], Times.Once);
            _canvasMock.VerifyGet(c => c[5, 8], Times.Once);
            _canvasMock.VerifyGet(c => c[5, 9], Times.Once);
            _canvasMock.VerifyGet(c => c[5, 10], Times.Once);

            Assert.AreEqual(6, pixelMocks.Count);
            foreach (var pixelMock in pixelMocks)
            {
                pixelMock.Verify(p => p.SetColour(_pixelMock.Object), Times.Exactly(1));
            }

            _canvasMock.VerifyNoOtherCalls();
        }
    }
}
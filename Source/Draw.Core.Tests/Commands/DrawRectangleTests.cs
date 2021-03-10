using System;
using Draw.Core.Commands;
using Draw.Core.Commands.Interfaces;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;
using Draw.Core.Tests.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Draw.Core.Tests.Commands
{
    [TestClass]
    public class DrawRectangleTests
    {
        private readonly Mock<ICanvas<PixelDataStub>> _canvasMock;
        private readonly Mock<IDrawStraightLine<PixelDataStub>> _drawStraightLineMock;
        private readonly IDrawRectangle<PixelDataStub> _subject;

        public DrawRectangleTests()
        {
            _canvasMock = new Mock<ICanvas<PixelDataStub>>();
            _drawStraightLineMock = new Mock<IDrawStraightLine<PixelDataStub>>();
            _subject = new DrawRectangle<PixelDataStub>(_drawStraightLineMock.Object, new Mock<ILogger>().Object);
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
        [DataRow(5, 10, 15, 20)]
        [DataRow(10, 5, 20, 15)]
        [DataRow(1, 2, 4, 3)]
        [DataRow(2, 1, 3, 4)]
        public void Draw_CallsDrawLine(int x1, int y1, int x2, int y2)
        {
            var pixel = new PixelStub();
            var point1 = new Point(x1, y1);
            var point2 = new Point(x2, y2);
            // act
            _subject.Draw(_canvasMock.Object, point1, point2, pixel);

            _canvasMock.Verify(c => c.ValidatePointWithinCanvas(point1), Times.Once);
            _canvasMock.Verify(c => c.ValidatePointWithinCanvas(point2), Times.Once);
            _drawStraightLineMock.Verify(
                d => d.Draw(_canvasMock.Object, new Point(x1, y1), new Point(x1, y2), pixel), Times.Once);
            _drawStraightLineMock.Verify(
                d => d.Draw(_canvasMock.Object, new Point(x1, y2), new Point(x2, y2), pixel), Times.Once);
            _drawStraightLineMock.Verify(
                d => d.Draw(_canvasMock.Object, new Point(x2, y2), new Point(x2, y1), pixel), Times.Once);
            _drawStraightLineMock.Verify(
                d => d.Draw(_canvasMock.Object, new Point(x2, y1), new Point(x1, y1), pixel), Times.Once);
            _drawStraightLineMock.VerifyNoOtherCalls();
        }
    }
}
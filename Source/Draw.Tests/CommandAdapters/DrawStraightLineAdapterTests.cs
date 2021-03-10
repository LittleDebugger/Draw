using Draw.Console.Commands;
using Draw.Console.CoreImplementations;
using Draw.Console.Tests.Helpers;
using Draw.Core.Commands.Interfaces;
using Draw.Core.Configuration;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;
using Draw.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Draw.Console.Tests.CommandAdapters
{
    [TestClass]
    public class DrawStraightLineAdapterTests
    {
        private readonly Mock<ICanvasConfiguration<ConsolePixelData>> _canvasConfigurationMock;
        private readonly Mock<ICanvas<ConsolePixelData>> _canvasMock;
        private readonly Mock<IDrawStraightLine<ConsolePixelData>> _drawStraightLineMock;
        private readonly DrawStraightLineAdapter _subject;

        public DrawStraightLineAdapterTests()
        {
            _canvasMock = new Mock<ICanvas<ConsolePixelData>>();
            _canvasConfigurationMock = new Mock<ICanvasConfiguration<ConsolePixelData>>();
            _drawStraightLineMock = new Mock<IDrawStraightLine<ConsolePixelData>>();
            _subject = new DrawStraightLineAdapter(
                _drawStraightLineMock.Object,
                new Mock<ILogger>().Object,
                _canvasConfigurationMock.Object);
        }

        [TestMethod]
        public void Execute_ValidatesCorrectNumberOfArguments()
        {
            Assert.ThrowsException<ValidationException>(() =>
                _subject.Execute(_canvasMock.Object, new ConsoleInputContextStub { InputParts = new string[4] }));
            Assert.ThrowsException<ValidationException>(() =>
                _subject.Execute(_canvasMock.Object, new ConsoleInputContextStub { InputParts = new string[6] }));
        }

        [TestMethod]
        public void Execute_ValidatesCanvasProvided()
        {
            Assert.ThrowsException<ValidationException>(() =>
                _subject.Execute(null, new ConsoleInputContextStub { InputParts = new string[5] }));
        }

        [TestMethod]
        public void Execute_DimensionsLessThanMaximums_CreatesCanvas()
        {
            var pixelMock = new Mock<IPixel<ConsolePixelData>>();

            _canvasConfigurationMock.SetupGet(c => c.DefaultForegroundPixel).Returns(pixelMock.Object);

            // act
            _subject.Execute(_canvasMock.Object,
                new ConsoleInputContextStub { InputParts = new[] { "l", "1", "2", "3", "4" } });

            _drawStraightLineMock.Verify(c => c.Draw(
                    _canvasMock.Object,
                    new Point(1, 2),
                    new Point(3, 4),
                    pixelMock.Object),
                Times.Once);
        }
    }
}
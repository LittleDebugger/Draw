using Draw.Console.Commands;
using Draw.Console.CoreImplementations;
using Draw.Console.IO;
using Draw.Console.Tests.Helpers;
using Draw.Core.Commands.Interfaces;
using Draw.Core.Configuration;
using Draw.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Draw.Console.Tests.CommandAdapters
{
    [TestClass]
    public class CreateCanvasAdapterTests
    {
        private readonly Mock<ICanvasConfiguration<ConsolePixelData>> _canvasConfigurationMock;
        private readonly Mock<ICreateCanvas<ConsolePixelData>> _createCanvasMock;
        private readonly CreateCanvasAdapter _subject;

        private readonly int height = 20;
        private readonly int width = 15;

        public CreateCanvasAdapterTests()
        {
            _canvasConfigurationMock = new Mock<ICanvasConfiguration<ConsolePixelData>>();
            _createCanvasMock = new Mock<ICreateCanvas<ConsolePixelData>>();
            _subject = new CreateCanvasAdapter(
                _createCanvasMock.Object,
                new Mock<ILogger>().Object,
                _canvasConfigurationMock.Object);
        }

        [TestMethod]
        public void Execute_ValidatesCorrectNumberOfArguments()
        {
            Assert.ThrowsException<ValidationException>(() =>
                _subject.Execute(null, new ConsoleInputContextStub { InputParts = new string[2] }));
            Assert.ThrowsException<ValidationException>(() =>
                _subject.Execute(null, new ConsoleInputContextStub { InputParts = new string[4] }));
        }

        [TestMethod]
        public void Execute_ValidatesMaxHeight()
        {
            _canvasConfigurationMock.SetupGet(c => c.MaxHeight).Returns(height - 1);

            Assert.ThrowsException<ValidationException>(() =>
                _subject.Execute(null,
                    new ConsoleInputContextStub { InputParts = new[] { "a", width.ToString(), height.ToString() } }));
        }

        [TestMethod]
        public void Execute_ValidatesMaxWidth()
        {
            _canvasConfigurationMock.SetupGet(c => c.MaxWidth).Returns(width - 1);

            Assert.ThrowsException<ValidationException>(() =>
                _subject.Execute(null,
                    new ConsoleInputContextStub { InputParts = new[] { "a", width.ToString(), height.ToString() } }));
        }

        [TestMethod]
        public void Execute_NoMaxWidthOrHeight_CreatesCanvas()
        {
            _subject.Execute(null, new ConsoleInputContext($"a {width} {height}"));

            _createCanvasMock.Verify(c => c.Create(width, height), Times.Once);
        }

        [TestMethod]
        public void Execute_DimensionsLessThanMaximums_CreatesCanvas()
        {
            _canvasConfigurationMock.SetupGet(c => c.MaxWidth).Returns(width + 1);
            _canvasConfigurationMock.SetupGet(c => c.MaxHeight).Returns(height + 1);

            // act
            _subject.Execute(null,
                new ConsoleInputContextStub { InputParts = new[] { "a", width.ToString(), height.ToString() } });

            _createCanvasMock.Verify(c => c.Create(width, height), Times.Once);
        }
    }
}
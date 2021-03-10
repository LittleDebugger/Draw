using Draw.Console.Commands.Interfaces;
using Draw.Console.CoreImplementations;
using Draw.Console.Tests.Helpers;
using Draw.Core.CoreInterfaces;
using Draw.Core.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Draw.Console.Tests
{
    [TestClass]
    internal class ConsoleCommandInvokerTests
    {
        private readonly Mock<ICanvas<ConsolePixelData>> _canvasMock;
        private readonly ConsoleInputContextStub _consoleInputContextStub;
        private readonly Mock<ICreateCanvasAdapter> _createCanvasMock;
        private readonly Mock<IDrawRectangleAdapter> _drawRectangleMock;
        private readonly Mock<IDrawStraightLineAdapter> _drawStraightLineMock;
        private readonly Mock<IFillAreaAdapter> _fillAreaMock;

        private readonly ConsoleCommandInvoker _subject;

        public ConsoleCommandInvokerTests()
        {
            _createCanvasMock = new Mock<ICreateCanvasAdapter>();
            _drawStraightLineMock = new Mock<IDrawStraightLineAdapter>();
            _drawRectangleMock = new Mock<IDrawRectangleAdapter>();
            _fillAreaMock = new Mock<IFillAreaAdapter>();
            _canvasMock = new Mock<ICanvas<ConsolePixelData>>();
            _consoleInputContextStub = new ConsoleInputContextStub();

            _createCanvasMock.Setup(c => c.Execute(null, _consoleInputContextStub))
                .Returns(_canvasMock.Object);
            _drawStraightLineMock.Setup(c => c.Execute(null, _consoleInputContextStub))
                .Returns(_canvasMock.Object);
            _drawRectangleMock.Setup(c => c.Execute(null, _consoleInputContextStub))
                .Returns(_canvasMock.Object);
            _fillAreaMock.Setup(c => c.Execute(null, _consoleInputContextStub))
                .Returns(_canvasMock.Object);


            _subject = new ConsoleCommandInvoker(
                _createCanvasMock.Object,
                _drawStraightLineMock.Object,
                _drawRectangleMock.Object,
                _fillAreaMock.Object);
        }

        [TestMethod]
        [DataRow("q")]
        [DataRow("Q")]
        public void Invoke_Quit_ReturnsNulLCanvas(string command)
        {
            _consoleInputContextStub.Command = command;

            // act
            var result = _subject.Execute(_canvasMock.Object, _consoleInputContextStub);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        [DataRow("c")]
        [DataRow("C")]
        public void Invoke_CreateCanvas_CreateCanvasCalled(string command)
        {
            _consoleInputContextStub.Command = command;

            // act
            var result = _subject.Execute(_canvasMock.Object, _consoleInputContextStub);

            Assert.AreEqual(_canvasMock.Object, result);
            _createCanvasMock.Verify();
        }

        [TestMethod]
        [DataRow("l")]
        [DataRow("L")]
        public void Invoke_DrawStraightLine_DrawStraightLineCalled(string command)
        {
            _consoleInputContextStub.Command = command;

            // act
            var result = _subject.Execute(_canvasMock.Object, _consoleInputContextStub);

            Assert.AreEqual(_canvasMock.Object, result);
            _drawStraightLineMock.Verify();
        }

        [TestMethod]
        [DataRow("r")]
        [DataRow("R")]
        public void Invoke_DrawRectangle_DrawRectangleCalled(string command)
        {
            _consoleInputContextStub.Command = command;

            // act
            var result = _subject.Execute(_canvasMock.Object, _consoleInputContextStub);

            Assert.AreEqual(_canvasMock.Object, result);
            _drawRectangleMock.Verify();
        }

        [TestMethod]
        [DataRow("b")]
        [DataRow("B")]
        public void Invoke_FillArea_FillAreaCalled(string command)
        {
            _consoleInputContextStub.Command = command;

            // act
            var result = _subject.Execute(_canvasMock.Object, _consoleInputContextStub);

            Assert.AreEqual(_canvasMock.Object, result);
            _fillAreaMock.Verify();
        }

        [TestMethod]
        [DataRow("a")]
        [DataRow("c")]
        [DataRow("6")]
        [DataRow("!")]
        public void Invoke_UnknownCommand_ThrowsValidationException(string command)
        {
            _consoleInputContextStub.Command = command;

            // act
            Assert.ThrowsException<ValidationException>(() =>
                _subject.Execute(_canvasMock.Object, _consoleInputContextStub));
        }
    }
}
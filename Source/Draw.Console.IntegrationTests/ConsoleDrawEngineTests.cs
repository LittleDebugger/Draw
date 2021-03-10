using Draw.Console.CoreImplementations;
using Draw.Console.Infrastructure;
using Draw.Console.IntegrationTests.Helpers;
using Draw.Console.IO;
using Draw.Console.IO.Interfaces;
using Draw.Core.Configuration;
using Draw.Core.CoreInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Draw.Console.IntegrationTests
{
    [TestClass]
    public class ConsoleDrawEngineTests
    {
        private readonly Mock<IReceiver<ConsoleInputContext>> _inputMock;
        private readonly Mock<IConsoleWriter> _senderMock;
        private readonly ConsoleDrawEngine _subject;

        public ConsoleDrawEngineTests()
        {
            _senderMock = new Mock<IConsoleWriter>();
            _inputMock = new Mock<IReceiver<ConsoleInputContext>>();

            var serviceProvider = new ServiceCollection()
                .RegisterServices();

            // register mocks and stubs
            serviceProvider.AddSingleton(_senderMock.Object);
            serviceProvider.AddSingleton(_inputMock.Object);
            serviceProvider.AddSingleton<ICanvasConfiguration<ConsolePixelData>>(new CanvasConfigurationStub());

            _subject = serviceProvider
                .BuildServiceProvider()
                .GetService<ConsoleDrawEngine>();
        }

        [TestMethod]
        public void Start_CreateCanvasCalled_Rendered()
        {
            _inputMock.SetupSequence(c => c.ReceiveInput())
                .Returns(new ConsoleInputContext(TestData.Commands[0]))
                .Returns(new ConsoleInputContext("Q"));

            // act
            _subject.Start();

            // assert
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedCanvasOutput));
        }

        [TestMethod]
        public void Start_LineDrawn_Rendered()
        {
            _inputMock.SetupSequence(c => c.ReceiveInput())
                .Returns(new ConsoleInputContext(TestData.Commands[0]))
                .Returns(new ConsoleInputContext(TestData.Commands[1]))
                .Returns(new ConsoleInputContext("Q"));

            // act
            _subject.Start();

            // assert
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedCanvasOutput));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedLine1Output));
        }

        [TestMethod]
        public void Start_AnotherLineDrawn_Rendered()
        {
            _inputMock.SetupSequence(c => c.ReceiveInput())
                .Returns(new ConsoleInputContext(TestData.Commands[0]))
                .Returns(new ConsoleInputContext(TestData.Commands[1]))
                .Returns(new ConsoleInputContext(TestData.Commands[2]))
                .Returns(new ConsoleInputContext("Q"));

            // act
            _subject.Start();

            // assert
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedCanvasOutput));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedLine1Output));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedLine2Output));
        }

        [TestMethod]
        public void Start_RectangleDrawn_Rendered()
        {
            _inputMock.SetupSequence(c => c.ReceiveInput())
                .Returns(new ConsoleInputContext(TestData.Commands[0]))
                .Returns(new ConsoleInputContext(TestData.Commands[1]))
                .Returns(new ConsoleInputContext(TestData.Commands[2]))
                .Returns(new ConsoleInputContext(TestData.Commands[3]))
                .Returns(new ConsoleInputContext("Q"));

            // act
            _subject.Start();

            // assert
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedCanvasOutput));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedLine1Output));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedLine2Output));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedRectangleOutput));
        }

        [TestMethod]
        public void Start_FillArea_Rendered()
        {
            _inputMock.SetupSequence(c => c.ReceiveInput())
                .Returns(new ConsoleInputContext(TestData.Commands[0]))
                .Returns(new ConsoleInputContext(TestData.Commands[1]))
                .Returns(new ConsoleInputContext(TestData.Commands[2]))
                .Returns(new ConsoleInputContext(TestData.Commands[3]))
                .Returns(new ConsoleInputContext(TestData.Commands[4]))
                .Returns(new ConsoleInputContext("Q"));

            // act
            _subject.Start();

            // assert
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedCanvasOutput));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedLine1Output));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedLine2Output));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedRectangleOutput));
            _senderMock.Verify(c => c.WriteLine(TestData.ExpectedFillOutput));
        }
    }

    internal static class TestData
    {
        public static readonly string[] Commands =
        {
            "C 20 4",
            "L 1 2 6 2",
            "L 6 3 6 4",
            "R 14 1 18 3",
            "B 10 3 o"
        };

        public static readonly string ExpectedCanvasOutput =
            @"----------------------
|                    |
|                    |
|                    |
|                    |
----------------------
";

        public static readonly string ExpectedFillOutput =
            @"----------------------
|oooooooooooooxxxxxoo|
|xxxxxxooooooox   xoo|
|     xoooooooxxxxxoo|
|     xoooooooooooooo|
----------------------
";

        public static readonly string ExpectedLine1Output =
            @"----------------------
|                    |
|xxxxxx              |
|                    |
|                    |
----------------------
";

        public static readonly string ExpectedLine2Output =
            @"----------------------
|                    |
|xxxxxx              |
|     x              |
|     x              |
----------------------
";

        public static readonly string ExpectedRectangleOutput =
            @"----------------------
|             xxxxx  |
|xxxxxx       x   x  |
|     x       xxxxx  |
|     x              |
----------------------
";
    }
}
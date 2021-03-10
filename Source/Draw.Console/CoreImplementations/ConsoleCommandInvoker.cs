using Draw.Console.Commands.Interfaces;
using Draw.Console.IO.Interfaces;
using Draw.Core.CoreInterfaces;
using Draw.Core.Exceptions;

namespace Draw.Console.CoreImplementations
{
    internal class ConsoleCommandInvoker : ICommandInvoker<ConsolePixelData, IConsoleInputContext>
    {
        private readonly ICreateCanvasAdapter _createCanvas;
        private readonly IDrawRectangleAdapter _drawRectangle;
        private readonly IDrawStraightLineAdapter _drawStraightLine;
        private readonly IFillAreaAdapter _fillArea;

        public ConsoleCommandInvoker(
            ICreateCanvasAdapter createCanvas,
            IDrawStraightLineAdapter drawStraightLine,
            IDrawRectangleAdapter drawRectangle,
            IFillAreaAdapter fillArea)
        {
            _createCanvas = createCanvas;
            _drawStraightLine = drawStraightLine;
            _drawRectangle = drawRectangle;
            _fillArea = fillArea;
        }

        public ICanvas<ConsolePixelData> Execute(ICanvas<ConsolePixelData> canvas, IConsoleInputContext inputContext)
        {
            switch (inputContext.Command)
            {
                case "Q":
                    return null;
                case "C":
                    return _createCanvas.Execute(canvas, inputContext);
                case "L":
                    return _drawStraightLine.Execute(canvas, inputContext);
                case "R":
                    return _drawRectangle.Execute(canvas, inputContext);

                case "B":
                    return _fillArea.Execute(canvas, inputContext);
                default:
                    throw new ValidationException($"'{inputContext.Command}' command not recognized.");
            }
        }
    }
}
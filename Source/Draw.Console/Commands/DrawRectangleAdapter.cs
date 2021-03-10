using Draw.Console.Commands.Interfaces;
using Draw.Console.CoreImplementations;
using Draw.Console.IO.Interfaces;
using Draw.Core.Commands.Interfaces;
using Draw.Core.Configuration;
using Draw.Core.CoreInterfaces;
using Microsoft.Extensions.Logging;

namespace Draw.Console.Commands
{
    internal class DrawRectangleAdapter : ConsoleCommandAdapterBase<IDrawRectangle<ConsolePixelData>>,
        IDrawRectangleAdapter
    {
        private readonly ICanvasConfiguration<ConsolePixelData> _canvasConfiguration;

        public DrawRectangleAdapter(
            IDrawRectangle<ConsolePixelData> coreCommand,
            ILogger logger, ICanvasConfiguration<ConsolePixelData> canvasConfiguration)
            : base(coreCommand, logger)
        {
            _canvasConfiguration = canvasConfiguration;
        }

        protected override void InitialValidation(ICanvas<ConsolePixelData> canvas, IConsoleInputContext inputContext)
        {
            ValidateCanvas(canvas);
            ValidateNumberOfInputParts(inputContext, 5, "Fill Line");
        }

        protected override ICanvas<ConsolePixelData> ExecuteInternal(
            ICanvas<ConsolePixelData> canvas,
            IConsoleInputContext inputContext)
        {
            // Currently only default supported.
            var defaultForegroundPixel = _canvasConfiguration.DefaultForegroundPixel;

            CoreCommand.Draw(
                canvas,
                ParsePoint(inputContext, 1, 2),
                ParsePoint(inputContext, 3, 4),
                defaultForegroundPixel);

            return canvas;
        }
    }
}
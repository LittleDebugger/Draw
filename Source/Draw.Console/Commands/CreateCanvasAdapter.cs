using Draw.Console.Commands.Interfaces;
using Draw.Console.CoreImplementations;
using Draw.Console.IO.Interfaces;
using Draw.Core.Commands.Interfaces;
using Draw.Core.Configuration;
using Draw.Core.CoreInterfaces;
using Draw.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Draw.Console.Commands
{
    internal class CreateCanvasAdapter : ConsoleCommandAdapterBase<ICreateCanvas<ConsolePixelData>>,
        ICreateCanvasAdapter
    {
        private readonly ICanvasConfiguration<ConsolePixelData> _canvasConfiguration;

        public CreateCanvasAdapter(
            ICreateCanvas<ConsolePixelData> coreCommand,
            ILogger logger,
            ICanvasConfiguration<ConsolePixelData> canvasConfiguration)
            : base(coreCommand, logger)
        {
            _canvasConfiguration = canvasConfiguration;
        }

        protected override void InitialValidation(
            ICanvas<ConsolePixelData> canvas,
            IConsoleInputContext inputContext)
        {
            ValidateNumberOfInputParts(inputContext, 3, "Create Canvas");
        }

        protected override ICanvas<ConsolePixelData> ExecuteInternal(
            ICanvas<ConsolePixelData> canvas,
            IConsoleInputContext inputContext)
        {
            var dimensions = ParsePoint(inputContext, 1, 2);

            if (_canvasConfiguration.MaxHeight.HasValue && dimensions.Y > _canvasConfiguration.MaxHeight)
            {
                throw new ValidationException($"Maximum height is {_canvasConfiguration.MaxHeight}.");
            }

            if (_canvasConfiguration.MaxWidth.HasValue && dimensions.X > _canvasConfiguration.MaxWidth)
            {
                throw new ValidationException($"Maximum width is {_canvasConfiguration.MaxWidth}.");
            }

            return CoreCommand.Create(dimensions.X, dimensions.Y);
        }
    }
}
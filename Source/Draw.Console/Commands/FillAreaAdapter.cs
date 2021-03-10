using Draw.Console.Commands.Interfaces;
using Draw.Console.CoreImplementations;
using Draw.Console.IO.Interfaces;
using Draw.Core.Commands.Interfaces;
using Draw.Core.CoreInterfaces;
using Draw.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Draw.Console.Commands
{
    internal class FillAreaAdapter : ConsoleCommandAdapterBase<IFillArea<ConsolePixelData>>, IFillAreaAdapter
    {
        public FillAreaAdapter(
            IFillArea<ConsolePixelData> coreCommand,
            ILogger logger)
            : base(coreCommand, logger)
        {
        }

        protected override void InitialValidation(
            ICanvas<ConsolePixelData> canvas,
            IConsoleInputContext inputContext)
        {
            ValidateCanvas(canvas);
            ValidateNumberOfInputParts(inputContext, 4, "Fill Area");

            if (inputContext.InputParts[3].Length != 1)
            {
                throw new ValidationException("Colour must be a single character.");
            }
        }

        protected override ICanvas<ConsolePixelData> ExecuteInternal(
            ICanvas<ConsolePixelData> canvas,
            IConsoleInputContext inputContext)
        {
            CoreCommand.Fill(
                canvas,
                ParsePoint(inputContext, 1, 2),
                ParseColour(inputContext, 3));

            return canvas;
        }
    }
}
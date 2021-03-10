using System;
using Draw.Console.CoreImplementations;
using Draw.Console.IO.Interfaces;
using Draw.Core.CoreBases;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;
using Draw.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Draw.Console.Commands
{
    internal abstract class ConsoleCommandAdapterBase<TCoreCommand>
        : CommandAdapterBase<TCoreCommand, IConsoleInputContext, ConsolePixelData>
    {
        protected ConsoleCommandAdapterBase(TCoreCommand coreCommand, ILogger logger)
            : base(coreCommand, logger)
        {
        }

        protected Point ParsePoint(IConsoleInputContext inputContext, int xIndex, int yIndex)
        {
            if (inputContext?.InputParts == null)
            {
                throw new ArgumentException("Input parts not provided.");
            }

            var inputPartLength = inputContext.InputParts?.Length;
            if (inputPartLength < xIndex)
            {
                throw new InvalidOperationException($"InputPart.Length: {inputPartLength}, xIndex: {xIndex}.");
            }

            var xString = inputContext.InputParts[xIndex];
            if (!int.TryParse(xString, out var x))
            {
                throw new ValidationException($"'{xString}' in not a valid x value.");
            }

            if (inputPartLength < yIndex)
            {
                throw new InvalidOperationException($"inputPartLength: {inputPartLength}, yIndex: {yIndex}.");
            }

            var yString = inputContext.InputParts[yIndex];
            if (!int.TryParse(yString, out var y))
            {
                throw new ValidationException($"'{yString}' in not a valid y value.");
            }

            return new Point(x, y);
        }

        protected static ConsolePixel ParseColour(IConsoleInputContext inputContext, int partIndex)
        {
            if (inputContext?.InputParts == null)
            {
                throw new ArgumentException("inputContext or InputParts null.");
            }

            var inputPartLength = inputContext.InputParts?.Length;
            if (inputPartLength < partIndex)
            {
                throw new InvalidOperationException($"inputPartLength: {inputPartLength}, partIndex: {partIndex}");
            }

            return new ConsolePixel(new ConsolePixelData
            {
                Colour = inputContext.InputParts[partIndex][0]
            });
        }

        protected void ValidateNumberOfInputParts(IConsoleInputContext inputContext, int expected, string commandName)
        {
            if (inputContext?.InputParts?.Length != expected)
            {
                throw new ValidationException($"'{commandName}' takes {expected - 1} arguments.");
            }
        }

        protected void ValidateCanvas(ICanvas<ConsolePixelData> canvas)
        {
            if (canvas == null)
            {
                throw new ValidationException("Please create a canvas.");
            }
        }
    }
}
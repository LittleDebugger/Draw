using System;
using Draw.Core.Commands.Interfaces;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;
using Draw.Core.Exceptions;
using Draw.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Draw.Core.Commands
{
    public class DrawStraightLine<TPixelData> : IDrawStraightLine<TPixelData>
        where TPixelData : IPixelData
    {
        private readonly ILogger _logger;

        public DrawStraightLine(ILogger logger)
        {
            _logger = logger;
        }

        public void Draw(ICanvas<TPixelData> canvas, Point start, Point end, IPixel<TPixelData> colour)
        {
            _logger.LogDebug($"[DrawStraightLine] start: {start}, end: {end}, colour: {colour}");

            Guard.ThrowIfNull(canvas, nameof(canvas));
            Guard.ThrowIfNull(colour, nameof(colour));
            canvas.ValidatePointWithinCanvas(start);
            canvas.ValidatePointWithinCanvas(end);

            if (start.X != end.X && start.Y != end.Y)
            {
                throw new ValidationException("Only horizontal and vertical lines are supported.");
            }

            if (start.X == end.X)
            {
                HorizontalLine(canvas, start, end, colour);
            }
            else
            {
                VerticalLine(canvas, start, end, colour);
            }
        }

        private static void HorizontalLine(ICanvas<TPixelData> canvas, Point start, Point end, IPixel<TPixelData> pixel)
        {
            var min = Math.Min(start.Y, end.Y);
            var max = Math.Max(start.Y, end.Y);
            for (var y = min; y <= max; y++)
            {
                canvas[start.X, y].SetColour(pixel);
            }
        }

        private static void VerticalLine(ICanvas<TPixelData> canvas, Point start, Point end, IPixel<TPixelData> pixel)
        {
            var min = Math.Min(start.X, end.X);
            var max = Math.Max(start.X, end.X);

            for (var x = min; x <= max; x++)
            {
                canvas[x, start.Y].SetColour(pixel);
            }
        }
    }
}
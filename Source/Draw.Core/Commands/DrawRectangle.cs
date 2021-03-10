using Draw.Core.Commands.Interfaces;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;
using Draw.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Draw.Core.Commands
{
    public class DrawRectangle<TPixelData> : IDrawRectangle<TPixelData>
        where TPixelData : IPixelData
    {
        private readonly IDrawStraightLine<TPixelData> _drawStraightLine;
        private readonly ILogger _logger;

        public DrawRectangle(IDrawStraightLine<TPixelData> drawStraightLine, ILogger logger)
        {
            _drawStraightLine = drawStraightLine;
            _logger = logger;
        }

        public void Draw(ICanvas<TPixelData> canvas, Point start, Point end, IPixel<TPixelData> colour)
        {
            _logger.LogDebug($"[DrawRectangle] start: {start}, end: {end}, colour: {colour}");

            Guard.ThrowIfNull(canvas, nameof(canvas));
            Guard.ThrowIfNull(colour, nameof(colour));
            canvas.ValidatePointWithinCanvas(start);
            canvas.ValidatePointWithinCanvas(end);

            var corners = new[]
            {
                start,
                new Point(start.X, end.Y),
                end,
                new Point(end.X, start.Y)
            };

            DrawSide(0, 1);
            DrawSide(1, 2);
            DrawSide(2, 3);
            DrawSide(3, 0);

            void DrawSide(int cornerIndex1, int cornerIndex2)
            {
                _drawStraightLine.Draw(canvas, corners[cornerIndex1], corners[cornerIndex2], colour);
            }
        }
    }
}
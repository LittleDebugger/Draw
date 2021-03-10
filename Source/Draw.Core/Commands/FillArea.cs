using System.Collections.Generic;
using System.Linq;
using Draw.Core.Commands.Interfaces;
using Draw.Core.CoreBases;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;
using Draw.Core.Helpers;
using Microsoft.Extensions.Logging;

namespace Draw.Core.Commands
{
    public class FillArea<TPixelData> : IFillArea<TPixelData>
        where TPixelData : IPixelData
    {
        private readonly ILogger _logger;

        public FillArea(ILogger logger)
        {
            _logger = logger;
        }

        public void Fill(ICanvas<TPixelData> canvas, Point target, PixelBase<TPixelData> colour)
        {
            _logger.LogDebug($"[FillArea] target: {target}, colour: {colour}");

            Guard.ThrowIfNull(canvas, nameof(canvas));
            Guard.ThrowIfNull(colour, nameof(colour));
            canvas.ValidatePointWithinCanvas(target);

            // needs to be a clone otherwise the colour comparision would be inconsistent
            var targetPixelClone = canvas[target.X, target.Y].Clone();

            var visited = new HashSet<Point>();
            var toVisit = new Queue<Point>();
            toVisit.Enqueue(target);

            do
            {
                target = toVisit.Dequeue();

                if (!canvas[target.X, target.Y].IsColourMatch(targetPixelClone))
                {
                    continue;
                }

                canvas[target.X, target.Y].SetColour(colour);
                QueueNextPointToVisit(target.X + 1, target.Y);
                QueueNextPointToVisit(target.X - 1, target.Y);
                QueueNextPointToVisit(target.X, target.Y + 1);
                QueueNextPointToVisit(target.X, target.Y - 1);

                visited.Add(target);
            } while (toVisit.Any());

            void QueueNextPointToVisit(int x, int y)
            {
                if (x < 1 || y < 1 || x > canvas.Width || y > canvas.Height)
                {
                    return;
                }

                var newPoint = new Point(x, y);
                if (!visited.Contains(newPoint))
                {
                    toVisit.Enqueue(newPoint);
                }
            }
        }
    }
}
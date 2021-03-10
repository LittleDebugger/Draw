using Draw.Core.CoreInterfaces;
using Draw.Core.Exceptions;

namespace Draw.Core.Entities
{
    public class Canvas<TPixelData> : ICanvas<TPixelData>
        where TPixelData : IPixelData
    {
        public Canvas(int width, int height, IPixel<TPixelData> defaultPixel)
        {
            Width = width;
            Height = height;
            Area = new IPixel<TPixelData>[width, height];

            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                Area[x, y] = defaultPixel.Clone();
            }
        }

        private IPixel<TPixelData>[,] Area { get; }

        public int Width { get; }
        public int Height { get; }

        public IPixel<TPixelData> this[int x, int y]
        {
            get
            {
                ValidateWithinCanvas(x, y);
                return Area[x - 1, y - 1];
            }
        }

        public void ValidatePointWithinCanvas(Point point)
        {
            ValidateWithinCanvas(point.X, point.Y);
        }

        private void ValidateWithinCanvas(int x, int y)
        {
            if (x < 1 || x > Width || y < 1 || y > Height)
            {
                throw new ValidationException(
                    $"Point x: {x}, y: {y} is invalid for Canvas; width: {Width}, height: {Height}.");
            }
        }
    }
}
using Draw.Core.Entities;

namespace Draw.Core.CoreInterfaces
{
    public interface ICanvas<TPixelData>
        where TPixelData : IPixelData
    {
        int Width { get; }
        int Height { get; }
        IPixel<TPixelData> this[int x, int y] { get; }
        void ValidatePointWithinCanvas(Point point);
    }
}
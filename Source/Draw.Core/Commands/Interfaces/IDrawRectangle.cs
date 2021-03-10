using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;

namespace Draw.Core.Commands.Interfaces
{
    public interface IDrawRectangle<TPixelData>
        where TPixelData : IPixelData
    {
        void Draw(ICanvas<TPixelData> canvas, Point start, Point end, IPixel<TPixelData> colour);
    }
}
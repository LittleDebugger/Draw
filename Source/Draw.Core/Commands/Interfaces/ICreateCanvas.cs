using Draw.Core.CoreInterfaces;

namespace Draw.Core.Commands.Interfaces
{
    public interface ICreateCanvas<TPixelData>
        where TPixelData : IPixelData
    {
        ICanvas<TPixelData> Create(int width, int height);
    }
}
using Draw.Core.CoreBases;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;

namespace Draw.Core.Commands.Interfaces
{
    public interface IFillArea<TPixelData>
        where TPixelData : IPixelData
    {
        void Fill(ICanvas<TPixelData> canvas, Point target, PixelBase<TPixelData> colour);
    }
}
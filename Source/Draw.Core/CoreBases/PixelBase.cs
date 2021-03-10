using Draw.Core.CoreInterfaces;

namespace Draw.Core.CoreBases
{
    public abstract class PixelBase<TPixelData> : IPixel<TPixelData>
        where TPixelData : IPixelData
    {
        public TPixelData Data { get; protected set; }
        public abstract void SetColour(IPixel<TPixelData> referencePixel);
        public abstract bool IsColourMatch(IPixel<TPixelData> referencePixel);
        public abstract IPixel<TPixelData> Clone();
    }
}
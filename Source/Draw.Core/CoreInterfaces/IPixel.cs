namespace Draw.Core.CoreInterfaces
{
    public interface IPixel<TPixelData>
        where TPixelData : IPixelData
    {
        TPixelData Data { get; }
        void SetColour(IPixel<TPixelData> referencePixel);
        bool IsColourMatch(IPixel<TPixelData> referencePixel);
        IPixel<TPixelData> Clone();
    }
}
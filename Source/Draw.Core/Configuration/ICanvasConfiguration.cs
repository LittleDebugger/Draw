using Draw.Core.CoreInterfaces;

namespace Draw.Core.Configuration
{
    public interface ICanvasConfiguration<TPixelData>
        where TPixelData : IPixelData
    {
        int? MaxWidth { get; }
        int? MaxHeight { get; }
        IPixel<TPixelData> DefaultBackgroundPixel { get; }
        IPixel<TPixelData> DefaultForegroundPixel { get; }
    }
}
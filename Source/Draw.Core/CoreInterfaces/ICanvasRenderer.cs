namespace Draw.Core.CoreInterfaces
{
    public interface ICanvasRenderer<TPixelData>
        where TPixelData : IPixelData
    {
        void Render(ICanvas<TPixelData> canvas);
    }
}
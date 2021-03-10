namespace Draw.Core.CoreInterfaces
{
    public interface ICommandInvoker<TPixelData, in TInput>
        where TPixelData : IPixelData
    {
        ICanvas<TPixelData> Execute(ICanvas<TPixelData> canvas, TInput input);
    }
}
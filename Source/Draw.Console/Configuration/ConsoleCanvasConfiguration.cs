using Draw.Console.CoreImplementations;
using Draw.Core.Configuration;
using Draw.Core.CoreInterfaces;

namespace Draw.Console.Configuration
{
    internal class ConsoleCanvasConfiguration : ICanvasConfiguration<ConsolePixelData>
    {
        public int? MaxWidth => System.Console.WindowWidth - 2;
        public int? MaxHeight => System.Console.WindowHeight - 4;

        public IPixel<ConsolePixelData> DefaultBackgroundPixel =>
            new ConsolePixel(
                new ConsolePixelData
                {
                    Colour = ' '
                });

        public IPixel<ConsolePixelData> DefaultForegroundPixel =>
            new ConsolePixel(
                new ConsolePixelData
                {
                    Colour = 'x'
                });
    }
}
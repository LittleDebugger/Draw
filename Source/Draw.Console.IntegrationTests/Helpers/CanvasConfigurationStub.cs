using Draw.Console.Configuration;
using Draw.Console.CoreImplementations;
using Draw.Core.Configuration;
using Draw.Core.CoreInterfaces;

namespace Draw.Console.IntegrationTests.Helpers
{
    internal class CanvasConfigurationStub : ICanvasConfiguration<ConsolePixelData>
    {
        private readonly ICanvasConfiguration<ConsolePixelData> _actualConfiguration = new ConsoleCanvasConfiguration();

        public int? MaxWidth => null;
        public int? MaxHeight => null;

        public IPixel<ConsolePixelData> DefaultBackgroundPixel => _actualConfiguration.DefaultBackgroundPixel;

        public IPixel<ConsolePixelData> DefaultForegroundPixel => _actualConfiguration.DefaultForegroundPixel;
    }
}
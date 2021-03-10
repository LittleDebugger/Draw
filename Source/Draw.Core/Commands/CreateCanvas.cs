using Draw.Core.Commands.Interfaces;
using Draw.Core.Configuration;
using Draw.Core.CoreInterfaces;
using Draw.Core.Entities;
using Microsoft.Extensions.Logging;

namespace Draw.Core.Commands
{
    public class CreateCanvas<TPixelData> : ICreateCanvas<TPixelData>
        where TPixelData : IPixelData
    {
        private readonly ICanvasConfiguration<TPixelData> _canvasConfiguration;
        private readonly ILogger _logger;

        public CreateCanvas(ICanvasConfiguration<TPixelData> canvasConfiguration, ILogger logger)
        {
            _canvasConfiguration = canvasConfiguration;
            _logger = logger;
        }

        public ICanvas<TPixelData> Create(int width, int height)
        {
            _logger.LogDebug($"[CreateCanvas] width: {width}, height: {height}");
            return new Canvas<TPixelData>(width, height, _canvasConfiguration.DefaultBackgroundPixel);
        }
    }
}
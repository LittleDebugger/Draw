using System;
using Draw.Core.CoreInterfaces;
using Draw.Core.Exceptions;
using Microsoft.Extensions.Logging;

namespace Draw.Core
{
    public class DrawEngine<TPixelData, TCommandInput>
        where TPixelData : IPixelData
        where TCommandInput : IInputContext
    {
        private readonly ICanvasRenderer<TPixelData> _canvasRenderer;
        private readonly ICommandInvoker<TPixelData, TCommandInput> _commandInvoker;
        private readonly ILogger _logger;
        private readonly IReceiver<TCommandInput> _receiver;

        public DrawEngine(
            ICanvasRenderer<TPixelData> canvasRenderer,
            ICommandInvoker<TPixelData, TCommandInput> commandInvoker,
            IReceiver<TCommandInput> receiver,
            ILogger logger)
        {
            _canvasRenderer = canvasRenderer;
            _commandInvoker = commandInvoker;
            _receiver = receiver;
            _logger = logger;
        }

        public void Start()
        {
            ICanvas<TPixelData> canvas = null;

            bool error;
            do
            {
                error = false;
                try
                {
                    var commandInput = _receiver.ReceiveInput();
                    canvas = _commandInvoker.Execute(canvas, commandInput);

                    _canvasRenderer.Render(canvas);
                }
                catch (ValidationException ex)
                {
                    error = true;
                    _logger.LogWarning(ex.Message);
                }
                catch (Exception ex)
                {
                    error = true;

                    // log
                    _logger.LogError("There has been an error. Please contact application support :/", ex);
                }

                // currently just loops until canvas == null. This is not ideal but the class would probably need redesigning to
                // accomodate other implementations, anyway.
            } while (error || canvas != null);
        }
    }
}
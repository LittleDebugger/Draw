using Draw.Console.IO;
using Draw.Core;
using Draw.Core.CoreInterfaces;
using Microsoft.Extensions.Logging;

namespace Draw.Console.CoreImplementations
{
    internal class ConsoleDrawEngine : DrawEngine<ConsolePixelData, ConsoleInputContext>
    {
        public ConsoleDrawEngine(
            ICanvasRenderer<ConsolePixelData> canvasRenderer,
            ICommandInvoker<ConsolePixelData, ConsoleInputContext> commandInvoker,
            IReceiver<ConsoleInputContext> receiver,
            ILogger logger)
            : base(canvasRenderer, commandInvoker, receiver, logger)
        {
        }
    }
}
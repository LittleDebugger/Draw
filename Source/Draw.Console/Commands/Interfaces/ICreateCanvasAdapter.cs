using Draw.Console.CoreImplementations;
using Draw.Console.IO.Interfaces;
using Draw.Core.CoreInterfaces;

namespace Draw.Console.Commands.Interfaces
{
    internal interface ICreateCanvasAdapter
    {
        ICanvas<ConsolePixelData> Execute(ICanvas<ConsolePixelData> canvas, IConsoleInputContext inputContext);
    }
}
using Draw.Console.CoreImplementations;
using Draw.Console.IO.Interfaces;
using Draw.Core.CoreInterfaces;

namespace Draw.Console.Commands.Interfaces
{
    internal interface IDrawStraightLineAdapter
    {
        ICanvas<ConsolePixelData> Execute(ICanvas<ConsolePixelData> canvas, IConsoleInputContext inputContext);
    }
}
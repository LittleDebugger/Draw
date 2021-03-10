using Draw.Core.CoreInterfaces;

namespace Draw.Console.IO.Interfaces
{
    internal interface IConsoleInputContext : IInputContext
    {
        string[] InputParts { get; }
        string Command { get; }
    }
}
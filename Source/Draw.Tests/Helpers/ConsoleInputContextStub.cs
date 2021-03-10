using Draw.Console.IO.Interfaces;

namespace Draw.Console.Tests.Helpers
{
    internal class ConsoleInputContextStub : IConsoleInputContext
    {
        public string[] InputParts { get; set; }
        public string Command { get; set; }
    }
}
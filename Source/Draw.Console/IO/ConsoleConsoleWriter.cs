using Draw.Console.IO.Interfaces;

namespace Draw.Console.IO
{
    public class ConsoleConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }
    }
}
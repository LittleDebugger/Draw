using Draw.Console.IO.Interfaces;
using Draw.Core.Exceptions;

namespace Draw.Console.IO
{
    public class ConsoleInputContext : IConsoleInputContext
    {
        public ConsoleInputContext(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ValidationException("Please receiver a command.");
            }

            InputParts = input.Trim(
            ).Split(" ");

            Command = InputParts[0].ToUpper();
        }

        public string[] InputParts { get; }

        public string Command { get; }

        public override string ToString()
        {
            return $"([ConsoleInputContext]: Command: {Command})";
        }
    }
}
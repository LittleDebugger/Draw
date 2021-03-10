using Draw.Core.CoreInterfaces;

namespace Draw.Console.IO
{
    internal class ConsoleReceiver : IReceiver<ConsoleInputContext>
    {
        public ConsoleInputContext ReceiveInput()
        {
            System.Console.Write("enter command: ");
            var input = System.Console.ReadLine();

            return new ConsoleInputContext(input);
        }
    }
}
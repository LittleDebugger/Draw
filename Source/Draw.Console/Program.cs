using Draw.Console.CoreImplementations;
using Draw.Console.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Draw.Console
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .RegisterServices()
                .BuildServiceProvider();

            var engine = serviceProvider.GetService<ConsoleDrawEngine>();
            engine.Start();
        }
    }
}
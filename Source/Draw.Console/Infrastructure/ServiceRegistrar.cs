using Draw.Console.Commands;
using Draw.Console.Commands.Interfaces;
using Draw.Console.Configuration;
using Draw.Console.CoreImplementations;
using Draw.Console.IO;
using Draw.Console.IO.Interfaces;
using Draw.Core;
using Draw.Core.Configuration;
using Draw.Core.CoreInterfaces;
using Draw.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Draw.Console.Infrastructure
{
    internal static class ServiceRegistrar
    {
        public static ServiceCollection RegisterServices(this ServiceCollection serviceCollection)
        {
            serviceCollection
                .AddTransient<ICreateCanvasAdapter, CreateCanvasAdapter>()
                .AddTransient<IDrawStraightLineAdapter, DrawStraightLineAdapter>()
                .AddTransient<IDrawRectangleAdapter, DrawRectangleAdapter>()
                .AddTransient<IFillAreaAdapter, FillAreaAdapter>()
                .AddTransient<ConsoleDrawEngine>()
                .AddTransient<ICanvasConfiguration<ConsolePixelData>, ConsoleCanvasConfiguration>()
                .AddTransient<ConsoleCanvasConfiguration>()
                .AddTransient<ICanvasRenderer<ConsolePixelData>, ConsoleCanvasRenderer>()
                .AddTransient<IConsoleWriter, ConsoleConsoleWriter>()
                .AddTransient<ICommandInvoker<ConsolePixelData, ConsoleInputContext>, ConsoleCommandInvoker>()
                .AddTransient<IReceiver<ConsoleInputContext>, ConsoleReceiver>()
                .AddTransient<DrawEngine<ConsolePixelData, ConsoleInputContext>, ConsoleDrawEngine>()
                .AddTransient<ILogger, ConsoleLogger>();

            serviceCollection.RegisterCoreServices<ConsolePixelData>();

            return serviceCollection;
        }
    }
}
using Draw.Core.Commands;
using Draw.Core.Commands.Interfaces;
using Draw.Core.CoreInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Draw.Core.Infrastructure
{
    public static class CoreServiceRegistrar
    {
        public static ServiceCollection RegisterCoreServices<TPixelData>(this ServiceCollection serviceCollection)
            where TPixelData : IPixelData
        {
            serviceCollection.AddTransient<ICreateCanvas<TPixelData>, CreateCanvas<TPixelData>>()
                .AddTransient<IDrawStraightLine<TPixelData>, DrawStraightLine<TPixelData>>()
                .AddTransient<IDrawRectangle<TPixelData>, DrawRectangle<TPixelData>>()
                .AddTransient<IFillArea<TPixelData>, FillArea<TPixelData>>();

            return serviceCollection;
        }
    }
}
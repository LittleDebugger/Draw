using System;
using Draw.Core.CoreBases;
using Draw.Core.CoreInterfaces;

namespace Draw.Console.CoreImplementations
{
    internal class ConsolePixel : PixelBase<ConsolePixelData>
    {
        public ConsolePixel(ConsolePixelData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public override void SetColour(IPixel<ConsolePixelData> referencePixelBase)
        {
            if (referencePixelBase == null)
            {
                throw new ArgumentNullException(nameof(referencePixelBase));
            }

            Data.Colour = referencePixelBase.Data.Colour;
        }

        public override bool IsColourMatch(IPixel<ConsolePixelData> referencePixelBase)
        {
            if (referencePixelBase == null)
            {
                throw new ArgumentNullException(nameof(referencePixelBase));
            }

            return Data.Colour == referencePixelBase.Data.Colour;
        }

        public override IPixel<ConsolePixelData> Clone()
        {
            return new ConsolePixel(new ConsolePixelData
            {
                Colour = Data.Colour
            });
        }

        public override string ToString()
        {
            return $"(ConsolePixel: Colour: {Data.Colour})";
        }
    }
}
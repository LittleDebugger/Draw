using Draw.Core.CoreBases;
using Draw.Core.CoreInterfaces;

namespace Draw.Core.Tests.Helpers
{
    public class PixelStub : PixelBase<PixelDataStub>
    {
        public PixelStub()
        {
            Data = new PixelDataStub();
        }

        public override void SetColour(IPixel<PixelDataStub> referencePixel)
        {
            Data.Colour = referencePixel.Data.Colour;
        }

        public override bool IsColourMatch(IPixel<PixelDataStub> referencePixel)
        {
            return Data.Colour == referencePixel.Data.Colour;
        }

        public override IPixel<PixelDataStub> Clone()
        {
            var clone = new PixelStub
            {
                Data = new PixelDataStub
                {
                    Colour = Data.Colour
                }
            };

            return clone;
        }
    }

    public class PixelDataStub : IPixelData
    {
        public int Colour { get; set; }
    }
}
using System;
using System.Text;
using Draw.Console.IO.Interfaces;
using Draw.Core.CoreInterfaces;

namespace Draw.Console.CoreImplementations
{
    internal class ConsoleCanvasRenderer : ICanvasRenderer<ConsolePixelData>
    {
        private readonly IConsoleWriter _consoleWriter;

        public ConsoleCanvasRenderer(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;
        }

        public void Render(ICanvas<ConsolePixelData> canvas)
        {
            if (canvas == null)
            {
                return;
            }

            var sb = new StringBuilder();
            HorizontalBorder(canvas.Width, sb);
            Rows(canvas, sb);
            HorizontalBorder(canvas.Width, sb);
            _consoleWriter.WriteLine(sb.ToString());
        }

        private void HorizontalBorder(int width, StringBuilder sb)
        {
            for (var i = 0; i < width + 2; i++)
            {
                sb.Append("-");
            }

            sb.Append(Environment.NewLine);
        }

        private void Rows(ICanvas<ConsolePixelData> canvas, StringBuilder sb)
        {
            for (var y = 1; y <= canvas.Height; y++)
            {
                sb.Append("|");
                for (var x = 1; x <= canvas.Width; x++)
                {
                    sb.Append(canvas[x, y].Data.Colour);
                }

                sb.Append("|");
                sb.Append(Environment.NewLine);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp
{
    class ImageProcessor
    {
        public static Bitmap Grayscale(Bitmap input)
        {
            Bitmap result = new Bitmap(input.Width, input.Height);
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    Color pixel = input.GetPixel(x, y);
                    int gray = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    result.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }
            return result;
        }

        public static Bitmap Negative(Bitmap input)
        {
            Bitmap result = new Bitmap(input.Width, input.Height);
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    Color pixel = input.GetPixel(x, y);
                    result.SetPixel(x, y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
            return result;
        }

        public static Bitmap Mirror(Bitmap input)
        {
            Bitmap result = new Bitmap(input.Width, input.Height);
            for (int y = 0; y < input.Height; y++)
            {
                for (int x = 0; x < input.Width; x++)
                {
                    result.SetPixel(input.Width - 1 - x, y, input.GetPixel(x, y));
                }
            }
            return result;
        }

        public static Bitmap EdgeDetection(Bitmap input)
        {
            Bitmap result = new Bitmap(input.Width, input.Height);
            int[,] filterX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] filterY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            for (int y = 1; y < input.Height - 1; y++)
            {
                for (int x = 1; x < input.Width - 1; x++)
                {
                    int gx = 0, gy = 0;
                    for (int fy = -1; fy <= 1; fy++)
                    {
                        for (int fx = -1; fx <= 1; fx++)
                        {
                            Color c = input.GetPixel(x + fx, y + fy);
                            int intensity = (c.R + c.G + c.B) / 3;
                            gx += intensity * filterX[fy + 1, fx + 1];
                            gy += intensity * filterY[fy + 1, fx + 1];
                        }
                    }
                    int g = Math.Min(255, (int)Math.Sqrt(gx * gx + gy * gy));
                    result.SetPixel(x, y, Color.FromArgb(g, g, g));
                }
            }
            return result;
        }
    }
}

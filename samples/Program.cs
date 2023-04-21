using System;
using System.Device.Gpio;
using System.Drawing;
using System.Threading;
using CutilloRigby.Device.RgbXmasTree;

namespace samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CancellationTokenSource();

            Console.CancelKeyPress += (source, cancelArgs) => {
                ctx.Cancel();
                cancelArgs.Cancel = true;
            };

            var gpioController = new GpioController();
            var rgbXmasTree = new RgbXmasTree(gpioController, 250000000);
            var brightness = (byte?)0x1; // null; // 0x1;
            var delay = 500;

            Console.WriteLine($"Clock Frequency: {rgbXmasTree.ClockFrequency}Hz");
            
            while (true)
            {
                if (ctx.IsCancellationRequested) break;
                Flash(rgbXmasTree, brightness, delay, ctx);
                if (ctx.IsCancellationRequested) break;
                RotateBottomPixels(rgbXmasTree, brightness, delay, ctx);
                if (ctx.IsCancellationRequested) break;
                RotateAllPixels(rgbXmasTree, brightness, delay, ctx);
            }

            rgbXmasTree.SetPixel(PixelId.All, Color.Black);
            rgbXmasTree.Update();
        }

        static void Flash(RgbXmasTree rgbXmasTree, byte? brightness, int delay, CancellationTokenSource ctx)
        {
            rgbXmasTree.SetPixel(PixelId.Star, Color.White, brightness);

            var cycles = 3; // 3 x 4 half-second delays = 12 Seconds.
            while (cycles-- > 0)
            {
                if (ctx.IsCancellationRequested) break;
                rgbXmasTree.SetPixel(PixelIdExt.Group1, Color.Red, brightness);
                rgbXmasTree.SetPixel(PixelIdExt.Group2, Color.DarkOrange, brightness);
                rgbXmasTree.Update();
                Thread.Sleep(delay);

                if (ctx.IsCancellationRequested) break;
                rgbXmasTree.SetPixel(PixelIdExt.Group1, Color.DarkOrange, brightness);
                rgbXmasTree.SetPixel(PixelIdExt.Group2, Color.Green, brightness);
                rgbXmasTree.Update();
                Thread.Sleep(delay);

                if (ctx.IsCancellationRequested) break;
                rgbXmasTree.SetPixel(PixelIdExt.Group1, Color.Green, brightness);
                rgbXmasTree.SetPixel(PixelIdExt.Group2, Color.Blue, brightness);
                rgbXmasTree.Update();
                Thread.Sleep(delay);

                if (ctx.IsCancellationRequested) break;
                rgbXmasTree.SetPixel(PixelIdExt.Group1, Color.Blue, brightness);
                rgbXmasTree.SetPixel(PixelIdExt.Group2, Color.Red, brightness);
                rgbXmasTree.Update();
                Thread.Sleep(delay);
            }
        }

        static void RotateBottomPixels(RgbXmasTree rgbXmasTree, byte? brightness, int delay, CancellationTokenSource ctx)
        {
            rgbXmasTree.SetPixel(PixelId.Star, Color.White, brightness);
            rgbXmasTree.SetPixel(PixelId.AllExStar, Color.Black, brightness);

            rgbXmasTree.SetPixel(new byte[] { PixelId.FrontLeftTreeBottom, PixelId.RearRightTreeBottom }, Color.Red, brightness);
            rgbXmasTree.SetPixel(new byte[] { PixelId.FrontLeftFinBottom, PixelId.RearRightFinBottom }, Color.DarkOrange, brightness);
            rgbXmasTree.SetPixel(new byte[] { PixelId.FrontRightFinBottom, PixelId.RearLeftFinBottom }, Color.Green, brightness);
            rgbXmasTree.SetPixel(new byte[] { PixelId.FrontRightTreeBottom, PixelId.RearLeftTreeBottom }, Color.Blue, brightness);
            
            rgbXmasTree.Update();
            Thread.Sleep(delay);

            var cycles = 19; // 20 half-second delays (inc. the one above) = 10 Seconds.
            while(cycles-- > 0)
            {
                if (ctx.IsCancellationRequested) break;
                rgbXmasTree.RotateRight(PixelIdExt.Bottom);
                rgbXmasTree.Update();
                Thread.Sleep(delay);
            }
        }

        static void RotateAllPixels(RgbXmasTree rgbXmasTree, byte? brightness, int delay, CancellationTokenSource ctx)
        {
            rgbXmasTree.SetPixel(PixelId.Star, Color.White, brightness);
            rgbXmasTree.SetPixel(PixelId.AllExStar, new Color[] { Color.Red, Color.DarkOrange, Color.Green, Color.Blue, Color.Purple }, brightness);
            rgbXmasTree.Update();
            Thread.Sleep(delay);

            var cycles = 19; // 20 half-second delays (inc. the one above) = 10 Seconds.
            while (cycles-- > 0)
            {
                if (ctx.IsCancellationRequested) break;
                rgbXmasTree.RotateLeft(PixelId.AllExStar);
                rgbXmasTree.Update();
                Thread.Sleep(delay);
            }
        }
    }
}

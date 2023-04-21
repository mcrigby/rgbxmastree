using System;
using System.Device.Gpio;
using System.Device.Spi;
using System.Drawing;
using Iot.Device.Spi;

namespace CutilloRigby.Device.RgbXmasTree
{
    public class RgbXmasTree
    {
        private const byte pixelCount = 25;

        private readonly SoftwareSpi spi;
        private Color[] pixel = new Color[pixelCount];

        public RgbXmasTree(GpioController gpioController, int clockFrequency = 500000)
        {
            var settings = new SpiConnectionSettings(0)
            {
                ClockFrequency = clockFrequency
            };

            spi = new SoftwareSpi(25, -1, 12, gpioController: gpioController, settings: settings);
        }

        public int ClockFrequency 
        {
            get { return spi.ConnectionSettings.ClockFrequency; }
        }
        
        public Color GetPixel(byte pixelId)
        {
            ValidatePixelId(pixelId);
            return pixel[pixelId];
        }
        public void SetPixel(byte pixelId, byte red, byte green, byte blue, byte? brightness = null)
        {
            SetPixel(new byte[] { pixelId }, red, green, blue, brightness);
        }
        public void SetPixel(byte[] pixelId, byte red, byte green, byte blue, byte? brightness = null)
        {
            var color = Color.FromArgb(red, green, blue);

            SetPixel(pixelId, color, brightness);
        }
        public void SetPixel(byte pixelId, Color color, byte? brightness = null)
        {
            SetPixel(new byte[] { pixelId }, color, brightness);
        }
        public void SetPixel(byte[] pixelId, Color color, byte? brightness = null)
        {
            SetPixel(pixelId, new Color[] { color }, brightness);
        }
        public void SetPixel(byte[] pixelId, Color[] color, byte? brightness = null)
        {
            if (color.Length == 0)
                throw new ArgumentException("No Colors specified.");

            int c = 0;

            foreach (var id in pixelId)
            {
                ValidatePixelId(id);

                if (brightness == null)
                    brightness = ColorAToBrightness(color[c].A);
                var alpha = BrightnessToColorA(brightness.Value);

                color[c] = Color.FromArgb(alpha, color[c].R, color[c].G, color[c].B);

                pixel[id] = color[c];

                c++;
                if (c == color.Length) c = 0;
            }
        }

        public byte GetBrightness(byte pixelId)
        {
            ValidatePixelId(pixelId);
            return ColorAToBrightness(pixel[pixelId].A);
        }
        public void SetBrightness(byte pixelId, byte brightness)
        {
            SetBrightness(new byte[] { pixelId }, brightness);
        }
        public void SetBrightness(byte[] pixelId, byte brightness)
        {
            var alpha = BrightnessToColorA(brightness);

            foreach (var id in pixelId)
            {
                ValidatePixelId(id);
                pixel[id] = Color.FromArgb(alpha, pixel[id].R, pixel[id].G, pixel[id].B);
            }
        }

        public void RotateLeft(byte[] pixelId)
        {
            if (pixelId.Length < 2)
                return;

            var carry = pixel[pixelId[0]];

            for (var id = 0; id < pixelId.Length - 1; id++)
            {
                pixel[pixelId[id]] = pixel[pixelId[id + 1]];
            }

            pixel[pixelId[pixelId.Length - 1]] = carry;
        }
        public void RotateRight(byte[] pixelId)
        {
            if (pixelId.Length < 2)
                return;

            var carry = pixel[pixelId[pixelId.Length - 1]];

            for (var id = pixelId.Length - 1; id > 0; id--)
            {
                pixel[pixelId[id]] = pixel[pixelId[id - 1]];
            }

            pixel[pixelId[0]] = carry;
        }

        public void Update()
        {
            var buffer = new byte[109];
            var index = 4;
            
            foreach(var p in pixel)
            {
                var brightness = ColorAToBrightness(p.A);
                Array.Copy(new byte[] { CalcBrightness(brightness), p.B, p.G, p.R }, 0, buffer, index, 4);
                index += 4;
            }

            spi.Write(buffer);
        }

        private static void ValidatePixelId(byte pixelId)
        {
            if (pixelId > pixelCount)
                throw new ArgumentOutOfRangeException(nameof(pixelId), pixelId, $"PixelId should be between 0 and {pixelCount}.");
        }
        private static byte ExtractBrightness(byte brightness)
        {
            return (byte)(0x1f & brightness);
        }
        private static byte CalcBrightness(byte brightness)
        {
            if (brightness > 31)
                throw new ArgumentOutOfRangeException(nameof(brightness), brightness, "Brightness must be between 0 and 31.");

            return (byte)(0xe0 | brightness);
        }

        private static byte ColorAToBrightness(byte colorA)
        {
            return (byte)(colorA >> 3);
        }
        private static byte BrightnessToColorA(byte brightness)
        {
            return (byte)(brightness << 3);
        }
    }
}
using System.Drawing;

namespace CutilloRigby.Device.RgbXmasTree;

public static class RgbXmasTreeExtensions
{
    public static void SetPixel(this IRgbXmasTree rgbXmasTree, byte pixelId, byte red, byte green, byte blue, byte? brightness = null)
    {
        rgbXmasTree.SetPixel(new byte[] { pixelId }, red, green, blue, brightness);
    }
    public static void SetPixel(this IRgbXmasTree rgbXmasTree, byte[] pixelId, byte red, byte green, byte blue, byte? brightness = null)
    {
        var color = Color.FromArgb(red, green, blue);

        rgbXmasTree.SetPixel(pixelId, color, brightness);
    }
    public static void SetPixel(this IRgbXmasTree rgbXmasTree, byte pixelId, Color color, byte? brightness = null)
    {
        rgbXmasTree.SetPixel(new byte[] { pixelId }, color, brightness);
    }
    public static void SetPixel(this IRgbXmasTree rgbXmasTree, byte[] pixelId, Color color, byte? brightness = null)
    {
        rgbXmasTree.SetPixel(pixelId, new Color[] { color }, brightness);
    }

    public static void SetBrightness(this IRgbXmasTree rgbXmasTree, byte pixelId, byte brightness)
    {
        rgbXmasTree.SetBrightness(new byte[] { pixelId }, brightness);
    }
}

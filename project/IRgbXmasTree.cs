using System.Drawing;

namespace CutilloRigby.Device.RgbXmasTree;

public interface IRgbXmasTree
{
    public int ClockFrequency { get; }
    
    public Color GetPixel(byte pixelId);
    public void SetPixel(byte[] pixelId, Color[] color, byte? brightness = null);

    public byte GetBrightness(byte pixelId);
    public void SetBrightness(byte[] pixelId, byte brightness);

    public void RotateLeft(byte[] pixelId);
    public void RotateRight(byte[] pixelId);
    public void Update();
}

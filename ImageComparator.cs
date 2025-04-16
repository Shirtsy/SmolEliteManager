using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public static class ImageComparator
{
    public static float DiffImages(Image<Rgba32> imageA, Image<Rgba32> imageB)
    {
        if (imageA.Size != imageB.Size)
            throw new ArgumentException("Images must be the same size to be compared.");
        return GetPixels<Rgba32>(imageA)
            .Zip(GetPixels<Rgba32>(imageB), DiffPixels)
            .Average();
    }

    public static float DiffImages(String imagePathA, String imagePathB)
    {
        using Image<Rgba32> imageA = Image.Load<Rgba32>(imagePathA);
        using Image<Rgba32> imageB = Image.Load<Rgba32>(imagePathB);
        return DiffImages(imageA, imageB);
    }

    private static float DiffPixels(Rgba32 pixelA, Rgba32 pixelB)
    {
        float rDiff = Math.Abs((pixelA.R - pixelB.R) / 255f);
        float gDiff = Math.Abs((pixelA.G - pixelB.G) / 255f);
        float bDiff = Math.Abs((pixelA.B - pixelB.B) / 255f);
        float rgbDiff = (rDiff + gDiff + bDiff) / 3f;
        float rgbaDiff = rgbDiff * (pixelA.A / 255f) * (pixelB.A / 255f);
        return rgbaDiff;
    }

    private static IEnumerable<TPixel> GetPixels<TPixel>(Image<TPixel> image)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                yield return image[i, j];
            }
        }
    }
}

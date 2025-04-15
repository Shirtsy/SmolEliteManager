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
        float rgbDiff = ((pixelA.R - pixelB.R) / 255)
        + ((pixelA.G - pixelB.G) / 255)
        + ((pixelA.B - pixelB.B) / 255)
        / 3;
        return rgbDiff * (pixelA.A / 255 + pixelB.A / 255);
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

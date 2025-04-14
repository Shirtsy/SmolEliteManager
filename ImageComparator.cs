using SixLabors.ImageSharp;

public static class ImageComparator
{
    public static float CompareImages(Image imageA, Image imageB)
    {
        return 5f;
    }

    public static float CompareImages(String imagePathA, String imagePathB) =>
        CompareImages(Image.Load(imagePathA), Image.Load(imagePathB));
}

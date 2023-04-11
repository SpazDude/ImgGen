using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SixLabors.ImageSharp;

namespace ImgGen.Models
{

    public static class AspectRatioHelpers
    {
        static int BaseWidth = 1600;

        public static int Width(this AspectRatio ratio)
        {
            return BaseWidth;
        }

        public static int Height(this AspectRatio ratio)
        {
            switch (ratio)
            {
                case AspectRatio._16x9:
                    return BaseWidth * 9 / 16;
                case AspectRatio._4x3:
                    return BaseWidth * 3 / 4;
                case AspectRatio._1x1:
                    return BaseWidth;
                case AspectRatio._3x4:
                    return BaseWidth * 4 / 3;
                default:
                    return BaseWidth * 9 / 16;
            }
        }

        public static Color TextColor(this ColorScheme colorScheme)
        {
            switch (colorScheme)
            {
                case ColorScheme.transparent:
                    return Color.Gray;
                default:
                    return Color.Black;
            }
        }

        public static Color BackgroundColor(this ColorScheme colorScheme)
        {
            switch (colorScheme)
            {
                case ColorScheme.normal:
                    return Color.Blue;
                case ColorScheme.warning:
                    return Color.Yellow;
                case ColorScheme.error:
                    return Color.Red;
                case ColorScheme.neutral:
                    return Color.White;
                case ColorScheme.transparent:
                    return Color.Transparent;
                default:
                    return Color.DarkGray;
            }
        }
    }

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ImageSize
    {
        large = 1,
        medium = 2,
        small = 4
    }


    public enum AspectRatio
    {
        _16x9,
        _4x3,
        _1x1,
        _3x4
    }

    public enum ColorScheme
    {
        normal,
        warning,
        error,
        neutral,
        transparent
    }


    public enum ImageFormat
    {
        jpg,
        png
    }
    public class ImageParams
    {
        public string Text { get; set; }
        public string Locale { get; set; }
        public string Aspect { get; set; }
        public ImageSize Size { get; set; }
        public ImageFormat Formmat { get; set; }
    }

    public class ImageResponse
    {
        public string URL { get; set; }
    }
}
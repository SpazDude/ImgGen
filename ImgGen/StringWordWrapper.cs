
using SixLabors.Fonts;

namespace ImgGen
{
    public class StringWordWrapper
    {
        public StringWordWrapper(FontFactory fontFactory)
        {
            _fontFactory = fontFactory;
        }

        private const int font_min_size = 8;
        private const int font_max_size = 1024;
        private readonly FontFactory _fontFactory;

        public int ComputeFontSize(string text, int width, int height)
        {
            int max = font_max_size;
            int min = font_min_size;
            int font_size;

            do
            {
                font_size = (max + min) / 2;
                var rect = TextMeasurer.Measure(text, new RendererOptions(_fontFactory.Font(font_size))
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    ApplyKerning = true,
                    WrappingWidth = width
                });

                if (rect.Height > height || rect.Width > width)
                {
                    max = font_size;
                }
                else
                {
                    min = font_size;
                }

            } while (max - min > 1);
            return min;
        }
    }
}

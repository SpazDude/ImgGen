using System.Linq;
using SixLabors.Fonts;

namespace ImgGen
{
    public class FontFactory
    {
        private readonly FontCollection collection;
        private readonly FontFamily family;

        public FontFactory()
        {
            collection = new FontCollection();

            var fontResourceNames = typeof(FontFactory).Assembly.GetManifestResourceNames();
            foreach (var resourceName in fontResourceNames.Where(x => x.EndsWith(".TTF")))
            {
                var stream = typeof(FontFactory).Assembly.GetManifestResourceStream(resourceName);
                family = collection.Install(stream);
            }
        }

        public Font Font(int size)
        {
            return family.CreateFont(size, FontStyle.Regular);
        }
    }
}
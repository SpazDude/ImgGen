using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using ImgGen.Models;

namespace ImgGen.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestImageController : ControllerBase
    {
        private readonly ILogger<TestImageController> _logger;
        private readonly FontFactory _fontFactory;
        private readonly StringWordWrapper _wrapper;

        public TestImageController(
            ILogger<TestImageController> logger,
            FontFactory fontLibrary,
            StringWordWrapper wrapper)
        {
            _logger = logger;
            _fontFactory = fontLibrary;
            _wrapper = wrapper;
        }

        /// <summary>
        /// Create an image containing the title and subtitle provided
        /// </summary>
        /// <see cref="https://discoverdot.net/projects/imagesharp"/>>
        /// <param name="title">This is the text that will be drawn on the image</param>
        /// <param name="colorScheme"></param>
        /// <param name="aspectRatio"></param>
        /// <param name="imageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(string title, ColorScheme colorScheme = ColorScheme.normal, AspectRatio aspectRatio = AspectRatio._16x9, ImageSize imageSize = ImageSize.large)
        {
            var stream = new MemoryStream();
            try
            {
                Color textColor = colorScheme.TextColor();
                Color bgColor = colorScheme.BackgroundColor();
                var width = aspectRatio.Width() / (int)imageSize;
                var height = aspectRatio.Height() / (int)imageSize;

                var textGraphicsOptions = new TextGraphicsOptions(
                    new GraphicsOptions(),
                    new TextOptions()
                    {
                        ApplyKerning = true,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        WrapTextWidth = width,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                );

                using var image = new Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(width, height);

                var font_size = _wrapper.ComputeFontSize(title, width, height);
                var point = new PointF(0, height / 2 - font_size / 10);

                // Do your drawing in here...
                image.Mutate(x =>
                {
                    x.BackgroundColor(bgColor);
                    x.DrawText(textGraphicsOptions, title, _fontFactory.Font(font_size), textColor, point);
                });

                image.SaveAsPng(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "image/png");
            }
            catch
            {
                stream.Dispose();
                throw;
            }
        }
    }
}

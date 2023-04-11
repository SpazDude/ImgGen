using ImgGen;
using NUnit.Framework;

namespace ImgGen.Tests
{
    public class TestClass
    {
        [Test]
        public void TestStringWordWrapper()
        {
            var wrapper = new StringWordWrapper(new FontFactory());

            var fontSize = wrapper.ComputeFontSize("this is a test", 1600, 900);

            Assert.True(true);
        }
    }
}

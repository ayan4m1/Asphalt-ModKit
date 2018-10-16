using NUnit.Framework;

namespace Asphalt.Tests
{
    [TestFixture]
    public class PluginTests
    {
        [Test]
        public void PluginIsSane()
        {
            var plugin = new AsphaltPlugin();
            Assert.IsNotNull(plugin);
            Assert.True(plugin.GetStatus() == "Initialized!");
        }
    }
}

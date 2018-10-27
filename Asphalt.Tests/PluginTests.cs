using NUnit.Framework;

namespace Asphalt.Tests
{
    [TestFixture]
    public class PluginTests
    {
        [Test]
        public void CanInitialize()
        {
            var plugin = new AsphaltPlugin();
            Assert.IsNotNull(plugin);
            Assert.IsNotNull(AsphaltPlugin.Harmony);
            Assert.True(plugin.GetStatus() == "Initialized!");
        }
    }
}

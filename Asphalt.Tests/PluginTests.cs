using NUnit.Framework;

namespace Asphalt.Tests
{
    [TestFixture]
    public class PluginTests
    {
        [Test]
        public void CanInitialize()
        {
            var plugin = new Asphalt();
            Assert.IsNotNull(plugin);
            Assert.IsNotNull(Asphalt.Harmony);
            Assert.True(plugin.GetStatus() == "Initialized!");
        }
    }
}

using Asphalt.Events;
using NUnit.Framework;
using System;

namespace Asphalt.Tests.Events
{
    [TestFixture]
    public class EventMappingRegistryTests
    {
        [TearDown]
        public void TearDown()
        {
            EventMappingRegistry.UnregisterAll();
        }

        [Test]
        public void CanRegisterMapping()
        {
            EventMappingRegistry.RegisterAll(this);
        }

        [Test]
        public void CanUnregisterMapping()
        {
            EventMappingRegistry.RegisterAll(this);
            var thisType = GetType();
            var eventType = typeof(EventArgs);
            Assert.IsTrue(EventMappingRegistry.IsRegistered(eventType));
            EventMappingRegistry.Unregister(eventType);
            Assert.IsFalse(EventMappingRegistry.IsRegistered(eventType));
        }

        [Test]
        public void CanRegisterMultipleMappings()
        {
            EventMappingRegistry.RegisterAll(this);
        }

        [Test]
        public void CanHandleEvent()
        {
            var evt = EventArgs.Empty;
            EventMappingRegistry.RegisterAll(this);
            EventMappingRegistry.Handle(ref evt);
        }

        [EventHandler]
        public void EventHandlerTest(EventArgs e)
        {
            Assert.AreEqual(e, EventArgs.Empty);
        }
    }
}

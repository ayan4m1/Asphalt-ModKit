using Asphalt.Events;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Items;
using NUnit.Framework;
using System;

namespace Asphalt.Tests.Events
{
    [TestFixture]
    public class EventPatchRegistryTests
    {
        [TearDown]
        public void ResetRegistry()
        {
            EventPatchRegistry.UnregisterAll();
        }

        [Test]
        public void CanRegisterPatch()
        {
            EventPatchRegistry.Register(typeof(PatchRegistryTestOne));
        }

        [Test]
        public void CannotRegisterPatchTwice()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var firstPatch = EventPatchRegistry.Register(typeof(PatchRegistryTestOne));
                var secondPatch = EventPatchRegistry.Register(typeof(PatchRegistryTestOne));
            });
        }

        [Test]
        public void CanRegisterMultiplePatches()
        {
            var firstPatch = EventPatchRegistry.Register(typeof(PatchRegistryTestOne));
            var secondPatch = EventPatchRegistry.Register(typeof(PatchRegistryTestTwo));
        }

        [Test]
        public void CannotRegisterPatchWithInvalidSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                EventPatchRegistry.Register(typeof(PatchRegistryTestWithInvalidPatchSite));
            });
        }

        [Test]
        public void CannotRegisterPatchLackingSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                EventPatchRegistry.Register(typeof(PatchRegistryTestWithoutPatchSite));
            });
        }
    }

    [EventPatchSite(typeof(SelectionInventory), "SelectIndex")]
    internal class PatchRegistryTestOne : EventEmitter<TestEventOne>
    {
        public static void Prefix() { }
    }

    [EventPatchSite(typeof(EconomyManager), "OnCreate")]
    internal class PatchRegistryTestTwo : EventEmitter<TestEventTwo>
    {
        public static void Prefix() { }
    }

    internal class PatchRegistryTestWithoutPatchSite : EventEmitter<TestEventThree> { }

    [EventPatchSite(typeof(EconomyManager), "NotAMethod")]
    internal class PatchRegistryTestWithInvalidPatchSite : EventEmitter<TestEventFour> { }

    internal class TestEventOne : EventArgs { }
    internal class TestEventTwo : EventArgs { }
    internal class TestEventThree : EventArgs { }
    internal class TestEventFour : EventArgs { }
}

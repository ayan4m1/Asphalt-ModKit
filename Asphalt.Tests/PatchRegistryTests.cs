using Asphalt.Events;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Items;
using NUnit.Framework;
using System;

namespace Asphalt.Tests
{
    [TestFixture]
    public class PatchRegistryTests
    {
        [TearDown]
        public void ResetRegistry()
        {
            PatchRegistry.Reset();
        }

        [Test]
        public void CanRegisterPatch()
        {
            PatchRegistry.RegisterPatch(typeof(PatchRegistryTestOne));
        }

        [Test]
        public void CannotRegisterPatchTwice()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var firstPatch = PatchRegistry.RegisterPatch(typeof(PatchRegistryTestOne));
                var secondPatch = PatchRegistry.RegisterPatch(typeof(PatchRegistryTestOne));
            });
        }

        [Test]
        public void CanRegisterMultiplePatches()
        {
            var firstPatch = PatchRegistry.RegisterPatch(typeof(PatchRegistryTestOne));
            var secondPatch = PatchRegistry.RegisterPatch(typeof(PatchRegistryTestTwo));
        }

        [Test]
        public void CannotRegisterPatchWithInvalidSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                PatchRegistry.RegisterPatch(typeof(PatchRegistryTestWithInvalidPatchSite));
            });
        }

        [Test]
        public void CannotRegisterPatchLackingSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                PatchRegistry.RegisterPatch(typeof(PatchRegistryTestWithoutPatchSite));
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

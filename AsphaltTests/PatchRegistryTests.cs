using Asphalt.Events;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Items;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AsphaltTests
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
            Assert.Throws(typeof(ArgumentException), () =>
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
            Assert.Throws(typeof(ArgumentException), () =>
            {
                PatchRegistry.RegisterPatch(typeof(PatchRegistryTestWithInvalidPatchSite));
            });
        }

        [Test]
        public void CannotRegisterPatchLackingSite()
        {
            Assert.Throws(typeof(ArgumentNullException), () =>
            {
                PatchRegistry.RegisterPatch(typeof(PatchRegistryTestWithoutPatchSite));
            });
        }
    }

    [EventPatchSite(typeof(SelectionInventory), "SelectIndex")]
    internal class PatchRegistryTestOne
    {
        public static void Prefix(SelectionInventory __instance)
        {
            Console.WriteLine(__instance.ToString());
        }
    }

    [EventPatchSite(typeof(EconomyManager), "OnCreate")]
    internal class PatchRegistryTestTwo
    {
        public static void Prefix(EconomyManager __instance)
        {
            Console.WriteLine(__instance.ToString());
        }
    }

    internal class PatchRegistryTestWithoutPatchSite { }

    [EventPatchSite(typeof(EconomyManager), "NotAMethod")]
    internal class PatchRegistryTestWithInvalidPatchSite { }
}

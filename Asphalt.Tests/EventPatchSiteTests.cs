using Asphalt.Events;
using NUnit.Framework;
using System;

namespace Asphalt.Tests
{
    [TestFixture]
    public class EventPatchSiteTests
    {
        private static readonly Type thisType = typeof(EventPatchSiteTests);

        [Test]
        public void FindSimpleSite()
        {
            new EventPatchSite(thisType, "Test");
        }

        [Test]
        public void FailSimpleSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new EventPatchSite(thisType, "NotAMethod");
            });
        }

        [Test]
        public void FindSpecificSite()
        {
            new EventPatchSite(thisType, "StaticTest", CommonBindingFlags.Static);
        }

        [Test]
        public void FailSpecificSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new EventPatchSite(thisType, "StaticTest", CommonBindingFlags.Instance);
            });
        }

        [Test]
        public void FindParameterCountSite()
        {
            new EventPatchSite(thisType, "TestParams", CommonBindingFlags.Instance, 3);
        }

        [Test]
        public void FailParameterCountSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new EventPatchSite(thisType, "TestParams", CommonBindingFlags.Instance, 1);
            });
        }

        [Test]
        public void FindAtomicActionSite()
        {
            new AtomicActionEventPatchSite(thisType);
        }

        [Test]
        public void FailAtomicActionSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new AtomicActionEventPatchSite(typeof(object));
            });
        }

        // stub methods for testing EventPatchSite
        public static void StaticTest() { }
        public void Test() { }
        public void TestParams(int first, int second, int third) { }
        public void CreateAtomicAction() { }
    }
}

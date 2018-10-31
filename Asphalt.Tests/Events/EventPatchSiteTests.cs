using Asphalt.Events;
using NUnit.Framework;
using System;

namespace Asphalt.Tests.Events
{
    [TestFixture]
    public class EventPatchSiteTests
    {
        private static readonly Type thisType = typeof(EventPatchSiteTests);

        [Test]
        public void FindSimpleSite()
        {
            new EventPatchSiteAttribute(thisType, "Test");
        }

        [Test]
        public void FailSimpleSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new EventPatchSiteAttribute(thisType, "NotAMethod");
            });
        }

        [Test]
        public void FindSpecificSite()
        {
            new EventPatchSiteAttribute(thisType, "StaticTest", CommonBindingFlags.Static);
        }

        [Test]
        public void FailSpecificSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new EventPatchSiteAttribute(thisType, "StaticTest", CommonBindingFlags.Instance);
            });
        }

        [Test]
        public void FindParameterCountSite()
        {
            new EventPatchSiteAttribute(thisType, "TestParams", CommonBindingFlags.Instance, 3);
        }

        [Test]
        public void FailParameterCountSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new EventPatchSiteAttribute(thisType, "TestParams", CommonBindingFlags.Instance, 1);
            });
        }

        [Test]
        public void FindAtomicActionSite()
        {
            new AtomicActionEventPatchSiteAttribute(thisType);
        }

        [Test]
        public void FailAtomicActionSite()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new AtomicActionEventPatchSiteAttribute(typeof(object));
            });
        }

        // stub methods for testing EventPatchSite
        public static void StaticTest() { }
        public void Test() { }
        public void TestParams(int first, int second, int third) { }
        public void CreateAtomicAction() { }
    }
}

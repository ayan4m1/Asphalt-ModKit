using Asphalt.Events;
using NUnit.Framework;
using System;

namespace Asphalt.Tests.Events
{
    [TestFixture]
    public class EventPatchTests
    {
        public void TestMethod()
        {
            Assert.Fail();
        }

        [Test]
        public void CanPatchAndUnpatchMethods()
        {
            var patch = typeof(EventPatchTest).GetEventPatch();
            Assert.Throws<AssertionException>(TestMethod);
            patch.Patch();
            Assert.Throws<SuccessException>(TestMethod);
            patch.Unpatch();
            Assert.Throws<AssertionException>(TestMethod);
        }

        [EventPatchSite(typeof(EventPatchTests), "TestMethod")]
        internal class EventPatchTest : EventEmitter<EventArgs>
        {
            public static void Prefix()
            {
                Assert.Pass();
            }
        }
    }
}

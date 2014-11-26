using System.Net;
using FeedBurnerPinger.Client.Tests.Fakes;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace FeedBurnerPinger.Client.Tests
{
    [TestFixture]
    public class PingingClientTests
    {
        private const string SomeFeedUrl = "http://some-feed-url";

        [TestCase("http://some-url-that-succeeds", PingStatus.Succeeded, "Successfully pinged")]
        [TestCase("http://some-url-that-reports-throttled", PingStatus.Throttled, "Ping is throttled")]
        [TestCase("http://some-url-that-reports-failed", PingStatus.Failed, "Your Ping resulted in an Error")]
        public void PingTest(string endpointUrl, PingStatus expectedStatus, string expectedMessage)
        {
            WebRequest.RegisterPrefix(endpointUrl, new FakeWebRequestCreator());
            IPingingClient client = new PingingClient(endpointUrl);

            PingResponse response = client.Ping(new PingRequest { FeedUrl = SomeFeedUrl });

            Assert.AreEqual(response.Status, expectedStatus);
            Assert.AreEqual(response.Message, expectedMessage);
        }
    }
}

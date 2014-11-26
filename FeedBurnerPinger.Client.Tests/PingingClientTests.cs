using FeedBurnerPinger.Client.Tests.Fakes;
using NUnit.Framework;

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
            IPingingClient client = new PingingClient(endpointUrl, new FakeWebClient());

            PingResponse response = client.Ping(new PingRequest { FeedUrl = SomeFeedUrl });

            Assert.AreEqual(expectedStatus, response.Status);
            Assert.AreEqual(expectedMessage, response.Message);
        }
    }
}

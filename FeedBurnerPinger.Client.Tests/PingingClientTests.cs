using System.Net;
using FeedBurnerPinger.Client.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeedBurnerPinger.Client.Tests
{
    [TestClass]
    public class PingingClientTests
    {
        private const string SomeFeedUrl = "http://some-feed-url";

        [TestMethod]
        public void When_the_endpoint_returns_SUCCEEDED_it_should_return_a_response_with_Succeeded()
        {
            const string EndpointUrl = "http://some-url-that-succeeds";
            
            WebRequest.RegisterPrefix(EndpointUrl, new FakeWebRequestCreator());
            IPingingClient client = new PingingClient(EndpointUrl);

            PingResponse response = client.Ping(new PingRequest
            {
                FeedUrl = SomeFeedUrl
            });

            Assert.AreEqual(response.Status, PingStatus.Succeeded);
            Assert.AreEqual(response.Message, "Successfully pinged");
        }

        [TestMethod]
        public void When_the_endpoint_returns_THROTTLED_it_should_return_a_response_with_Throttled()
        {
            const string EndpointUrl = "http://some-url-that-reports-throttled";

            WebRequest.RegisterPrefix(EndpointUrl, new FakeWebRequestCreator());
            IPingingClient client = new PingingClient(EndpointUrl);

            PingResponse response = client.Ping(new PingRequest
            {
                FeedUrl = SomeFeedUrl
            });

            Assert.AreEqual(response.Status, PingStatus.Throttled);
            Assert.AreEqual(response.Message, "Ping is throttled");
        }

        [TestMethod]
        public void When_the_endpoint_returns_FAILED_it_should_return_a_response_with_Failed()
        {
            const string EndpointUrl = "http://some-url-that-reports-failed";

            WebRequest.RegisterPrefix(EndpointUrl, new FakeWebRequestCreator());
            IPingingClient client= new PingingClient(EndpointUrl);

            PingResponse response = client.Ping(new PingRequest
            {
                FeedUrl = SomeFeedUrl
            });

            Assert.AreEqual(response.Status, PingStatus.Failed);
            Assert.AreEqual(response.Message, "Your Ping resulted in an Error");
        }
    }
}

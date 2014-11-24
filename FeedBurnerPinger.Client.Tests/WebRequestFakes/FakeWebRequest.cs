using System.Net;

namespace FeedBurnerPinger.Client.Tests.WebRequestFakes
{
    public class FakeWebRequest : WebRequest
    {
        private readonly string responseJson;

        public FakeWebRequest(string responseJson)
        {
            this.responseJson = responseJson;
        }

        public override WebResponse GetResponse()
        {
            return new FakeWebResponse(this.responseJson);
        }
    }
}
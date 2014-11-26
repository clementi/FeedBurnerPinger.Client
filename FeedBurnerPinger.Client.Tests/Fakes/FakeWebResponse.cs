using System.IO;
using System.Net;
using System.Text;

namespace FeedBurnerPinger.Client.Tests.Fakes
{
    public class FakeWebResponse : WebResponse
    {
        private readonly string responseJson;

        public FakeWebResponse(string responseJson)
        {
            this.responseJson = responseJson;
        }

        public override Stream GetResponseStream()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(this.responseJson));
        }
    }
}
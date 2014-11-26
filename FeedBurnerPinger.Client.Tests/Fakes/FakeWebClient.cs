using System.Collections.Specialized;
using System.Text;

namespace FeedBurnerPinger.Client.Tests.Fakes
{
    public class FakeWebClient : IWebClient
    {
        public byte[] UploadValues(string url, NameValueCollection values)
        {
            if (url.Contains("succeed"))
                return Encoding.UTF8.GetBytes(@"{ 'status': 'SUCCEEDED', 'message': 'Successfully pinged' }");

            if (url.Contains("throttle"))
                return Encoding.UTF8.GetBytes(@"{ 'status': 'THROTTLED', 'message': 'Ping is throttled' }");

            if (url.Contains("fail"))
                return Encoding.UTF8.GetBytes(@"{ 'status': 'FAILED', 'message': 'Your Ping resulted in an Error' }");

            return null;
        }
    }
}
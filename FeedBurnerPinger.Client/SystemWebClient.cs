using System.Collections.Specialized;
using System.Net;

namespace FeedBurnerPinger.Client
{
    internal class SystemWebClient : IWebClient
    {
        public byte[] UploadValues(string url, NameValueCollection values)
        {
            using (var webClient = new WebClient())
                return webClient.UploadValues(url, values);
        }
    }
}
using System.Collections.Specialized;

namespace FeedBurnerPinger.Client
{
    internal interface IWebClient
    {
        byte[] UploadValues(string url, NameValueCollection values);
    }
}
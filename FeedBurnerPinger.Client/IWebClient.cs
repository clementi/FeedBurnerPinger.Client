using System.Collections.Specialized;

namespace FeedBurnerPinger.Client
{
    public interface IWebClient
    {
        byte[] UploadValues(string url, NameValueCollection values);
    }
}
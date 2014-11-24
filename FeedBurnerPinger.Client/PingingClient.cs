using System.Configuration;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace FeedBurnerPinger.Client
{
    public class PingingClient : IPingingClient
    {
        private readonly string endpointUrl;

        public PingingClient(string endpointUrl)
        {
            this.endpointUrl = endpointUrl;
        }

        public PingingClient()
        {
            this.endpointUrl = ConfigurationManager.AppSettings["EndpointUrl"];
        }

        public PingResponse Ping(PingRequest request)
        {
            var webRequest = WebRequest.Create(this.endpointUrl);

            using (WebResponse response = webRequest.GetResponse())
            {
                Stream responseStream = response.GetResponseStream();
                if (responseStream != null)
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        var serializer = new JsonSerializer();
                        return serializer.Deserialize<PingResponse>(new JsonTextReader(reader));
                    }
                }
                return null;
            }
        }
    }
}

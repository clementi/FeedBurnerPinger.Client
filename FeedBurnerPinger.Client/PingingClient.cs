using System.Configuration;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace FeedBurnerPinger.Client
{
    public class PingingClient : IPingingClient
    {
        private readonly string endpointUrl;
        private readonly JsonSerializer serializer;

        public PingingClient(string endpointUrl)
        {
            this.endpointUrl = endpointUrl;
            this.serializer = new JsonSerializer();
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
                    using (var reader = new StreamReader(responseStream))
                        return this.serializer.Deserialize<PingResponse>(new JsonTextReader(reader));
                
                return null;
            }
        }
    }
}

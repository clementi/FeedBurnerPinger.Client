using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("FeedBurnerPinger.Client.Tests")]

namespace FeedBurnerPinger.Client
{
    public class PingingClient : IPingingClient
    {
        private readonly string endpointUrl;
        private readonly IWebClient webClient;
        private readonly JsonSerializer serializer;

        internal PingingClient(string endpointUrl, IWebClient webClient)
        {
            this.endpointUrl = endpointUrl;
            this.webClient = webClient;
            this.serializer = new JsonSerializer();
        }

        public PingingClient() : this(ConfigurationManager.AppSettings["EndpointUrl"], new SystemWebClient())
        {
        }

        public PingResponse Ping(PingRequest request)
        {
            byte[] response = this.webClient.UploadValues(this.endpointUrl, new NameValueCollection { { "url", request.FeedUrl } });
         
            using (var reader = new StreamReader(new MemoryStream(response)))
                return this.serializer.Deserialize<PingResponse>(new JsonTextReader(reader));
        }
    }
}

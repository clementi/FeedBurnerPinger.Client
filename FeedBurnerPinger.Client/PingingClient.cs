using System;
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
        private const string EndpointUrlSettingKey = "EndpointUrl";
        private const string UrlFormParameterName = "url";
        private readonly string endpointUrl;
        private readonly IWebClient webClient;
        private readonly JsonSerializer serializer;

        internal PingingClient(string endpointUrl, IWebClient webClient)
        {
            this.endpointUrl = endpointUrl;
            this.webClient = webClient;
            this.serializer = new JsonSerializer();
        }

        public PingingClient() : this(GetEndpointUrl(), new SystemWebClient())
        {
        }

        public PingResponse Ping(PingRequest request)
        {
            byte[] response = this.webClient.UploadValues(this.endpointUrl, new NameValueCollection { { UrlFormParameterName, request.FeedUrl } });
         
            using (var reader = new StreamReader(new MemoryStream(response)))
                return this.serializer.Deserialize<PingResponse>(new JsonTextReader(reader));
        }

        private static string GetConfigurationAppSetting(Configuration config, string key)
        {
            if (config != null)
            {
                KeyValueConfigurationElement element = config.AppSettings.Settings[key];

                if (element != null)
                {
                    string value = element.Value;
                    if (!string.IsNullOrWhiteSpace(value))
                        return value;
                }
            }
            return string.Empty;
        }

        private static string GetEndpointUrl()
        {
            string exeConfigPath = typeof(PingingClient).Assembly.Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exeConfigPath);

            return GetConfigurationAppSetting(config, EndpointUrlSettingKey);
        }
    }
}

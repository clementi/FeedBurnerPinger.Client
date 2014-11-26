using System;
using System.Net;

namespace FeedBurnerPinger.Client.Tests.Fakes
{
    public class FakeWebRequestCreator : IWebRequestCreate
    {
        public WebRequest Create(Uri uri)
        {
            string uriString = uri.ToString();
            string responseJson = null;

            if (uriString.Contains("succeed"))
                responseJson = "{ 'status': 'SUCCEEDED', 'message': 'Successfully pinged' }";
            else if (uriString.Contains("throttle"))
                responseJson = "{ 'status': 'THROTTLED', 'message': 'Ping is throttled' }";
            else if (uriString.Contains("fail"))
                responseJson = "{ 'status': 'FAILED', 'message': 'Your Ping resulted in an Error' }";

            return new FakeWebRequest(responseJson);
        }
    }
}
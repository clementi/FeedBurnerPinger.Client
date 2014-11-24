using System.Runtime.Serialization;

namespace FeedBurnerPinger.Client
{
    [DataContract]
    public class PingResponse
    {
        [DataMember(Name = "status")]
        public PingStatus Status { get; private set; }

        [DataMember(Name = "message")]
        public string Message { get; private set; }
    }
}

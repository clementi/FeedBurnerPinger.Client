namespace FeedBurnerPinger.Client
{
    public interface IPingingClient
    {
        PingResponse Ping(PingRequest request);
    }
}
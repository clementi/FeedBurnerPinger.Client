# FeedBurnerPinger.Client

## Description

FeedBurnerPinger.Client is a .NET client library for access to the FeedBurner-Pinger service at http://feedburner-pinger.herokuapp.com.

## Usage

Use the client like this:

```csharp
IPingingClient client = new PingingClient();

var request = new PingRequest { FeedUrl = "http://url-to-your-feedburner-feed" };
PingResponse response = client.Ping(request);
```

The `PingResponse` contains two fields: `PingStatus` and `Message`. `PingStatus` is an enum with values `Succeeded`, `Throttled`, or `Failed`. `Message` is a string which gives a human-readable version of the status. More information on this can be found at http://feedburner-pinger.herokuapp.com.

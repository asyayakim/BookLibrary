// AllEventsResponse.cs
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BookLibrary.Database;

public class AllEventsResponse
{
    [JsonProperty("data")]
    public List<Event> Events { get; set; } = new List<Event>();
}

public class Event
{
    [JsonProperty("eventname")]
    public string EventName { get; set; }

    [JsonProperty("event_start_date")]
    public string StartDate { get; set; }

    [JsonProperty("event_end_date")]
    public string EndDate { get; set; }

    [JsonProperty("venue")]
    public string Venue { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("event_url")]
    public string Url { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("thumb_url")]
    public string ThumbnailUrl { get; set; }
}
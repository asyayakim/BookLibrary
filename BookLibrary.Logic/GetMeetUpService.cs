using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace BookLibrary.Logic;

public class GetMeetUpService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GetMeetUpService(IConfiguration config, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiKey = config["Ticketmaster:ApiKey"] 
                  ?? throw new ArgumentNullException("Missing Ticketmaster:ApiKey configuration");
        Console.WriteLine($"Using Ticketmaster API Key: {_apiKey}");
    }

    public async Task<List<Event>> GetEventsAsync(string location = "Oslo")
    {
        string url = $"https://app.ticketmaster.com/discovery/v2/events.json?apikey={_apiKey}&city={location}&size=20";

        Console.WriteLine($"Fetching events from: {url}");

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine("✅ Ticketmaster API Response:");
            Console.WriteLine(content.Substring(0, 500)); // Print first 500 characters

            var ticketmasterResponse = JsonSerializer.Deserialize<TicketmasterResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return ticketmasterResponse?.Embedded?.Events ?? new List<Event>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching events: {ex.Message}");
            throw;
        }
        
    }
    public class TicketmasterResponse
    {
        [JsonPropertyName("_embedded")]
        public EmbeddedData Embedded { get; set; }
    }
    public class EmbeddedData
    {
        [JsonPropertyName("events")]
        public List<Event> Events { get; set; }
    }
    public class Event
    {
        [JsonPropertyName("name")]
        public string EventName { get; set; }

        [JsonPropertyName("dates")]
        public DateData Dates { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("images")]
        public List<ImageData> Images { get; set; }

        [JsonPropertyName("_embedded")]
        public VenueData Venue { get; set; }
    }

    public class DateData
    {
        [JsonPropertyName("start")]
        public StartDate Start { get; set; }
    }

    public class StartDate
    {
        [JsonPropertyName("localDate")]
        public string Date { get; set; }
    }

    public class VenueData
    {
        [JsonPropertyName("venues")]
        public List<Venue> Venues { get; set; }
    }

    public class Venue
    {
        [JsonPropertyName("name")]
        public string VenueName { get; set; }

        [JsonPropertyName("city")]
        public City City { get; set; }
    }

    public class City
    {
        [JsonPropertyName("name")]
        public string CityName { get; set; }
    }

    public class ImageData
    {
        [JsonPropertyName("url")]
        public string ImageUrl { get; set; }
    }

}
using System.Text.Json;
using BookLibrary.Database;
using Microsoft.Extensions.Configuration;

namespace BookLibrary.Logic;

public class GoogleBooksService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GoogleBooksService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["GoogleBooks:ApiKey"] ?? throw new ArgumentNullException("GoogleBooks:ApiKey is not set");
        }

        public async Task<List<Book>> GetFictionBooksAsync(int maxResults = 2)
        {
            var books = new List<Book>();
            int startIndex = 0;

            while (books.Count < maxResults)
            {
                string query = $"https://www.googleapis.com/books/v1/volumes?q=subject:fiction+intitle:book&printType=books&startIndex={startIndex}&maxResults=10&orderBy=relevance&key={_apiKey}";
                var response = await _httpClient.GetAsync(query);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var googleBooksResponse = JsonSerializer.Deserialize<GoogleBooksResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (googleBooksResponse?.Items != null)
                {
                    Console.WriteLine($"Google Books API Items Count: {googleBooksResponse.Items.Count}");
                }
                else
                {
                    Console.WriteLine("Google Books API Response has no items.");
                }
                
                foreach (var item in googleBooksResponse.Items)
                {
                    if (item.VolumeInfo == null || string.IsNullOrEmpty(item.VolumeInfo.Title))
                    {
                        continue;
                    }

                    var authors = string.Join(", ", item.VolumeInfo.Authors ?? new List<string>());
                    var genre = string.Join(", ", item.VolumeInfo.Categories ?? new List<string>());
                    var isbn = item.VolumeInfo.IndustryIdentifiers?.FirstOrDefault()?.Identifier ?? "N/A";
                    var coverImageUrl = item.VolumeInfo.ImageLinks?.Thumbnail ?? "https://via.placeholder.com/150";

                    books.Add(new Book
                    {
                        Title = item.VolumeInfo.Title,
                        Author = authors,
                        Genre = string.IsNullOrEmpty(genre) ? "Fiction" : genre,
                        Year = ParseYear(item.VolumeInfo.PublishedDate),
                        Isbn = isbn,
                        CoverImageUrl = coverImageUrl
                    });
                }
                startIndex += 10; 
                if (googleBooksResponse.Items.Count < 10) break;
            }
            return books.Take(maxResults).ToList();
        }

        private int ParseYear(string? publishedDate)
        {
            if (string.IsNullOrEmpty(publishedDate)) return 0;
            if (int.TryParse(publishedDate.Split('-')[0], out int year)) return year;
            return 0;
        }
    }

public class GoogleBooksResponse
{
    public string Kind { get; set; }
    public int TotalItems { get; set; }
    public List<GoogleBookItem> Items { get; set; } = new(); 
}

public class GoogleBookItem
{
    public string Kind { get; set; } 
    public string Id { get; set; } 
    public string Etag { get; set; } 
    public string SelfLink { get; set; } 
    public VolumeInfo VolumeInfo { get; set; } = new();
}

public class VolumeInfo
{
    public string Title { get; set; } = string.Empty; 
    public List<string>? Authors { get; set; } 
    public string? PublishedDate { get; set; } 
    public List<string>? Categories { get; set; } 
    public List<IndustryIdentifier>? IndustryIdentifiers { get; set; }
    public ImageLinks? ImageLinks { get; set; } 
}

public class IndustryIdentifier
{
    public string Type { get; set; } = string.Empty; 
    public string Identifier { get; set; } = string.Empty; 
}

public class ImageLinks
{
    public string? Thumbnail { get; set; } 
}

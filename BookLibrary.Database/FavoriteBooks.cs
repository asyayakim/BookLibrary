namespace BookLibrary.Database;

public class FavoriteBooks
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string CoverImageUrl { get; set; }
    
}
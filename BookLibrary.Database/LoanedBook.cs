namespace BookLibrary.Database;

public class LoanedBook
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public DateTime LoanDate { get; set; }
}
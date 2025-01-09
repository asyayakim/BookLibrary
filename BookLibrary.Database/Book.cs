using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Database
{
    public class Book
    {
        public int Id { get; set; }
        
        public string Title { get; set; }  = string.Empty;
       [Required]
        [MaxLength(50)]
        public string Author { get; set; } = string.Empty;
       
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;
       
        public int Year { get; set; } 
        
        public string Isbn { get; set; }= string.Empty; 
       
        
        public string CoverImageUrl { get; set; } = string.Empty;
    }   
}


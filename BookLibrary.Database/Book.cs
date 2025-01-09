using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Database
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }  = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Author { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Genre { get; set; } = string.Empty;
        [Required]
        public int Year { get; set; } 
        [Required]
        [MaxLength(13)]
        public string Isbn { get; set; }= string.Empty; 
        [Required]
        
        public string CoverImageUrl { get; set; } = string.Empty;
    }   
}


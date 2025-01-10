using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Database;

public class UserData
{
    [Required]
    [MaxLength(20)]
    public string UserName { get; set; }
    [Required]
    [MaxLength(50)]  
    public string Password { get; set; } 
    
}
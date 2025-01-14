using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Database;

public class UserData
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(20)]
    public required string UserName { get; set; }
    [Required]
    [MaxLength(50)]  
    public required string Password { get; set; }

    [MaxLength(6)] public string Role { get; set; } = "User";

}
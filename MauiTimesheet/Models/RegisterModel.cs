using System.ComponentModel.DataAnnotations;

namespace MauiTimesheet.Models;

public class RegisterModel
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Payments.Api.DTOs;

public class UserDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Document { get; set; } = string.Empty;
    
    public bool IsCompany { get; set; } = false;
    
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}
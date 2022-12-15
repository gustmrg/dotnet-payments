using System.ComponentModel.DataAnnotations;

namespace Payments.Api.DTOs;

public class WalletDto
{
    [Required]
    public int UserId { get; set; }
    public double Balance { get; set; }
}
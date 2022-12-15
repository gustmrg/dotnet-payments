using System.ComponentModel.DataAnnotations;

namespace Payments.Api.DTOs;

public class TransactionDto
{
    [Required]
    public double Amount { get; set; }
    
    [Required]
    public int PayerId { get; set; }
    
    [Required]
    public int PayeeId { get; set; }
}
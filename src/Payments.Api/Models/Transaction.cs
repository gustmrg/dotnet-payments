namespace Payments.Api.Models;

public class Transaction
{
    public int Id { get; set; }
    public double Amount { get; set; }
    public int PayerId { get; set; }
    public int PayeeId { get; set; }
    public DateTime Time { get; set; }
}
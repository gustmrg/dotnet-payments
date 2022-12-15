using System.Text.Json.Serialization;

namespace Payments.Api.Models;

public class Wallet
{
    public int Id { get; set; }
    public double Balance { get; set; }
    public int UserId { get; set; }
    public bool IsActive { get; set; } = false;
    [JsonIgnore]
    public virtual User User { get; set; }
    // public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
}
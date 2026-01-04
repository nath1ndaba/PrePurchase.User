namespace PrePurchase.Mobile.Shared.Models.Collector;

public class AddAuthorizedCollectorCommand
{
    public Guid UserId { get; set; }
    public string? CollectorName { get; set; }
    public string? CollectorPhone { get; set; }
    public string? Relationship { get; set; }
}

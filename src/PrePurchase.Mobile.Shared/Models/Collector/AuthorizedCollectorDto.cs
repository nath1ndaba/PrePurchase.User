namespace PrePurchase.Mobile.Shared.Models.Collector;

public class AuthorizedCollectorDto
{
    public Guid Id { get; set; }
    public string? CollectorName { get; set; }
    public string? CollectorPhone { get; set; }
    public string? Relationship { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

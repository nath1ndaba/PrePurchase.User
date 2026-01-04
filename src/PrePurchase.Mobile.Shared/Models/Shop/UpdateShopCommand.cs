namespace PrePurchase.Mobile.Shared.Models.Shop;

public class UpdateShopCommand
{
    public Guid ShopId { get; set; }
    public Guid OwnerId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Suburb { get; set; }
    public string? Province { get; set; }
    public double ServiceRadiusKm { get; set; }
    public string? OpeningTime { get; set; }
    public string? ClosingTime { get; set; }
    public List<string>? ClosedDays { get; set; }
}

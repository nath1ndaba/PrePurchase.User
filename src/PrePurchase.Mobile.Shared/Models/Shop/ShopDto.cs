namespace PrePurchase.Mobile.Shared.Models.Shop;

public class ShopDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Distance { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
    public decimal AverageRating { get; private set; } = 3.0m;
    public int TotalReviews { get; private set; } = 0;
}

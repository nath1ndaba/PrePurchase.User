namespace PrePurchase.Mobile.Shared.Models.Cart;

public class CartValidationIssue
{
    public Guid ShopProductId { get; set; }
    public string? ProductName { get; set; }
    public string? IssueType { get; set; }
    public string? Message { get; set; }
    public int? RequestedQuantity { get; set; }
    public int? AvailableStock { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal? NewPrice { get; set; }
}
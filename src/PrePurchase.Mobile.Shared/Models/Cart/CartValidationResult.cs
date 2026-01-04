namespace PrePurchase.Mobile.Shared.Models.Cart;

// Matches backend CartValidationResult
public class CartValidationResult
{
    public bool IsValid { get; set; }
    public List<CartValidationIssue>? Issues { get; set; }
    public decimal TotalAmount { get; set; }
    public int TotalItems { get; set; }
}

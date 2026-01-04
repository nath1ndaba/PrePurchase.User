namespace PrePurchase.Mobile.Shared.Models.Category;

public class UpdateCategoryCommand
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

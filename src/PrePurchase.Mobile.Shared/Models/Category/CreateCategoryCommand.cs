namespace PrePurchase.Mobile.Shared.Models.Category;

public class CreateCategoryCommand
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}

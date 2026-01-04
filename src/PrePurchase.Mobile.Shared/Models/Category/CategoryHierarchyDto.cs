namespace PrePurchase.Mobile.Shared.Models.Category;

public class CategoryHierarchyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public List<CategoryHierarchyDto> Children { get; set; } = new();
}

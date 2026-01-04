using PrePurchase.Mobile.Shared.Models.Category;
using PrePurchase.Mobile.Shared.Models.Common;

namespace PrePurchase.Mobile.Shared.Services.Category;

public interface ICategoryService
{
    Task<Result<List<CategoryDto>>> GetCategoriesAsync(bool includeInactive = false);
    Task<Result<List<CategoryDto>>> GetRootCategoriesAsync(bool includeInactive = false);
    Task<Result<List<CategoryHierarchyDto>>> GetCategoryHierarchyAsync();
    Task<Result<CategoryDto>> GetCategoryByIdAsync(Guid id);
    Task<Result<Guid>> CreateCategoryAsync(string name, string? description = null, Guid? parentCategoryId = null);
    Task<Result<CategoryDto>> UpdateCategoryAsync(Guid id, string name, string? description = null);
    Task<Result> DeleteCategoryAsync(Guid id);
    Task<Result<CategoryDto>> ActivateCategoryAsync(Guid id);
    Task<Result<CategoryDto>> DeactivateCategoryAsync(Guid id);
}

using PrePurchase.Mobile.Shared.Models.Category;
using Refit;

namespace PrePurchase.Mobile.Shared.Services.Api;

public interface ICategoryApi
{
    [Get("/api/categories")]
    Task<Models.Common.ApiResponse<List<CategoryDto>>> GetCategoriesAsync([Query] bool includeInactive = false);

    [Get("/api/categories/roots")]
    Task<Models.Common.ApiResponse<List<CategoryDto>>> GetRootCategoriesAsync([Query] bool includeInactive = false);

    [Get("/api/categories/hierarchy")]
    Task<Models.Common.ApiResponse<List<CategoryHierarchyDto>>> GetCategoryHierarchyAsync();

    [Get("/api/categories/{id}")]
    Task<Models.Common.ApiResponse<CategoryDto>> GetCategoryByIdAsync(Guid id);

    [Post("/api/categories")]
    Task<Models.Common.ApiResponse<Guid>> CreateCategoryAsync([Body] CreateCategoryCommand request);

    [Put("/api/categories/{id}")]
    Task<Models.Common.ApiResponse<CategoryDto>> UpdateCategoryAsync(Guid id, [Body] UpdateCategoryCommand request);

    [Delete("/api/categories/{id}")]
    Task<Models.Common.ApiResponse<object?>> DeleteCategoryAsync(Guid id);


    [Post("/api/categories/{id}/activate")]
    Task<Models.Common.ApiResponse<CategoryDto>> ActivateCategoryAsync(Guid id);

    [Post("/api/categories/{id}/deactivate")]
    Task<Models.Common.ApiResponse<CategoryDto>> DeactivateCategoryAsync(Guid id);
}

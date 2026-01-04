using PrePurchase.Mobile.Shared.Models.Category;
using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.Category;

public class CategoryService(ICategoryApi categoryApi) : ICategoryService
{
    private readonly ICategoryApi _categoryApi = categoryApi;

    public async Task<Result<List<CategoryDto>>> GetCategoriesAsync(bool includeInactive = false)
    {
        try
        {
            var response = await _categoryApi.GetCategoriesAsync(includeInactive);

            if (response.Success && response.Data != null)
            {
                return Result<List<CategoryDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<List<CategoryDto>>.Failure(response.Errors)
                : Result<List<CategoryDto>>.Failure(response.Message ?? "Failed to fetch categories");
        }
        catch (Exception ex)
        {
            return Result<List<CategoryDto>>.Failure($"Error fetching categories: {ex.Message}");
        }
    }

    public async Task<Result<List<CategoryDto>>> GetRootCategoriesAsync(bool includeInactive = false)
    {
        try
        {
            var response = await _categoryApi.GetRootCategoriesAsync(includeInactive);

            if (response.Success && response.Data != null)
            {
                return Result<List<CategoryDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<List<CategoryDto>>.Failure(response.Errors)
                : Result<List<CategoryDto>>.Failure(response.Message ?? "Failed to fetch root categories");
        }
        catch (Exception ex)
        {
            return Result<List<CategoryDto>>.Failure($"Error fetching root categories: {ex.Message}");
        }
    }

    public async Task<Result<List<CategoryHierarchyDto>>> GetCategoryHierarchyAsync()
    {
        try
        {
            var response = await _categoryApi.GetCategoryHierarchyAsync();

            if (response.Success && response.Data != null)
            {
                return Result<List<CategoryHierarchyDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<List<CategoryHierarchyDto>>.Failure(response.Errors)
                : Result<List<CategoryHierarchyDto>>.Failure(response.Message ?? "Failed to fetch category hierarchy");
        }
        catch (Exception ex)
        {
            return Result<List<CategoryHierarchyDto>>.Failure($"Error fetching category hierarchy: {ex.Message}");
        }
    }

    public async Task<Result<CategoryDto>> GetCategoryByIdAsync(Guid id)
    {
        try
        {
            var response = await _categoryApi.GetCategoryByIdAsync(id);

            if (response.Success && response.Data != null)
            {
                return Result<CategoryDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<CategoryDto>.Failure(response.Errors)
                : Result<CategoryDto>.Failure(response.Message ?? "Failed to fetch category");
        }
        catch (Exception ex)
        {
            return Result<CategoryDto>.Failure($"Error fetching category: {ex.Message}");
        }
    }

    public async Task<Result<Guid>> CreateCategoryAsync(string name, string? description = null, Guid? parentCategoryId = null)
    {
        try
        {
            var command = new CreateCategoryCommand
            {
                Name = name,
                Description = description,
                ParentCategoryId = parentCategoryId
            };

            var response = await _categoryApi.CreateCategoryAsync(command);

            if (response.Success && response.Data != default)
            {
                return Result<Guid>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<Guid>.Failure(response.Errors)
                : Result<Guid>.Failure(response.Message ?? "Failed to create category");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Error creating category: {ex.Message}");
        }
    }

    public async Task<Result<CategoryDto>> UpdateCategoryAsync(Guid id, string name, string? description = null)
    {
        try
        {
            var command = new UpdateCategoryCommand
            {
                Name = name,
                Description = description
            };

            var response = await _categoryApi.UpdateCategoryAsync(id, command);

            if (response.Success && response.Data != null)
            {
                return Result<CategoryDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<CategoryDto>.Failure(response.Errors)
                : Result<CategoryDto>.Failure(response.Message ?? "Failed to update category");
        }
        catch (Exception ex)
        {
            return Result<CategoryDto>.Failure($"Error updating category: {ex.Message}");
        }
    }

    public async Task<Result> DeleteCategoryAsync(Guid id)
    {
        try
        {
            var response = await _categoryApi.DeleteCategoryAsync(id);

            if (response.Success)
            {
                return Result.Success();
            }

            return response.Errors?.Count == 0
                ? Result.Failure(response.Errors)
                : Result.Failure(response.Message ?? "Failed to delete category");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error deleting category: {ex.Message}");
        }
    }

    public async Task<Result<CategoryDto>> ActivateCategoryAsync(Guid id)
    {
        try
        {
            var response = await _categoryApi.ActivateCategoryAsync(id);

            if (response.Success && response.Data != null)
            {
                return Result<CategoryDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<CategoryDto>.Failure(response.Errors)
                : Result<CategoryDto>.Failure(response.Message ?? "Failed to activate category");
        }
        catch (Exception ex)
        {
            return Result<CategoryDto>.Failure($"Error activating category: {ex.Message}");
        }
    }

    public async Task<Result<CategoryDto>> DeactivateCategoryAsync(Guid id)
    {
        try
        {
            var response = await _categoryApi.DeactivateCategoryAsync(id);

            if (response.Success && response.Data != null)
            {
                return Result<CategoryDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<CategoryDto>.Failure(response.Errors)
                : Result<CategoryDto>.Failure(response.Message ?? "Failed to deactivate category");
        }
        catch (Exception ex)
        {
            return Result<CategoryDto>.Failure($"Error deactivating category: {ex.Message}");
        }
    }
}

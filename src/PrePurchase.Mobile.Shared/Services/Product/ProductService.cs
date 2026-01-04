using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Product;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.Product;

public class ProductService(IProductApi productApi) : IProductService
{
    private readonly IProductApi _productApi = productApi;

    public async Task<Result<PagedResult<ProductDto>>> SearchProductsAsync(
        string? searchTerm = null,
        Guid? shopId = null,
        Guid? categoryId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        bool? inStockOnly = null,
        int pageNumber = 1,
        int pageSize = 20)
    {
        try
        {
            var response = await _productApi.SearchProductsAsync(
                searchTerm, shopId, categoryId, minPrice, maxPrice, inStockOnly, pageNumber, pageSize);

            if (response.Success && response.Data != null)
            {
                return Result<PagedResult<ProductDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<PagedResult<ProductDto>>.Failure(response.Errors)
                : Result<PagedResult<ProductDto>>.Failure(response.Message ?? "Failed to search products");
        }
        catch (Exception ex)
        {
            return Result<PagedResult<ProductDto>>.Failure($"Error searching products: {ex.Message}");
        }
    }

    public async Task<Result<ProductDetailsDto>> GetProductByIdAsync(Guid id)
    {
        try
        {
            var response = await _productApi.GetProductByIdAsync(id);

            if (response.Success && response.Data != null)
            {
                return Result<ProductDetailsDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<ProductDetailsDto>.Failure(response.Errors)
                : Result<ProductDetailsDto>.Failure(response.Message ?? "Failed to fetch product details");
        }
        catch (Exception ex)
        {
            return Result<ProductDetailsDto>.Failure($"Error fetching product details: {ex.Message}");
        }
    }
}

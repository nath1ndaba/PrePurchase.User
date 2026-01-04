using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.Product;

namespace PrePurchase.Mobile.Shared.Services.Product;

public interface IProductService
{
    Task<Result<PagedResult<ProductDto>>> SearchProductsAsync(
        string? searchTerm = null,
        Guid? shopId = null,
        Guid? categoryId = null,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        bool? inStockOnly = null,
        int page = 1,
        int pageSize = 20);

    Task<Result<ProductDetailsDto>> GetProductByIdAsync(Guid id);
}

using PrePurchase.Mobile.Shared.Models.Collector;
using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.AuthorizedCollector;

public class AuthorizedCollectorService(IAuthorizedCollectorApi collectorApi) : IAuthorizedCollectorService
{
    private readonly IAuthorizedCollectorApi _collectorApi = collectorApi;

    public async Task<Result<List<AuthorizedCollectorDto>>> GetMyCollectorsAsync()
    {
        try
        {
            var response = await _collectorApi.GetMyCollectorsAsync();

            if (response.Success && response.Data != null)
            {
                return Result<List<AuthorizedCollectorDto>>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<List<AuthorizedCollectorDto>>.Failure(response.Errors)
                : Result<List<AuthorizedCollectorDto>>.Failure(response.Message ?? "Failed to fetch collectors");
        }
        catch (Exception ex)
        {
            return Result<List<AuthorizedCollectorDto>>.Failure($"Error fetching collectors: {ex.Message}");
        }
    }

    public async Task<Result<Guid>> AddCollectorAsync(Guid userId, string collectorName, string collectorPhone, string? relationship = null)
    {
        try
        {
            var command = new AddAuthorizedCollectorCommand
            {
                UserId = userId,
                CollectorName = collectorName,
                CollectorPhone = collectorPhone,
                Relationship = relationship
            };

            var response = await _collectorApi.AddCollectorAsync(command);

            if (response.Success && response.Data != default)
            {
                return Result<Guid>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<Guid>.Failure(response.Errors)
                : Result<Guid>.Failure(response.Message ?? "Failed to add collector");
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure($"Error adding collector: {ex.Message}");
        }
    }

    public async Task<Result<AuthorizedCollectorDto>> UpdateCollectorAsync(Guid id, Guid collectorId, Guid userId, string collectorName, string collectorPhone, string? relationship = null)
    {
        try
        {
            var command = new UpdateAuthorizedCollectorCommand
            {
                CollectorId = collectorId,
                UserId = userId,
                CollectorName = collectorName,
                CollectorPhone = collectorPhone,
                Relationship = relationship
            };

            var response = await _collectorApi.UpdateCollectorAsync(id, command);

            if (response.Success && response.Data != null)
            {
                return Result<AuthorizedCollectorDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<AuthorizedCollectorDto>.Failure(response.Errors)
                : Result<AuthorizedCollectorDto>.Failure(response.Message ?? "Failed to update collector");
        }
        catch (Exception ex)
        {
            return Result<AuthorizedCollectorDto>.Failure($"Error updating collector: {ex.Message}");
        }
    }

    public async Task<Result> DeactivateCollectorAsync(Guid id)
    {
        try
        {
            var response = await _collectorApi.DeactivateCollectorAsync(id);

            if (response.Success)
            {
                return Result.Success();
            }

            return response.Errors?.Count == 0
                ? Result.Failure(response.Errors)
                : Result.Failure(response.Message ?? "Failed to deactivate collector");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error deactivating collector: {ex.Message}");
        }
    }
}

using PrePurchase.Mobile.Shared.Models.Common;
using PrePurchase.Mobile.Shared.Models.User;
using PrePurchase.Mobile.Shared.Services.Api;

namespace PrePurchase.Mobile.Shared.Services.User;

public class UserService(IUserApi userApi) : IUserService
{
    private readonly IUserApi _userApi = userApi;

    public async Task<Result<UserProfileDto>> GetProfileAsync()
    {
        try
        {
            var response = await _userApi.GetProfileAsync();

            if (response.Success && response.Data != null)
            {
                return Result<UserProfileDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<UserProfileDto>.Failure(response.Errors)
                : Result<UserProfileDto>.Failure(response.Message ?? "Failed to fetch profile");
        }
        catch (Exception ex)
        {
            return Result<UserProfileDto>.Failure($"Error fetching profile: {ex.Message}");
        }
    }

    public async Task<Result<UserProfileDto>> UpdateProfileAsync(Guid userId, string? firstName = null, string? lastName = null, string? phoneNumber = null)
    {
        try
        {
            var command = new UpdateUserProfileCommand
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            var response = await _userApi.UpdateProfileAsync(command);

            if (response.Success && response.Data != null)
            {
                return Result<UserProfileDto>.Success(response.Data);
            }

            return response.Errors?.Count == 0
                ? Result<UserProfileDto>.Failure(response.Errors)
                : Result<UserProfileDto>.Failure(response.Message ?? "Failed to update profile");
        }
        catch (Exception ex)
        {
            return Result<UserProfileDto>.Failure($"Error updating profile: {ex.Message}");
        }
    }

    public async Task<Result> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        try
        {
            var command = new ChangePasswordCommand
            {
                UserId = userId,
                CurrentPassword = currentPassword,
                NewPassword = newPassword
            };

            var response = await _userApi.ChangePasswordAsync(command);

            if (response.Success)
            {
                return Result.Success();
            }

            return response.Errors?.Count == 0
                ? Result.Failure(response.Errors)
                : Result.Failure(response.Message ?? "Failed to change password");
        }
        catch (Exception ex)
        {
            return Result.Failure($"Error changing password: {ex.Message}");
        }
    }
}

namespace PrePurchase.Mobile.Shared.Models.Common;

// API Response Wrapper
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public List<string>? Errors { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message
        };
    }

    public static ApiResponse<T> ErrorResponse(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors ?? new List<string>()
        };
    }
}

//// Result Pattern (for service layer)
//public class Result<T>
//{
//    public bool IsSuccess { get; set; }
//    public T? Data { get; set; }
//    public string? ErrorMessage { get; set; }
//    public List<string>? ValidationErrors { get; set; }

//    public static Result<T> Success(T data)
//    {
//        return new Result<T>
//        {
//            IsSuccess = true,
//            Data = data
//        };
//    }

//    public static Result<T> Failure(string errorMessage, List<string>? validationErrors = null)
//    {
//        return new Result<T>
//        {
//            IsSuccess = false,
//            ErrorMessage = errorMessage,
//            ValidationErrors = validationErrors
//        };
//    }
//}

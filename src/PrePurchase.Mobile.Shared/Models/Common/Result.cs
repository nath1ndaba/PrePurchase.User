namespace PrePurchase.Mobile.Shared.Models.Common;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public string? Message { get; set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }
    public List<string>? Errors { get; private set; }

    private Result(bool isSuccess, T? data, string? message, string? errorMessage, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Message = message;
        ErrorMessage = errorMessage;
        Errors = errors;
    }

    // Success with data only
    public static Result<T> Success(T data) => new(true, data, null, null);

    // Success with data and message
    public static Result<T> Success(T data, string message) => new(true, data, message, null);

    // Failure with single error message
    public static Result<T> Failure(string errorMessage) => new(false, default, null, errorMessage);

    // Failure with list of errors
    public static Result<T> Failure(List<string> errors) => new(false, default, null, null, errors);

    // Failure with message and error message (for more context)
    public static Result<T> Failure(string message, string errorMessage) => new(false, default, message, errorMessage);
}

public class Result
{
    public bool IsSuccess { get; private set; }
    public string? Message { get; set; }
    public string? ErrorMessage { get; private set; }
    public List<string>? Errors { get; private set; }

    private Result(bool isSuccess, string? message, string? errorMessage, List<string>? errors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        ErrorMessage = errorMessage;
        Errors = errors;
    }

    // Success with no message
    public static Result Success() => new(true, null, null);

    // Success with message
    public static Result Success(string message) => new(true, message, null);

    // Failure with single error message
    public static Result Failure(string errorMessage) => new(false, null, errorMessage);

    // Failure with list of errors
    public static Result Failure(List<string> errors) => new(false, null, null, errors);

    // Failure with message and error message
    public static Result Failure(string message, string errorMessage) => new(false, message, errorMessage);
}
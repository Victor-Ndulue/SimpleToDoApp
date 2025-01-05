using System.Text.Json;

namespace SimpleToDoApp.Helpers.ObjectWrapper;

public class StandardResponse<T>
{
    public T? Data { get; set; }
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public int StatusCode { get; set; }
    public DateTime? RequestDate { get; set; } = DateTime.UtcNow;

    public StandardResponse(int statusCode, bool success, string msg, T? data)
    {
        Data = data;
        Succeeded = success;
        StatusCode = statusCode;
        Message = msg;
    }

    /// <summary>
    /// Application custom response message, 400 response means Validation Error
    /// </summary>
    /// <param name="errorMessage"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public static StandardResponse<T> Failed(T? data, string? errorMessage = "Request failed", int statusCode = 400)
    {
        ValidateStatusCode(statusCode);
        return new StandardResponse<T> ( success: false, msg: errorMessage, statusCode: statusCode, data: data );
    }

    /// <summary>
    /// Application custom response message, 200 means successful
    /// </summary>
    /// <param name="successMessage"></param>
    /// <param name="data"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public static StandardResponse<T> Success(T data, int statusCode = 200, string? successMessage = "Success")
    {
        ValidateStatusCode(statusCode);
        return new StandardResponse<T>(success: true, msg: successMessage, statusCode: statusCode, data: data);
    }

    /// <summary>
    /// Application custom response message, 500 means server error
    /// </summary>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public static StandardResponse<T> UnExpectedError(T? data, string? message = "Service currently unavailable. Kindly retry", int statusCode = 500)
    {
        ValidateStatusCode(statusCode);
        return new StandardResponse<T>(success: false, msg: message, statusCode: statusCode, data: data);
    }

    public override string ToString() => JsonSerializer.Serialize(this);

    private static void ValidateStatusCode(int statusCode) =>
    _ = statusCode > 0 ? statusCode : throw new ArgumentException("StatusCode must be positive.");


}

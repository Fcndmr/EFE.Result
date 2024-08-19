using System.Text.Json.Serialization;

namespace EFE.Result;

public enum ResultStatus
{
    Success = 200,
    NotFound = 404,
    BadRequest = 400,
    InternalServerError = 500,
    Unauthorized = 401,
    Forbidden = 403
}

public class ErrorDetail
{
    public string Message { get; set; }
    public string Code { get; set; }
    public string? Target { get; set; }

    public ErrorDetail(string message, string code, string? target = null)
    {
        Message = message;
        Code = code;
        Target = target;
    }
}

public sealed class Result<T>
{
    public T? Data { get; set; }
    public List<ErrorDetail>? ErrorMessages { get; set; }
    public bool IsSuccessful { get; set; } = true;

    [JsonIgnore]
    public ResultStatus StatusCode { get; set; } = ResultStatus.Success;

    public Result(T data)
    {
        Data = data;
    }

    public Result(ResultStatus statusCode, List<ErrorDetail> errorMessages)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }

    public Result(ResultStatus statusCode, ErrorDetail errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = new() { errorMessage };
    }

    public static implicit operator Result<T>(T data)
    {
        return new(data);
    }

    public static implicit operator Result<T>((ResultStatus statusCode, List<ErrorDetail> errorMessages) parameters)
    {
        return new(parameters.statusCode, parameters.errorMessages);
    }

    public static implicit operator Result<T>((ResultStatus statusCode, ErrorDetail errorMessage) parameters)
    {
        return new(parameters.statusCode, parameters.errorMessage);
    }

    public static Result<T> Succeed(T data)
    {
        return new(data);
    }

    public static Result<T> Failure(ResultStatus statusCode, params ErrorDetail[] errorMessages)
    {
        return new(statusCode, errorMessages.ToList());
    }

    public static Result<T> Failure(params ErrorDetail[] errorMessages)
    {
        return new(ResultStatus.InternalServerError, errorMessages.ToList());
    }

    public void LogErrors()
    {
        if (!IsSuccessful && ErrorMessages != null)
        {
            foreach (var error in ErrorMessages)
            {
                Console.WriteLine($"Error: {error.Message}, Code: {error.Code}, Target: {error.Target}");
            }
        }
    }
}

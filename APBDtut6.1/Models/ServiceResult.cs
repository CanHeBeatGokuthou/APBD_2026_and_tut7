namespace APBDtut6._1.Models;

public class ServiceResult
{
    public bool IsSuccess => ErrorTypes == ErrorTypes.None;
    public ErrorTypes ErrorTypes { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;

    public static ServiceResult Success() => new() { ErrorTypes = ErrorTypes.None };
    public static ServiceResult NotFound(string msg) => new() { ErrorTypes = ErrorTypes.NotFound, ErrorMessage = msg };
    public static ServiceResult BadRequest(string msg) => new() { ErrorTypes = ErrorTypes.BadRequest, ErrorMessage = msg };
    public static ServiceResult Conflict(string msg) => new() { ErrorTypes = ErrorTypes.Conflict, ErrorMessage = msg };
}
public class ServiceResult<T> : ServiceResult
{
    public T? Data { get; set; }
    public static ServiceResult<T> Success(T data) => new() { ErrorTypes = ErrorTypes.None, Data = data };
    
    public static new ServiceResult<T> NotFound(string msg) => new() { ErrorTypes = ErrorTypes.NotFound, ErrorMessage = msg };
    public static new ServiceResult<T> BadRequest(string msg) => new() { ErrorTypes = ErrorTypes.BadRequest, ErrorMessage = msg };
    public static new ServiceResult<T> Conflict(string msg) => new() { ErrorTypes = ErrorTypes.Conflict, ErrorMessage = msg };
}
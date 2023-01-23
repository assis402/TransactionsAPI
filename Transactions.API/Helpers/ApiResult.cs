using System.Net;

namespace Transactions.API.Helpers;

public record struct ApiResult<T>
{
    public bool Success { get; init; }
    public int StatusCode { get; init; }
    public string? Title { get; init; }
    public T? Data { get; init; }

    public ApiResult(bool success, string? title, T? data, HttpStatusCode statusCode)
    {
        Success = success;
        StatusCode = (int)statusCode;
        Title = title;
        Data = data;
    }
}
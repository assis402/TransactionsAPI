using System.Net;
using System.Text.Json.Serialization;

namespace Transactions.API.Helpers;

public record struct ApiResult<T>
{
    public bool Success { get; private set; }
    public HttpStatusCode StatusCode { get; private set; }
    public string? Title { get; private set; }
    public T? Data { get; private set; }

    public ApiResult(bool success, string? title, T? data, HttpStatusCode statusCode)
    {
        Success = success;
        StatusCode = statusCode;
        Title = title;
        Data = data;
    }
}
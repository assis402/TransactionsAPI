using System.Net;

namespace Transactions.API.Helpers;

public static class ResultHelper
{
    public static IResult SuccessResult<T>(T data)
        => Results.Ok(new ApiResult<T>(true, null, data, HttpStatusCode.OK));

    public static IResult SuccessResult(string title)
        => Results.Ok(new ApiResult<object>(true, title, null, HttpStatusCode.OK));

    public static IResult NotFoundResult<T>(T data)
        => Results.NotFound(new ApiResult<T>(true, null, data, HttpStatusCode.OK));

    public static IResult ErrorResult(string errorTitle)
        => Results.BadRequest(new ApiResult<string>(false, errorTitle, null, HttpStatusCode.BadRequest));

    public static IResult ErrorResult(IDictionary<string, string[]> errors)
        => Results.BadRequest(new ApiResult<IDictionary<string, string[]>>(false, "One or more validation errors occurred.", errors, HttpStatusCode.BadRequest));

    public static IResult CriticalErrorResult(string errorMessage)
        => Results.BadRequest(new ApiResult<string>(false, "An internal system error has occurred.", errorMessage, HttpStatusCode.BadRequest));
}
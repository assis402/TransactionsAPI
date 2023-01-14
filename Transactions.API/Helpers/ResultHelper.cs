namespace Transactions.API.Helpers;

public static class ResultHelper 
{
    public static ApiResult<T> SuccessResult<T>(T data)
        => new ApiResult<T>(true, null, data);
    
    public static ApiResult<string> ErrorResult(string errorTitle)
        => new ApiResult<string>(false, errorTitle, null);

    public static ApiResult<IDictionary<string, string[]>> ErrorResult(IDictionary<string, string[]> errors)
        => new ApiResult<IDictionary<string, string[]>>(false, "One or more validation errors occurred.", errors);

    public static ApiResult<string> CriticalErrorResult(string errorMessage)
        => new ApiResult<string>(false, "An internal system error has occurred.", errorMessage);
}
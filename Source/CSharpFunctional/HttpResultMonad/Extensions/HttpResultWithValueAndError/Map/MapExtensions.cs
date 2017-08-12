using System.Diagnostics;

namespace HttpResultMonad.Extensions.HttpResultWithValueAndError.Map
{
    public static class MapExtensions
    {
        [DebuggerStepThrough]
        public static HttpResultWithError<TError> ToHttpResultWithError<TValue, TError>(this HttpResult<TValue, TError> result)
        {
            return result.IsSuccess
                ? HttpResultWithError.Ok<TError>(result.HttpState)
                : HttpResultWithError.Fail(result.Error, result.HttpState);
        }
    }
}

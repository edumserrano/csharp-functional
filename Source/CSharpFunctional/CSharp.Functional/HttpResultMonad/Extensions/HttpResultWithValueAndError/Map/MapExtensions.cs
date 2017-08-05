using System.Diagnostics;

namespace CSharp.Functional.HttpResultMonad.Extensions.HttpResultWithValueAndError.Map
{
    public static class MapExtensions
    {
        [DebuggerStepThrough]
        public static HttpResultError<TError> ToHttpResultError<TValue, TError>(this HttpResult<TValue, TError> result)
        {
            return result.IsSuccess
                ? HttpResultError.Ok<TError>(result.HttpState)
                : HttpResultError.Fail(result.Error, result.HttpState);
        }
    }
}

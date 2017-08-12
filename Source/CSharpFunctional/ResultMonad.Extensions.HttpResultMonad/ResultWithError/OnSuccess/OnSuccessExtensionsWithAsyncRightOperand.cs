using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HttpResultMonad;

namespace ResultMonad.Extensions.HttpResultMonad.ResultWithError.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncRightOperand
    {
        [DebuggerStepThrough]
        public static Task<HttpResult<TValue, TError>> OnSuccessToHttpResultWithValueAndError<TValue, TError>(
            this ResultWithError<TError> resultWithError,
            Func<Task<HttpResult<TValue, TError>>> func)
        {
            return resultWithError.IsFailure
                ? Task.FromResult(HttpResult.Fail<TValue, TError>(resultWithError.Error))
                : func();
        }

        [DebuggerStepThrough]
        public static Task<HttpResultWithError<TError>> OnSuccessToHttpResultWithError<TError>(
            this ResultWithError<TError> resultWithError,
            Func<Task<HttpResultWithError<TError>>> func)
        {
            return resultWithError.IsFailure
                ? Task.FromResult(HttpResultWithError.Fail(resultWithError.Error))
                : func();
        }
    }
}
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HttpResultMonad;

namespace ResultMonad.Extensions.HttpResultMonad.ResultWithErrorMonad.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncRightOperand
    {
        [DebuggerStepThrough]
        public static Task<HttpResult<TValue, TError>> OnSuccessToHttpResultWithValueAndError<TValue, TError>(
            this ResultWithError<TError> resultWithError,
            Func<Task<HttpResult<TValue, TError>>> onSuccessFunc)
        {
            return resultWithError.IsFailure
                ? Task.FromResult(HttpResult.Fail<TValue, TError>(resultWithError.Error))
                : onSuccessFunc();
        }

        [DebuggerStepThrough]
        public static Task<HttpResultWithError<TError>> OnSuccessToHttpResultWithError<TError>(
            this ResultWithError<TError> resultWithError,
            Func<Task<HttpResultWithError<TError>>> onSuccessFunc)
        {
            return resultWithError.IsFailure
                ? Task.FromResult(HttpResultWithError.Fail(resultWithError.Error))
                : onSuccessFunc();
        }
    }
}
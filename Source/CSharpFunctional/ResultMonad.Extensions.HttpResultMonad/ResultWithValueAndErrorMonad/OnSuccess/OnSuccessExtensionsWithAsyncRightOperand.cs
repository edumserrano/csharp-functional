using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HttpResultMonad;

namespace ResultMonad.Extensions.HttpResultMonad.ResultWithValueAndErrorMonad.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncRightOperand
    {
        [DebuggerStepThrough]
        public static Task<HttpResultWithError<TError>> OnSuccessToHttpResultWithError<TValue, TError>(
            this Result<TValue, TError> result,
            Func<TValue, Task<HttpResultWithError<TError>>> onSuccessFunc)
        {
            return result.IsFailure
                ? Task.FromResult(HttpResultWithError.Fail(result.Error))
                : onSuccessFunc(result.Value);
        }

        [DebuggerStepThrough]
        public static Task<HttpResult<KValue, TError>> OnSuccessToHttpResultWithValueAndError<TValue, TError, KValue>(
            this Result<TValue, TError> result,
            Func<TValue, Task<HttpResult<KValue, TError>>> onSuccessFunc)
        {
            return result.IsFailure
                ? Task.FromResult(HttpResult.Fail<KValue, TError>(result.Error))
                : onSuccessFunc(result.Value);
        }
    }
}

using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpResultMonad.Extensions.HttpResultWithValueAndErrorMonad.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncRightOperand
    {
        [DebuggerStepThrough]
        public static Task<HttpResult<KValue, TError>> OnSuccessToHttpResultWithValueAndError<TValue, TError, KValue>(
            this HttpResult<TValue, TError> httpResult,
            Func<TValue, Task<HttpResult<KValue, TError>>> onSuccessFunc)
        {
            return httpResult.IsFailure
                ? Task.FromResult(HttpResult.Fail<KValue, TError>(httpResult.Error, httpResult.HttpState))
                : onSuccessFunc(httpResult.Value);
        }
    }
}
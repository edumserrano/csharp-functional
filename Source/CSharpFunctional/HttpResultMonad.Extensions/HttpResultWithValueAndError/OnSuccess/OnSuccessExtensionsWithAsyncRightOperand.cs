using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpResultMonad.Extensions.HttpResultWithValueAndError.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncRightOperand
    {
        [DebuggerStepThrough]
        public static Task<HttpResult<KValue, TError>> OnSuccessToHttpResultWithValueAndError<TValue, TError, KValue>(
            this HttpResult<TValue, TError> httpResult,
            Func<TValue, Task<HttpResult<KValue, TError>>> func)
        {
            return httpResult.IsFailure
                ? Task.FromResult(HttpResult.Fail<KValue, TError>(httpResult.Error, httpResult.HttpState))
                : func(httpResult.Value);
        }
    }
}
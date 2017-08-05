using System;
using System.Diagnostics;

namespace CSharp.Functional.HttpResultMonad.Extensions.HttpResultWithError.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> OnSuccessToHttpResultWithValueAndError<TValue, TError>(
            this HttpResultError<TError> httpResultError,
            Func<TValue> func)
        {
            if (httpResultError.IsFailure)
            {
                return HttpResult.Fail<TValue, TError>(httpResultError.Error, httpResultError.HttpState);
            }

            var newValue = func();
            return HttpResult.Ok<TValue, TError>(newValue, httpResultError.HttpState);
        }
    }
}
using System;
using System.Diagnostics;

namespace HttpResultMonad.Extensions.HttpResultWithErrorMonad.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static HttpResult<TValue, TError> OnSuccessToHttpResultWithValueAndError<TValue, TError>(
            this HttpResultWithError<TError> httpResultWithError,
            Func<TValue> onSuccessFunc)
        {
            if (httpResultWithError.IsFailure)
            {
                return HttpResult.Fail<TValue, TError>(httpResultWithError.Error, httpResultWithError.HttpState);
            }

            var newValue = onSuccessFunc();
            return HttpResult.Ok<TValue, TError>(newValue, httpResultWithError.HttpState);
        }
    }
}
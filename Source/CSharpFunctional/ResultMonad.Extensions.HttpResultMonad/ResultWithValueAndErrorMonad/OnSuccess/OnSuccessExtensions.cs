using System;
using System.Diagnostics;
using HttpResultMonad;
using HttpResultMonad.State;

namespace ResultMonad.Extensions.HttpResultMonad.ResultWithValueAndErrorMonad.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static HttpResult<KValue, TError> OnSuccessToHttpResultWithValueAndError<TValue, TError, KValue>(
            this Result<TValue, TError> result,
            Func<TValue, KValue> onSuccessFunc)
        {
            return result.IsFailure
                ? HttpResult.Fail<KValue, TError>(result.Error, HttpState.Empty)
                : HttpResult.Ok<KValue, TError>(onSuccessFunc(result.Value), HttpState.Empty);
        }
    }
}

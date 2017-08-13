using System;
using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithValueMonad.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static Result<KValue> OnSuccessToResultWithValue<TValue, KValue>(
           this Result<TValue> result,
           Func<TValue, Result<KValue>> onSuccessFunc)
        {
            return result.IsFailure
                ? Result.Fail<KValue>()
                : onSuccessFunc(result.Value);
        }
    }
}

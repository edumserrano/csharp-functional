using System;
using System.Diagnostics;

namespace CSharp.Functional.ResultMonad.Extensions.ResultWithValue.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static Result<KValue> OnSuccessToResultWithValue<TValue, KValue>(
           this Result<TValue> result,
           Func<TValue, Result<KValue>> valueFunc)
        {
            return result.IsFailure
                ? Result.Fail<KValue>()
                : valueFunc(result.Value);
        }
    }
}

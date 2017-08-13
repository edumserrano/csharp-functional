using System;
using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithErrorMonad.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static Result<TValue, TError> OnSuccessToResultWithValueAndError<TValue, TError>(
            this ResultWithError<TError> resultWithError,
            Func<TValue> onSuccessFunc)
        {
            if (resultWithError.IsFailure)
            {
                return Result.Fail<TValue, TError>(resultWithError.Error);
            }

            var newValue = onSuccessFunc();
            return Result.Ok<TValue, TError>(newValue);
        }
    }
}
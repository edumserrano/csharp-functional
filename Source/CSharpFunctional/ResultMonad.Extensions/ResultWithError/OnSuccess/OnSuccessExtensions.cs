using System;
using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithError.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static Result<TValue, TError> OnSuccessToResultWithValueAndError<TValue, TError>(
            this ResultWithError<TError> resultWithError,
            Func<TValue> func)
        {
            if (resultWithError.IsFailure)
            {
                return Result.Fail<TValue, TError>(resultWithError.Error);
            }

            var newValue = func();
            return Result.Ok<TValue, TError>(newValue);
        }
    }
}
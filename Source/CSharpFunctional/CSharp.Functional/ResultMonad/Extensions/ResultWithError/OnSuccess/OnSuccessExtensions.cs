using System;
using System.Diagnostics;

namespace CSharp.Functional.ResultMonad.Extensions.ResultWithError.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static Result<TValue, TError> OnSuccessToResultWithValueAndError<TValue, TError>(
            this ResultError<TError> resultError,
            Func<TValue> func)
        {
            if (resultError.IsFailure)
            {
                return Result.Fail<TValue, TError>(resultError.Error);
            }

            var newValue = func();
            return Result.Ok<TValue, TError>(newValue);
        }
    }
}
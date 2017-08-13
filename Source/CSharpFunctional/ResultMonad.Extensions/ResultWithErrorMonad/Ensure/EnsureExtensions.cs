using System;
using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithErrorMonad.Ensure
{
    public static class EnsureExtensions
    {
        [DebuggerStepThrough]
        public static ResultWithError<TError> EnsureToResultWithError<TError>(
            this ResultWithError<TError> resultWithError,
            Func<bool> predicate,
            TError error)
        {
            if (resultWithError.IsFailure)
            {
                return ResultMonad.ResultWithError.Fail(resultWithError.Error);
            }

            return predicate()
                ? ResultMonad.ResultWithError.Ok<TError>()
                : ResultMonad.ResultWithError.Fail(error);
        }
    }
}

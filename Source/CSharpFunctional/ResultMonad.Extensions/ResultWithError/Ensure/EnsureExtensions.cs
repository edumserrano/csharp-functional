using System;
using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithError.Ensure
{
    public static class EnsureExtensions
    {
        [DebuggerStepThrough]
        public static ResultError<TError> EnsureToResultError<TError>(
            this ResultError<TError> resultError,
            Func<bool> predicate,
            TError error)
        {
            if (resultError.IsFailure)
            {
                return ResultError.Fail(resultError.Error);
            }

            return predicate()
                ? ResultError.Ok<TError>()
                : ResultError.Fail(error);
        }
    }
}

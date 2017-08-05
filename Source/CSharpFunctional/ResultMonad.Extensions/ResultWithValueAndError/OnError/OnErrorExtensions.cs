using System;
using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithValueAndError.OnError
{
    public static class OnErrorExtensions
    {
        [DebuggerStepThrough]
        public static Result<TValue, KError> OnErrorToResultWithValueAndError<TValue, TError, KError>(
            this Result<TValue, TError> result,
            Func<TError, KError> func)
        {
            if (result.IsSuccess)
            {
                return Result.Ok<TValue, KError>(result.Value);
            }

            var error = func(result.Error);
            return Result.Fail<TValue, KError>(error);
        }

        [DebuggerStepThrough]
        public static ResultError<KError> OnErrorToResultError<TValue, TError, KError>(
            this Result<TValue, TError> result,
            Func<TError, KError> func)
        {
            if (result.IsSuccess)
            {
                return ResultError.Ok<KError>();
            }

            var error = func(result.Error);
            return ResultError.Fail(error);
        }
    }
}

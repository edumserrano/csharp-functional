using System;
using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithValueAndErrorMonad.OnError
{
    public static class OnErrorExtensions
    {
        [DebuggerStepThrough]
        public static Result<TValue, KError> OnErrorToResultWithValueAndError<TValue, TError, KError>(
            this Result<TValue, TError> result,
            Func<TError, KError> onErrorFunc)
        {
            if (result.IsSuccess)
            {
                return Result.Ok<TValue, KError>(result.Value);
            }

            var error = onErrorFunc(result.Error);
            return Result.Fail<TValue, KError>(error);
        }

        [DebuggerStepThrough]
        public static ResultWithError<KError> OnErrorToResultWithError<TValue, TError, KError>(
            this Result<TValue, TError> result,
            Func<TError, KError> onErrorFunc)
        {
            if (result.IsSuccess)
            {
                return ResultMonad.ResultWithError.Ok<KError>();
            }

            var error = onErrorFunc(result.Error);
            return ResultMonad.ResultWithError.Fail(error);
        }
    }
}

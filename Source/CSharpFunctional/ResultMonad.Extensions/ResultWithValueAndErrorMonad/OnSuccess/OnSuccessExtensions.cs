using System;
using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithValueAndErrorMonad.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static Result<KValue, TError> OnSuccessToResultWithValueAndError<TValue, TError, KValue>(
            this Result<TValue, TError> result,
            Func<TValue, Result<KValue, TError>> onSuccessFunc)
        {
            if (result.IsFailure)
            {
                return Result.Fail<KValue, TError>(result.Error);
            }

            return onSuccessFunc(result.Value);
        }

        [DebuggerStepThrough]
        public static Result<KValue, TError> OnSuccessToResultWithValueAndError<TValue, TError, KValue>(
            this Result<TValue, TError> result,
            Func<TValue, KValue> onSuccessFunc)
        {
            if (result.IsFailure)
            {
                return Result.Fail<KValue, TError>(result.Error);
            }

            var newValue = onSuccessFunc(result.Value);
            return Result.Ok<KValue, TError>(newValue);
        }

        [DebuggerStepThrough]
        public static Result<TValue, TError> OnSuccess<TValue, TError>(
            this Result<TValue, TError> result,
            Action<TValue> onSuccessAction)
        {
            if (result.IsSuccess)
            {
                onSuccessAction(result.Value);
            }
;
            return result;
        }
    }
}

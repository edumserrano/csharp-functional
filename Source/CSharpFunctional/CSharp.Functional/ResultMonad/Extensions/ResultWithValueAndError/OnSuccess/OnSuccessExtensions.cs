using System;
using System.Diagnostics;
using CSharp.Functional.HttpResultMonad;

namespace CSharp.Functional.ResultMonad.Extensions.ResultWithValueAndError.OnSuccess
{
    public static class OnSuccessExtensions
    {
        [DebuggerStepThrough]
        public static Result<KValue, TError> OnSuccessToResultWithValueAndError<TValue, TError, KValue>(
            this Result<TValue, TError> result,
            Func<TValue, Result<KValue, TError>> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<KValue, TError>(result.Error);
            }

            return func(result.Value);
        }

        [DebuggerStepThrough]
        public static Result<KValue, TError> OnSuccessToResultWithValueAndError<TValue, TError, KValue>(
            this Result<TValue, TError> result,
            Func<TValue, KValue> func)
        {
            if (result.IsFailure)
            {
                return Result.Fail<KValue, TError>(result.Error);
            }

            var newValue = func(result.Value);
            return Result.Ok<KValue, TError>(newValue);
        }


        [DebuggerStepThrough]
        public static HttpResult<KValue, TError> OnSuccessToHttpResultWithValueAndError<TValue, TError, KValue>(
            this Result<TValue, TError> result,
            Func<TValue, KValue> func)
        {
            return result.IsFailure
                ? HttpResult.Fail<KValue, TError>(result.Error)
                : HttpResult.Ok<KValue, TError>(func(result.Value));
        }

        [DebuggerStepThrough]
        public static Result<TValue, TError> OnSuccess<TValue, TError>(
            this Result<TValue, TError> result,
            Action<TValue> action)
        {
            if (result.IsSuccess)
            {
                action(result.Value);
            }
;
            return result;
        }
    }
}

using System;
using System.Diagnostics;
using CSharp.Functional.HttpResultMonad;

namespace CSharp.Functional.ResultMonad.Extensions.ResultWithValueAndError.OnError
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

        [DebuggerStepThrough]
        public static HttpResult<TValue, KError> OnErrorToHttpResultWithValueAndError<TValue, TError, KError>(
           this Result<TValue, TError> result,
           Func<TError, KError> errorFunc)
        {
            if (result.IsSuccess)
            {
                return HttpResult.Ok<TValue, KError>(result.Value);
            }

            var newError = errorFunc(result.Error);
            return HttpResult.Fail<TValue, KError>(newError);
        }
    }
}

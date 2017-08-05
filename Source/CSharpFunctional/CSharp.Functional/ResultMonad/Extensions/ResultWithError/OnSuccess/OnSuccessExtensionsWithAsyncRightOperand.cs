using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CSharp.Functional.HttpResultMonad;

namespace CSharp.Functional.ResultMonad.Extensions.ResultWithError.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncRightOperand
    {
        [DebuggerStepThrough]
        public static Task<HttpResult<TValue, TError>> OnSuccessToHttpResultWithValueAndError<TValue, TError>(
            this ResultError<TError> resultError,
            Func<Task<HttpResult<TValue, TError>>> func)
        {
            return resultError.IsFailure
                ? Task.FromResult(HttpResult.Fail<TValue, TError>(resultError.Error))
                : func();
        }

        [DebuggerStepThrough]
        public static Task<HttpResultError<TError>> OnSuccessToHttpResultError<TError>(
            this ResultError<TError> resultError,
            Func<Task<HttpResultError<TError>>> func)
        {
            return resultError.IsFailure
                ? Task.FromResult(HttpResultError.Fail(resultError.Error))
                : func();
        }
    }
}
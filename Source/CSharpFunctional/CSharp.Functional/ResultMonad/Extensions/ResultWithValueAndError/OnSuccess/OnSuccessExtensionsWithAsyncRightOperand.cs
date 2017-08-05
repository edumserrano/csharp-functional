using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CSharp.Functional.HttpResultMonad;

namespace CSharp.Functional.ResultMonad.Extensions.ResultWithValueAndError.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncRightOperand
    {
        [DebuggerStepThrough]
        public static Task<HttpResultError<TError>> OnSuccessToHttpResultError<TValue, TError>(
            this Result<TValue, TError> result,
            Func<TValue, Task<HttpResultError<TError>>> func)
        {
            return result.IsFailure
                ? Task.FromResult(HttpResultError.Fail(result.Error))
                : func(result.Value);
        }

    }
}

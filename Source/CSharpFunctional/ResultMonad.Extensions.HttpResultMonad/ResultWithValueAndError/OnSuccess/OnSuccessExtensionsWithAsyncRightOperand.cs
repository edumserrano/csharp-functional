using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HttpResultMonad;

namespace ResultMonad.Extensions.HttpResultMonad.ResultWithValueAndError.OnSuccess
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

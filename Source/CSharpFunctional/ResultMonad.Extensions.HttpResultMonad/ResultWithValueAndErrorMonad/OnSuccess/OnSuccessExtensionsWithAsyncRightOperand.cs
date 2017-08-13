using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HttpResultMonad;

namespace ResultMonad.Extensions.HttpResultMonad.ResultWithValueAndErrorMonad.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncRightOperand
    {
        [DebuggerStepThrough]
        public static Task<HttpResultWithError<TError>> OnSuccessToHttpResultWithError<TValue, TError>(
            this Result<TValue, TError> result,
            Func<TValue, Task<HttpResultWithError<TError>>> func)
        {
            return result.IsFailure
                ? Task.FromResult(HttpResultWithError.Fail(result.Error))
                : func(result.Value);
        }

    }
}

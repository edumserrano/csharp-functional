using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpResultMonad.Extensions.HttpResultWithValueAndErrorMonad.OnSuccess
{
    public static class OnSuccessExtensionsWithAsyncBothOperands
    {
        [DebuggerStepThrough]
        public static async Task<HttpResult<KValue, TError>> OnSuccessToHttpResultWithValueAndError<TValue, TError, KValue>(
            this Task<HttpResult<TValue, TError>> result,
            Func<TValue, Task<HttpResult<KValue, TError>>> onSuccessFunc)
        {
            var taskResult = await result.ConfigureAwait(false);
            return taskResult.IsFailure
                ? HttpResult.Fail<KValue, TError>(taskResult.Error, taskResult.HttpState)
                : await onSuccessFunc(taskResult.Value).ConfigureAwait(false);
        }
    }
}
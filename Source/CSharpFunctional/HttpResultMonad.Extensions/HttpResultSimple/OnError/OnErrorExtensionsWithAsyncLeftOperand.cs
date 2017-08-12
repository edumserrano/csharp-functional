using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpResultMonad.Extensions.HttpResultSimple.OnError
{
    public static class OnErrorExtensionsWithAsyncLeftOperand
    {
        [DebuggerStepThrough]
        public static async Task<HttpResultWithError<TError>> OnErrorToHttpResultWithError<TError>(
            this Task<HttpResult> httpResultTask,
            Func<TError> errorFunc)
        {
            var httpResult = await httpResultTask.ConfigureAwait(false);
            if (httpResult.IsSuccess)
            {
                return HttpResultMonad.HttpResultWithError.Ok<TError>(httpResult.HttpState);
            }

            var error = errorFunc();
            return HttpResultMonad.HttpResultWithError.Fail(error, httpResult.HttpState);
        }
    }
}

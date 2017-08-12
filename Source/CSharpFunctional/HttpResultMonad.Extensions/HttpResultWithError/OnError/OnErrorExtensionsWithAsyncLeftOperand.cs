using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpResultMonad.Extensions.HttpResultWithError.OnError
{
    public static class OnErrorExtensionsWithAsyncLeftOperand
    {
        [DebuggerStepThrough]
        public static async Task<HttpResultWithError<KError>> OnErrorToHttpResultWithError<TError, KError>(
             this Task<HttpResultWithError<TError>> httpResultTask,
             Func<TError, KError> errorFunc)
        {
            var httpResult = await httpResultTask.ConfigureAwait(false);
            if (httpResult.IsSuccess)
            {
                return HttpResultMonad.HttpResultWithError.Ok<KError>(httpResult.HttpState);
            }

            var error = errorFunc(httpResult.Error);
            return HttpResultMonad.HttpResultWithError.Fail(error, httpResult.HttpState);
        }
    }
}

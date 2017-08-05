using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpResultMonad.Extensions.HttpResultWithError.OnError
{
    public static class OnErrorExtensionsWithAsyncLeftOperand
    {
        [DebuggerStepThrough]
        public static async Task<HttpResultError<KError>> OnErrorToHttpResultError<TError, KError>(
             this Task<HttpResultError<TError>> httpResultTask,
             Func<TError, KError> errorFunc)
        {
            var httpResult = await httpResultTask.ConfigureAwait(false);
            if (httpResult.IsSuccess)
            {
                return HttpResultError.Ok<KError>(httpResult.HttpState);
            }

            var error = errorFunc(httpResult.Error);
            return HttpResultError.Fail(error, httpResult.HttpState);
        }
    }
}

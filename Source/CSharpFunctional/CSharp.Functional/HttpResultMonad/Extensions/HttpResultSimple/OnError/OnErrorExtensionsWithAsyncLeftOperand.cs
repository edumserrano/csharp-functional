using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CSharp.Functional.HttpResultMonad.Extensions.HttpResultSimple.OnError
{
    public static class OnErrorExtensionsWithAsyncLeftOperand
    {
        [DebuggerStepThrough]
        public static async Task<HttpResultError<TError>> OnErrorToHttpResultError<TError>(
            this Task<HttpResult> httpResultTask,
            Func<TError> errorFunc)
        {
            var httpResult = await httpResultTask.ConfigureAwait(false);
            if (httpResult.IsSuccess)
            {
                return HttpResultError.Ok<TError>(httpResult.HttpState);
            }

            var error = errorFunc();
            return HttpResultError.Fail(error, httpResult.HttpState);
        }
    }
}

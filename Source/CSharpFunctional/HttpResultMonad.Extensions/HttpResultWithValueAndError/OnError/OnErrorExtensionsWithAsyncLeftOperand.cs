using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpResultMonad.Extensions.HttpResultWithValueAndError.OnError
{
    public static class OnErrorExtensionsWithAsyncLeftOperand
    {
        [DebuggerStepThrough]
        public static async Task<HttpResult<TValue, KError>> OnErrorToHttpResultWithValueAndError<TValue, TError, KError>(
            this Task<HttpResult<TValue, TError>> httpResultTask,
            Func<TError, KError> errorFunc)
        {
            var httpResult = await httpResultTask.ConfigureAwait(false);
            if (httpResult.IsSuccess)
            {
                return HttpResult.Ok<TValue, KError>(httpResult.Value, httpResult.HttpState);
            }

            var error = errorFunc(httpResult.Error);
            return HttpResult.Fail<TValue, KError>(error, httpResult.HttpState);
        }
    }
}

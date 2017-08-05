using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HttpResultMonad.Extensions.HttpResultWithValue.OnError
{
    public static class OnErrorExtensionsWithAsyncLeftOperand
    {
        [DebuggerStepThrough]
        public static async Task<HttpResult<TValue, TError>> OnErrorToHttpResultWithValueAndError<TValue, TError>(
            this Task<HttpResult<TValue>> httpResultTask,
            Func<TError> errorFunc)
        {
            var httpResult = await httpResultTask.ConfigureAwait(false);
            if (httpResult.IsSuccess)
            {
                return HttpResult.Ok<TValue, TError>(httpResult.Value, httpResult.HttpState);
            }

            var error = errorFunc();
            return HttpResult.Fail<TValue, TError>(error, httpResult.HttpState);
        }
    }
}

using System;
using System.Diagnostics;
using HttpResultMonad;

namespace ResultMonad.Extensions.HttpResultMonad.ResultWithValueAndError.OnError
{
    public static class OnErrorExtensions
    {
        [DebuggerStepThrough]
        public static HttpResult<TValue, KError> OnErrorToHttpResultWithValueAndError<TValue, TError, KError>(
           this Result<TValue, TError> result,
           Func<TError, KError> errorFunc)
        {
            if (result.IsSuccess)
            {
                return HttpResult.Ok<TValue, KError>(result.Value);
            }

            var newError = errorFunc(result.Error);
            return HttpResult.Fail<TValue, KError>(newError);
        }
    }
}

using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithValueAndErrorMonad.Map
{
    public static class MapExtensions
    {
        [DebuggerStepThrough]
        public static ResultWithError<TError> ToResultWithError<TValue, TError>(this Result<TValue, TError> result)
        {
            return result.IsSuccess
                ? ResultWithError.Ok<TError>()
                : ResultWithError.Fail(result.Error);
        }
    }
}

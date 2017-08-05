using System.Diagnostics;

namespace CSharp.Functional.ResultMonad.Extensions.ResultWithValueAndError.Map
{
    public static class MapExtensions
    {
        [DebuggerStepThrough]
        public static ResultError<TError> ToResultError<TValue, TError>(this Result<TValue, TError> result)
        {
            return result.IsSuccess
                ? ResultError.Ok<TError>()
                : ResultError.Fail(result.Error);
        }
    }
}

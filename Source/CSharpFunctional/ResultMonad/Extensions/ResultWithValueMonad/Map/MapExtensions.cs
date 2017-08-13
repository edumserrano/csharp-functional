using System.Diagnostics;

namespace ResultMonad.Extensions.ResultWithValueMonad.Map
{
    public static class MapExtensions
    {
        [DebuggerStepThrough]
        public static Result ToResult<TValue>(this Result<TValue> result)
        {
            return result.IsSuccess
                ? Result.Ok()
                : Result.Fail();
        }
    }
}

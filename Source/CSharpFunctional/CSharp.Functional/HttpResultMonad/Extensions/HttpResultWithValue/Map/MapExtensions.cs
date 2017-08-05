using System.Diagnostics;

namespace CSharp.Functional.HttpResultMonad.Extensions.HttpResultWithValue.Map
{
    public static class MapExtensions
    {
        [DebuggerStepThrough]
        public static HttpResult ToHttpResult<TValue>(this HttpResult<TValue> result)
        {
            return result.IsSuccess
                ? HttpResult.Ok(result.HttpState)
                : HttpResult.Fail(result.HttpState);
        }
    }
}

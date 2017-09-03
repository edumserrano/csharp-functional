using HttpResultMonad.Extensions.HttpResultWithValue.Map;
using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.Extensions.HttpResultWithValue.Map
{
    [Trait("Monad", "HttpResultSimple")]
    public class ToResultTests
    {
        [Fact]
        public void To_propagates_ok_status_to_HttpResultWithValue()
        {
            var result = HttpResult.Ok(1);
            var httpResult = result.ToHttpResult();
            httpResult.IsSuccess.ShouldBe(result.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_status_to_HttpResultWithValue()
        {
            var result = HttpResult.Fail<int>();
            var httpResult = result.ToHttpResult();
            httpResult.IsFailure.ShouldBe(result.IsFailure);
        }

        [Fact]
        public void To_propagate_HttpState_to_HttpResultWithValue_if_status_is_ok()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Ok(1, httpState);
            var httpResult = result.ToHttpResult();
            httpResult.HttpState.ShouldBe(httpState);
        }

        [Fact]
        public void To_propagates_HttpState_to_HttpResultWithValue_if_status_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Fail<int>(httpState);
            var httpResult = result.ToHttpResult();
            httpResult.HttpState.ShouldBe(httpState);
        }
    }
}

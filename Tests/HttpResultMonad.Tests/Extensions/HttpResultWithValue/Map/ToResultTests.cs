using HttpResultMonad.Extensions.HttpResultWithValue.Map;
using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.Extensions.HttpResultWithValue.Map
{
    public class ToResultTests
    {
        [Fact]
        public void To_propagates_ok_result_to_httpResult()
        {
            var result = HttpResult.Ok(1);
            var httpResult = result.ToHttpResult();
            httpResult.IsSuccess.ShouldBe(result.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_result_to_httpResult()
        {
            var result = HttpResult.Fail<int>();
            var httpResult = result.ToHttpResult();
            httpResult.IsFailure.ShouldBe(result.IsFailure);
        }

        [Fact]
        public void To_propagate_http_state_to_httpResult_if_http_result_is_ok()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Ok(1, httpState);
            var httpResult = result.ToHttpResult();
            httpResult.HttpState.ShouldBe(httpState);
        }

        [Fact]
        public void To_propagates_http_state_to_httpResult_if_http_result_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Fail<int>(httpState);
            var httpResult = result.ToHttpResult();
            httpResult.HttpState.ShouldBe(httpState);
        }
    }
}

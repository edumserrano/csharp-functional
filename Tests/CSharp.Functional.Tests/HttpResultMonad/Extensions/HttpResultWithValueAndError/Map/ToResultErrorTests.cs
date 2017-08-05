using HttpResultMonad;
using HttpResultMonad.Extensions.HttpResultWithValueAndError.Map;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.HttpResultMonad.Extensions.HttpResultWithValueAndError.Map
{
    public class ToResultErrorTests
    {
        [Fact]
        public void To_propagates_ok_result_to_httpResultError()
        {
            var result = HttpResult.Ok<int, string>(1);
            var httpResult = result.ToHttpResultError();
            httpResult.IsSuccess.ShouldBe(result.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_result_to_httpResultError()
        {
            var result = HttpResult.Fail<int, string>("error");
            var httpResult = result.ToHttpResultError();
            httpResult.IsFailure.ShouldBe(result.IsFailure);
        }

        [Fact]
        public void To_propagates_error_to_httpResultError_if_http_result_is_fail()
        {
            var result = HttpResult.Fail<int, string>("error");
            var httpResult = result.ToHttpResultError();
            httpResult.Error.ShouldBe(result.Error);
        }

        [Fact]
        public void To_propagates_http_state_to_httpResultError_if_http_result_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Fail<int, string>("error", httpState);
            var httpResult = result.ToHttpResultError();
            httpResult.HttpState.ShouldBe(result.HttpState);
        }

        [Fact]
        public void To_propagates_http_state_to_httpResultError_if_http_result_is_ok()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Ok<int, string>(1, httpState);
            var httpResult = result.ToHttpResultError();
            httpResult.HttpState.ShouldBe(result.HttpState);
        }
    }
}

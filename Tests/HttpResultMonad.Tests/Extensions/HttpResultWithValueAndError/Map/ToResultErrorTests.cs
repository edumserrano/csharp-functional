using HttpResultMonad.Extensions.HttpResultWithValueAndError.Map;
using Shouldly;
using Tests.Shared;
using Xunit;

namespace HttpResultMonad.Tests.Extensions.HttpResultWithValueAndError.Map
{
    [Trait("Monad", "HttpResultWithValueAndError")]
    public class ToResultWithErrorTests
    {
        [Fact]
        public void To_propagates_ok_status_to_HttpResultWithError()
        {
            var result = HttpResult.Ok<int, string>(1);
            var httpResult = result.ToHttpResultWithError();
            httpResult.IsSuccess.ShouldBe(result.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_status_to_HttpResultWithError()
        {
            var result = HttpResult.Fail<int, string>("error");
            var httpResult = result.ToHttpResultWithError();
            httpResult.IsFailure.ShouldBe(result.IsFailure);
        }

        [Fact]
        public void To_propagates_error_to_HttpResultWithError_if_status_is_fail()
        {
            var result = HttpResult.Fail<int, string>("error");
            var httpResult = result.ToHttpResultWithError();
            httpResult.Error.ShouldBe(result.Error);
        }

        [Fact]
        public void To_propagates_HttpState_to_HttpResultWithError_if_status_is_fail()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Fail<int, string>("error", httpState);
            var httpResult = result.ToHttpResultWithError();
            httpResult.HttpState.ShouldBe(result.HttpState);
        }

        [Fact]
        public void To_propagates_HttpState_to_HttpResultWithError_if_status_is_ok()
        {
            var httpState = Test.CreateHttpStateA();
            var result = HttpResult.Ok<int, string>(1, httpState);
            var httpResult = result.ToHttpResultWithError();
            httpResult.HttpState.ShouldBe(result.HttpState);
        }
    }
}

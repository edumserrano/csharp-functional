using ResultMonad.Extensions.ResultWithValueAndError.Map;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.Extensions.ResultWithValueAndError.Map
{
    [Trait("Monad", "ResultSimple")]
    public class ToResultWithErrorTests
    {
        [Fact]
        public void To_creates_ok_ResultWithError_if_ResultWithValueAndError_is_ok()
        {
            var resultWithValueAndError = Result.Ok<int, string>(1);
            var resultWithError = resultWithValueAndError.ToResultWithError();
            resultWithError.IsSuccess.ShouldBe(resultWithValueAndError.IsSuccess);
        }

        [Fact]
        public void To_create_fail_ResultWithError_if_ResultWithValueAndError_is_fail()
        {
            var resultWithValueAndError = Result.Fail<int, string>("error");
            var resultWithError = resultWithValueAndError.ToResultWithError();
            resultWithError.IsFailure.ShouldBe(resultWithValueAndError.IsFailure);
        }

        [Fact]
        public void To_propagates_error_to_ResultWithError_if_ResultWithValueAndError_is_fail()
        {
            var resultWithValueAndError = Result.Fail<int, string>("error");
            var resultWithError = resultWithValueAndError.ToResultWithError();
            resultWithError.Error.ShouldBe(resultWithValueAndError.Error);
        }
    }
}

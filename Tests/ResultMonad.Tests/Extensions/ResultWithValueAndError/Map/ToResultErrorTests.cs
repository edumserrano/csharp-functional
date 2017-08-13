using ResultMonad.Extensions.ResultWithValueAndError.Map;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.Extensions.ResultWithValueAndError.Map
{
    public class ToResultWithErrorTests
    {
        [Fact]
        public void To_propagates_ok_result_to_ResultWithError()
        {
            var resultWithValueAndError = Result.Ok<int, string>(1);
            var resultWithError = resultWithValueAndError.ToResultWithError();
            resultWithError.IsSuccess.ShouldBe(resultWithValueAndError.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_result_to_ResultWithError()
        {
            var resultWithValueAndError = Result.Fail<int, string>("error");
            var resultWithError = resultWithValueAndError.ToResultWithError();
            resultWithError.IsFailure.ShouldBe(resultWithValueAndError.IsFailure);
        }

        [Fact]
        public void To_propagates_error_from_result_to_ResultWithError()
        {
            var resultWithValueAndError = Result.Fail<int, string>("error");
            var resultWithError = resultWithValueAndError.ToResultWithError();
            resultWithError.Error.ShouldBe(resultWithValueAndError.Error);
        }
    }
}

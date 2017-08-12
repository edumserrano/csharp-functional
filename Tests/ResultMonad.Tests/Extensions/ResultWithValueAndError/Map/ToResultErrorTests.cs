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
            var result = Result.Ok<int, string>(1);
            var httpResult = result.ToResultWithError();
            httpResult.IsSuccess.ShouldBe(result.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_result_to_ResultWithError()
        {
            var result = Result.Fail<int, string>("error");
            var httpResult = result.ToResultWithError();
            httpResult.IsFailure.ShouldBe(result.IsFailure);
        }

        [Fact]
        public void To_propagates_error_from_result_to_ResultWithError()
        {
            var result = Result.Fail<int, string>("error");
            var httpResult = result.ToResultWithError();
            httpResult.Error.ShouldBe(result.Error);
        }
    }
}

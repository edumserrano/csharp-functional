using ResultMonad;
using ResultMonad.Extensions.ResultWithValueAndError.Map;
using Shouldly;
using Xunit;

namespace CSharp.Functional.Tests.ResultMonad.Extensions.ResultWithValueAndError.Map
{
    public class ToResultErrorTests
    {
        [Fact]
        public void To_propagates_ok_result_to_resultError()
        {
            var result = Result.Ok<int, string>(1);
            var httpResult = result.ToResultError();
            httpResult.IsSuccess.ShouldBe(result.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_result_to_resultError()
        {
            var result = Result.Fail<int, string>("error");
            var httpResult = result.ToResultError();
            httpResult.IsFailure.ShouldBe(result.IsFailure);
        }

        [Fact]
        public void To_propagates_error_from_result_to_resultError()
        {
            var result = Result.Fail<int, string>("error");
            var httpResult = result.ToResultError();
            httpResult.Error.ShouldBe(result.Error);
        }
    }
}

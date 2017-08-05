using ResultMonad.Extensions.ResultWithValue.Map;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.Extensions.ResultWithValue.Map
{
    public class ToResultTests
    {
        [Fact]
        public void To_propagates_ok_result_to_resultError()
        {
            var result = Result.Ok(1);
            var httpResult = result.ToResult();
            httpResult.IsSuccess.ShouldBe(result.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_result_to_resultError()
        {
            var result = Result.Fail<int>();
            var httpResult = result.ToResult();
            httpResult.IsFailure.ShouldBe(result.IsFailure);
        }
    }
}

using ResultMonad.Extensions.ResultWithValue.Map;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.Extensions.ResultWithValue.Map
{
    public class ToResultTests
    {
        [Fact]
        public void To_propagates_ok_status_from_ResultWithValue_to_Result()
        {
            var resultWithValue = Result.Ok(1);
            var result = resultWithValue.ToResult();
            result.IsSuccess.ShouldBe(resultWithValue.IsSuccess);
        }

        [Fact]
        public void To_propagates_fail_result_to_Result()
        {
            var resultWithvalue = Result.Fail<int>();
            var result = resultWithvalue.ToResult();
            result.IsFailure.ShouldBe(resultWithvalue.IsFailure);
        }
    }
}

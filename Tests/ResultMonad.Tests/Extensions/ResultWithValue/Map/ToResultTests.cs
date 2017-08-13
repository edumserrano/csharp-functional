using ResultMonad.Extensions.ResultWithValue.Map;
using Shouldly;
using Xunit;

namespace ResultMonad.Tests.Extensions.ResultWithValue.Map
{
    [Trait("Monad", "Result")]
    public class ToResultTests
    {
        [Fact]
        public void ToResult_creates_ok_SimpleResult_if_ResultWithValue_is_ok()
        {
            var resultWithValue = Result.Ok(1);
            var simpleResult = resultWithValue.ToResult();
            simpleResult.IsSuccess.ShouldBe(resultWithValue.IsSuccess);
        }

        [Fact]
        public void ToResult_creats_fail_SimpleResult_if_ResultWithValue_is_fail()
        {
            var resultWithvalue = Result.Fail<int>();
            var simpleResult = resultWithvalue.ToResult();
            simpleResult.IsFailure.ShouldBe(resultWithvalue.IsFailure);
        }
    }
}

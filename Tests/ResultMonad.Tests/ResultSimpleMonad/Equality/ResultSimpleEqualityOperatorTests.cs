using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultSimpleMonad.Equality
{
    [Trait("Monad", "ResultSimple")]
    public class ResultSimpleEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_ResultSimple_is_true()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_ResultSimple_is_true()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_ResultSimple_and_fail_ResultSimple_is_false()
        {
            var okResult = Result.Ok();
            var failResult = Result.Fail();
            var isEqual = okResult == failResult;
            isEqual.ShouldBeFalse();
        }
    }
}

using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultSimpleMonad.Equality
{
    [Trait("Monad", "ResultSimple")]
    public class ResultSimpleInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_ok_ResultSimple_is_false()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_ResultSimple_is_false()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_ResultSimple_and_fail_ResultSimple_is_true()
        {
            var okResult = Result.Ok();
            var failResult = Result.Fail();
            var isDifferent = okResult != failResult;
            isDifferent.ShouldBeTrue();
        }
    }
}

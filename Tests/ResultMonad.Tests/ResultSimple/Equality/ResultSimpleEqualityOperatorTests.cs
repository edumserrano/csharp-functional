using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultSimple.Equality
{
    [Trait("Monad", "ResultSimple")]
    public class ResultSimpleEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_two_ok_results_is_true()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_results_is_true()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_result_and_fail_result_is_false()
        {
            var okResult = Result.Ok();
            var failResult = Result.Fail();
            var isEqual = okResult == failResult;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_results_is_false()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_results_is_false()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_result_and_fail_result_is_true()
        {
            var okResult = Result.Ok();
            var failResult = Result.Fail();
            var isDifferent = okResult != failResult;
            isDifferent.ShouldBeTrue();
        }
    }
}

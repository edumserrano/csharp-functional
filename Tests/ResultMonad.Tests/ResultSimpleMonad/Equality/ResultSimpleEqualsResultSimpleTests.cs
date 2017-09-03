using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultSimpleMonad.Equality
{
    [Trait("Monad", "ResultSimple")]
    public class ResultSimpleEqualsResultSimpleTests
    {
        [Fact]
        public void Equals_between_two_ok_ResultSimple_is_true()
        {
            var result1 = Result.Ok();
            var result2 = Result.Ok();
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_ResultSimple_is_true()
        {
            var result1 = Result.Fail();
            var result2 = Result.Fail();
            var isEqual = result1.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_ResultSimple_and_fail_ResultSimple_is_false()
        {
            var result1 = Result.Ok();
            var result2 = Result.Fail();
            var isEqual = result1.Equals(result2);
            var isEqual2 = result2.Equals(result1);
            isEqual.ShouldBeFalse();
            isEqual2.ShouldBeFalse();
        }
    }
}

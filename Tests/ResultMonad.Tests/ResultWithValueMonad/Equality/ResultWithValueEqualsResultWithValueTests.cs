using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueMonad.Equality
{
    [Trait("Monad", "ResultWithValue")]
    public class ResultWithValueEqualsResultWithValueTests
    {
        [Fact]
        public void Equals_between_two_ok_ResultWithValue_is_true_if_they_have_the_same_value()
        {
            var value = "abc";
            var result = Result.Ok(value);
            var result2 = Result.Ok(value);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_ResultWithValue_is_false_if_they_do_not_have_the_same_value()
        {
            var result = Result.Ok("abc");
            var result2 = Result.Ok("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_ResultWithValue_is_true()
        {
            var result = Result.Fail<string>();
            var result2 = Result.Fail<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_ResultWithValue_and_fail_ResultWithValue_is_false()
        {
            var result = Result.Ok("abc");
            var result2 = Result.Fail<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}

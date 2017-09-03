using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueMonad.Equality
{
    [Trait("Monad", "ResultWithValue")]
    public class ResultWithValueGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_ok_ResultWithValue_is_equal_if_both_values_are_equal()
        {
            var value = "abc";
            var result1 = Result.Ok(value);
            var result2 = Result.Ok(value);
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_ok_ResultWithValue_is_not_equal_if_values_are_not_equal()
        {
            var result1 = Result.Ok(1);
            var result2 = Result.Ok(2);
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_ResultWithValue_is_not_equal_if_values_are_not_of_the_same_type()
        {
            var result1 = Result.Fail<int>();
            var result2 = Result.Fail<string>();
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_ok_ResultWithValue_and_fail_ResultWithValue_is_not_equal()
        {
            var result1 = Result.Ok("abc");
            var result2 = Result.Fail<string>();
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }
    }
}

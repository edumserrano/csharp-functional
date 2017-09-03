using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "ResultWithValueAndError")]
    public class ResultWithValueAndErrorGetHashCodeTests
    {
        [Fact]
        public void GetHasCode_between_two_ok_ResultWithValueAndError_is_equal_if_both_values_are_equal()
        {
            var value = "abc";
            var result1 = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            result1.GetHashCode().ShouldBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_ok_ResultWithValueAndError_is_not_equal_if_values_are_not_equal()
        {
            var result1 = Result.Ok<int, string>(1);
            var result2 = Result.Ok<int, string>(2);
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_ok_ResultWithValueAndError_is_not_equal_if_errors_are_not_of_the_same_type()
        {
            var value = 1;
            var result1 = Result.Ok<int, bool>(value);
            var result2 = Result.Ok<int, string>(value);
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_ResultWithValueAndError_is_not_equal_if_values_are_not_of_the_same_type()
        {
            var result1 = Result.Fail<int, string>("error");
            var result2 = Result.Fail<string, string>("error");
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_two_fail_ResultWithValueAndError_is_not_equal_if_errors_are_not_of_the_same_type()
        {
            var result1 = Result.Fail<string, int>(1);
            var result2 = Result.Fail<string, string>("error");
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }

        [Fact]
        public void GetHasCode_between_ok_ResultWithValueAndError_and_fail_ResultWithValueAndError_is_not_equal()
        {
            var result1 = Result.Ok<string, string>("value");
            var result2 = Result.Fail<string, string>("error");
            result1.GetHashCode().ShouldNotBe(result2.GetHashCode());
        }
    }
}

using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithValueAndErrorMonad.Equality
{
    [Trait("Monad", "ResultWithValueAndError")]
    public class ResultWithValueAndErrorEqualsResultWithValueAndErrorTests
    {
        [Fact]
        public void Equals_between_two_ok_ResultWithValueAndError_is_true_if_they_have_the_same_value()
        {
            var value = "abc";
            var result = Result.Ok<string, string>(value);
            var result2 = Result.Ok<string, string>(value);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_ok_ResultWithValueAndError_is_false_if_they_do_not_have_the_same_value()
        {
            var result = Result.Ok<string, string>("abc");
            var result2 = Result.Ok<string, string>("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_fail_ResultWithValueAndError_is_true_if_they_have_the_same_error()
        {
            var error = "abc";
            var result = Result.Fail<string, string>(error);
            var result2 = Result.Fail<string, string>(error);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_ResultWithValueAndError_is_false_if_they_do_not_have_the_same_error()
        {
            var result = Result.Fail<string, string>("abc");
            var result2 = Result.Fail<string, string>("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_ok_ResultWithValueAndError_and_fail_ResultWithValueAndError_is_false()
        {
            var result = Result.Ok<string, string>("abc");
            var result2 = Result.Fail<string, string>("abc");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}

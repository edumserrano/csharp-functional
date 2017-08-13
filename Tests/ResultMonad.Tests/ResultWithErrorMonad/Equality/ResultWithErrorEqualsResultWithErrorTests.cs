using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithErrorMonad.Equality
{
    [Trait("Monad", "ResultWithError")]
    public class ResultWithErrorEqualsResultWithErrorTests
    {
        [Fact]
        public void Equals_between_two_fail_ResultWithErrors_is_true_if_both_errors_are_equal()
        {
            var error = "abc";
            var result = ResultWithError.Fail(error);
            var result2 = ResultWithError.Fail(error);
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_two_fail_ResultWithErrors_is_false_if_they_both_errors_are_not_equal()
        {
            var result = ResultWithError.Fail("abc");
            var result2 = ResultWithError.Fail("zzz");
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equals_between_two_ok_ResultWithErrors_is_true()
        {
            var result = ResultWithError.Ok<string>();
            var result2 = ResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equals_between_ok_ResultWithError_and_fail_ResultWithError_is_false()
        {
            var result = ResultWithError.Fail("abc");
            var result2 = ResultWithError.Ok<string>();
            var isEqual = result.Equals(result2);
            isEqual.ShouldBeFalse();
        }
    }
}

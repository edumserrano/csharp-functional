using Shouldly;
using Xunit;

namespace ResultMonad.Tests.ResultWithErrorMonad.Equality
{
    [Trait("Monad", "ResultWithError")]
    public class ResultWithErrorEqualityOperatorTests
    {
        [Fact]
        public void Equality_operator_between_error_and_fail_ResultWithError_is_true_if_errors_are_equal()
        {
            var error = "abc";
            var result = ResultWithError.Fail(error);
            var isEqual = result == error;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_error_and_ok_ResultWithError_is_false()
        {
            var error = "abc";
            var result = ResultWithError.Ok<string>();
            var isEqual = result == error;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_two_fail_ResultWithErrors_is_true_if_errors_are_equal()
        {
            var error = "abc";
            var result1 = ResultWithError.Fail(error);
            var result2 = ResultWithError.Fail(error);
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_two_fail_ResultWithErrors_is_false_if_errors_are_not_equal()
        {
            var result1 = ResultWithError.Fail("abc");
            var result2 = ResultWithError.Fail("zzz");
            var isEqual = result1 == result2;
            isEqual.ShouldBeFalse();
        }

        [Fact]
        public void Equality_operator_between_ok_ResultWithErrors_is_true()
        {
            var result1 = ResultWithError.Ok<string>();
            var result2 = ResultWithError.Ok<string>();
            var isEqual = result1 == result2;
            isEqual.ShouldBeTrue();
        }

        [Fact]
        public void Equality_operator_between_ok_ResultWithError_and_fail_ResultWithError_is_false()
        {
            var okResult = ResultWithError.Ok<string>();
            var errorResult = ResultWithError.Fail("abc");
            var isEqual = okResult == errorResult;
            isEqual.ShouldBeFalse();
        }
    }
}

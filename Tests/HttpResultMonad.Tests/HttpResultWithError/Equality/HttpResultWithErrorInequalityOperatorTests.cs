using Shouldly;
using Xunit;

namespace HttpResultMonad.Tests.HttpResultWithError.Equality
{
    public class HttpResultWithErrorInequalityOperatorTests
    {
        [Fact]
        public void Inequality_operator_between_two_fail_httpResultWithErrors_is_false_if_the_http_state_and_the_error_are_the_same()
        {
            var error = "abc";
            var result1 = HttpResultMonad.HttpResultWithError.Fail(error);
            var result2 = HttpResultMonad.HttpResultWithError.Fail(error);
            var result3 = HttpResultMonad.HttpResultWithError.Fail(error, Test.CreateHttpStateA());
            var result4 = HttpResultMonad.HttpResultWithError.Fail(error, Test.CreateHttpStateA());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_httpResultWithErrors_with_different_error_is_true()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Fail("abc");
            var result2 = HttpResultMonad.HttpResultWithError.Fail("zzz");
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_fail_httpResultWithErrors_with_different_http_state_is_true()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateA());
            var result2 = HttpResultMonad.HttpResultWithError.Fail("abc", Test.CreateHttpStateB());
            var isDifferent = result1 != result2;
            isDifferent.ShouldBeTrue();
        }

        [Fact]
        public void Inequality_operator_between_two_ok_httpResultWithErrors_is_false_if_http_state_are_the_same()
        {
            var result1 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result2 = HttpResultMonad.HttpResultWithError.Ok<string>();
            var result3 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());
            var result4 = HttpResultMonad.HttpResultWithError.Ok<string>(Test.CreateHttpStateA());

            var isDifferent1 = result1 != result2;
            var isDifferent2 = result3 != result4;

            isDifferent1.ShouldBeFalse();
            isDifferent2.ShouldBeFalse();
        }

        [Fact]
        public void Inequality_operator_between_ok_httpResultWithError_and_fail_httpResultWithError_is_true()
        {
            var okResult = HttpResultMonad.HttpResultWithError.Ok<string>();
            var errorResult = HttpResultMonad.HttpResultWithError.Fail("abc");
            var isEqual = okResult != errorResult;
            isEqual.ShouldBeTrue();
        }
    }
}
